using FlexiTeams.Graph.Nodes;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using Assets.Scripts.Application;
using Assets.Scripts.UI.Workflow3DUI;
using UnityEngine.XR.Interaction.Toolkit;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using UnityEngine.UIElements;
using FlexiTeams.DataClasses.Task.Wrapper;

public class Layout3D : MonoBehaviour
{
    private GameObject Layout3DObj;

    private VR_AR_Application application;
    private UISettings settings;

    public readonly List<Layer3D> Layers = new();
    public readonly Dictionary<WorkflowId , Workflow3D> workflowMap = new();

    private DateTime begin;
    private DateTime end;

    private float zPosition = 0;
    private float zIncrement = 0;
    private float depth = 0;

    private float xOffset = 0;

    private const float meterToMillimeter = 1000;

    // Start is called before the first frame update
    void Start()
    {
        application = GameObject.Find("Application").GetComponent<VR_AR_Application>();
        Layout3DObj = GameObject.Find("Layout 3D");
        settings = application.Settings;

        int lCount = 0;

        application.Graph.GetWorkflowNodes().ForEach(node => {

            var layer = new Layer3D(this, application, "Layer_" + lCount++);
            
            var workflow = new Workflow3D() {
                Application = application,
                Id = node.Id,
                Layer = layer,
                Settings = settings
            };

            layer.Add(workflow);
            workflowMap.Add(node.Id, workflow);
        });

        CalculateLayout();
        Draw();
    }

    public void CalculateLayout()
    {
        Layers.ForEach(layer => layer.CalculateLayout());

        zIncrement = settings.Layout3D.Spacing + settings.Layout3D.Workflow.TaskDimensions.z;
        zPosition = -(((Layers.Count - 1) * zIncrement) / 2);
        depth = zIncrement * (Layers.Count - 1) + settings.Layout3D.Workflow.TaskDimensions.z;

        //Increment for the workflows position
        int maxPathCount = 0;
        var minDateTime = DateTime.MaxValue;
        Layers.ForEach(layer => {
            layer.Workflows.ForEach(workflow =>
            {
                var wNode = application.Graph.FindNode(workflow.Id);
                int pathCount = application.Graph.GetLongestPath(wNode).Count;
                if (maxPathCount < pathCount) maxPathCount = pathCount;

                var startTaskId = application.Graph.FindNode(workflow.Id).StartNodeId;
                var task = application.TaskPool[startTaskId];
                if (task.begin < minDateTime) minDateTime = task.begin;
            });
        });

        xOffset = - (0.6f * 8f / 2f);

        begin = minDateTime;
        end = begin.AddHours(8);
    }

    public void Remove(Layer3D layer)
    {
        Layers.Remove(layer);
    }

    public void Draw()
    {
        CleanUp();

        Layers.ForEach(layer =>
        {
            var layerObj = CreateLayerObject(layer);
            var transform = layerObj.transform;
            transform.SetParent(Layout3DObj.transform);
            transform.SetLocalPositionAndRotation(new Vector3(0, 0, zPosition), Quaternion.identity);
            zPosition += zIncrement;

            float yIncrement = layer.GetHeight() / layer.Workflows.Count;
            float yPos = - ((layer.Workflows.Count-1) *yIncrement/2);
            layer.Workflows.ForEach(workflow =>
            {
                var workflowObject = CreateWorkflowObject(workflow);
                var transform = workflowObject.GetComponent<Transform>();
                transform.SetParent(layerObj.transform);
                transform.SetLocalPositionAndRotation(new Vector3(0, yPos, 0), Quaternion.identity);
                yPos += yIncrement;
            });
        });

        var timeGridObj = CreateTimeGridObject();
        var transform = timeGridObj.transform;
        transform.SetParent(Layout3DObj.transform);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void CleanUp()
    {
        foreach (Transform child in Layout3DObj.transform)
        {
            Destroy(child.gameObject);
        }
    }

    //----------------------------------------------------------------
    //Unity Game Object creation
    //----------------------------------------------------------------

    private GameObject CreateLayerObject(Layer3D layer)
    {
        var layerObj = new GameObject(layer.Id)
        {
            layer = 5
        };

        return layerObj;
    }
    private GameObject CreateWorkflowObject(Workflow3D workflow)
    {
        var workflowObject = new GameObject(workflow.Id.ToString())
        {
            layer = 5
        };

        var layout = workflow.geometryGraph;

        var wNode = application.Graph.FindNode(workflow.Id);

        var taskNodes = application.Graph.GetTaskNodes(wNode);

        var height = workflow.GetHeight();
        taskNodes.ForEach(tNode =>
        {
            var task = application.TaskPool[tNode.Id];
            var middle = new DateTime(((task.begin.Ticks + task.end.Ticks) / 2L) - begin.Ticks);

            var xPos = Convert.ToSingle(TimeSpan.FromTicks(middle.Ticks).TotalMinutes / 100d);

            var layoutNode = layout.FindNodeByUserData(tNode);
            float width = RoundAndConvert(layoutNode.BoundingBox.Width);
            float y = RoundAndConvert(layoutNode.BoundingBox.Center.Y);


            var taskObject = CreateTaskObject(tNode, width);
            taskObject.transform.SetParent(workflowObject.transform);
            taskObject.transform.SetLocalPositionAndRotation(new Vector3(xPos+xOffset, y-height/2, 0), Quaternion.identity);
        });

        foreach(var edge in layout.Edges)
        {
            var taskAId = ((TaskNode) edge.Source.UserData).Id;
            var taskBId = ((TaskNode) edge.Target.UserData).Id;

            var taskA = application.TaskPool[taskAId];
            var taskB = application.TaskPool[taskBId];

            var AMiddle = new DateTime(((taskA.begin.Ticks + taskA.end.Ticks) / 2L) - begin.Ticks);
            var AXPos = Convert.ToSingle(TimeSpan.FromTicks(AMiddle.Ticks).TotalMinutes / 100d);

            var BMiddle = new DateTime(((taskB.begin.Ticks + taskB.end.Ticks) / 2L) - begin.Ticks);
            var BXPos = Convert.ToSingle(TimeSpan.FromTicks(BMiddle.Ticks).TotalMinutes / 100d);

            float sourceX = AXPos;
            float targetX = BXPos;
            float sourceY = RoundAndConvert(edge.SourcePort.Location.Y);
            float targetY = RoundAndConvert(edge.TargetPort.Location.Y);


            if(taskA.end == taskB.begin & sourceY == targetY) { continue; }

            float xPos = (sourceX + targetX) / 2 + xOffset;
            float yPos = (sourceY + targetY) / 2;

            float edgeLength = targetX - sourceX;
            float edgeHeigth = sourceY - targetY;

            Vector3[] frontLinePositions;
            Vector3[] backLinePositions;

            if (application.Graph.GetNextTasks((TaskNode) edge.Source.UserData).Count > 1)
            {
                frontLinePositions = new Vector3[]
                {
                    new Vector3(-edgeLength/2,  edgeHeigth/2, 0),
                    new Vector3(-edgeLength/2, -edgeHeigth/2, 0),
                    new Vector3( edgeLength/2, -edgeHeigth/2, 0)
                };

                backLinePositions = new Vector3[]
                {
                    new Vector3( edgeLength /2,  edgeHeigth / 2, 0),
                    new Vector3( edgeLength /2, -edgeHeigth / 2, 0),
                    new Vector3(-edgeLength /2, -edgeHeigth / 2, 0)
                };
            }
            else
            {
                frontLinePositions = new Vector3[]
                {
                    new Vector3(-edgeLength/2,  edgeHeigth/2, 0),
                    new Vector3( edgeLength/2,  edgeHeigth/2, 0),
                    new Vector3( edgeLength/2, -edgeHeigth/2, 0),
                };

                backLinePositions = new Vector3[]
                {
                    new Vector3( edgeLength/2,  edgeHeigth/2, 0),
                    new Vector3(-edgeLength/2,  edgeHeigth/2, 0),
                    new Vector3(-edgeLength/2, -edgeHeigth/2, 0)
                };
            }



            var edgeObject = CreateEdgeObject(frontLinePositions, backLinePositions, workflow.GetHeight());

            edgeObject.transform.SetParent(workflowObject.transform);
            edgeObject.transform.SetLocalPositionAndRotation(new Vector3(xPos, yPos, 0), Quaternion.identity);
        }

        return workflowObject;
    }
    private GameObject CreateTaskObject(TaskNode taskNode, float width)
    {
    //Gameobject
        var taskObj = new GameObject(taskNode.Id.ToString()) {
            layer = 5
        };

        var dimensions = new Vector3()
        {
            x = width,
            y = settings.Layout3D.Workflow.TaskDimensions.y,
            z = settings.Layout3D.Workflow.TaskDimensions.z
        };

        //meshfilter
        var meshFilter = taskObj.AddComponent<MeshFilter>();
        meshFilter.mesh = CreateCuboidMesh(dimensions);

        //meshRenderer
        var meshRenderer = taskObj.AddComponent<MeshRenderer>();
        meshRenderer.material = settings.Layout3D.Workflow.TaskNormalMaterial;

        //Collider
        var collider = taskObj.AddComponent<BoxCollider>();

        //XR-Simple Interactable
        var interactable = taskObj.AddComponent<XRSimpleInteractable>();
        interactable.interactionLayers = InteractionLayerMask.GetMask("UI");
        
        //Button
        var taskButton3D = taskObj.AddComponent<TaskButton3D>();
        taskButton3D.application = application;
        taskButton3D.ID = taskNode.Id;
        taskButton3D.leftRayInteractor = application.leftRayInteractor;
        taskButton3D.rightRayInteractor = application.rightRayInteractor;
        taskButton3D.AddListener(taskButton3D.onClicK);

        var textObject = CreateTextObject(application.TaskPool[taskNode.Id].Type.ToString(), width-settings.Layout3D.Workflow.FrameWidth);
        textObject.transform.SetParent(taskObj.transform);
        textObject.transform.position = new Vector3(0, 0, - (settings.Layout3D.Workflow.TaskDimensions.z / 2) - 0.001f);

        var frameObj = CreateFrameObj(dimensions, settings.Layout3D.Workflow.FrameWidth);
        frameObj.transform.SetParent(taskObj.transform);

        return taskObj;
    }
    private GameObject CreateTextObject(string text, float width)
    {
        //GameObject
        var textObject = new GameObject("Text")
        {
            layer = 5
        };

        //RectTansform
        var tranform = textObject.AddComponent<RectTransform>();
        tranform.sizeDelta = new Vector2(width, settings.Layout3D.Workflow.TaskDimensions.y);

        //TextMesh
        var textMesh = textObject.AddComponent<TextMeshPro>();
        textMesh.text = text;
        textMesh.color = settings.Layout3D.Workflow.TextColor;
        textMesh.fontSize = settings.Layout3D.Workflow.FontSize;
        textMesh.fontStyle = settings.FontStyle;

        textMesh.verticalAlignment = VerticalAlignmentOptions.Middle;
        textMesh.horizontalAlignment = HorizontalAlignmentOptions.Center;

        textMesh.enableWordWrapping = true;

        return textObject;
    }
    private GameObject CreateEdgeObject(Vector3[] frontPositions, Vector3[] backPositions, float heigth)
    {
        //GameObjects
        var edgeObject = new GameObject("Edge")
        {
            layer = 5
        };
        var frontOutLineObj = new GameObject("Line")
        {
            layer = 5
        };
        var backOutLineObj = new GameObject("Line")
        {
            layer = 5
        };

        var frontFillLineObj = new GameObject("Line")
        {
            layer = 5
        };

        var backFillLineObj = new GameObject("Line")
        {
            layer = 5
        };

        //Transform        
        frontOutLineObj.transform.SetParent(edgeObject.transform);
        frontOutLineObj.transform.SetLocalPositionAndRotation(new Vector3(0, -heigth/2, 0), Quaternion.identity);
        backOutLineObj.transform.SetParent(edgeObject.transform);
        backOutLineObj.transform.SetLocalPositionAndRotation(new Vector3(0, -heigth / 2, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
        frontFillLineObj.transform.SetParent(edgeObject.transform);
        frontFillLineObj.transform.SetLocalPositionAndRotation(new Vector3(0, -heigth / 2, -0.001f), Quaternion.identity);
        backFillLineObj.transform.SetParent(edgeObject.transform);
        backFillLineObj.transform.SetLocalPositionAndRotation(new Vector3(0, -heigth / 2, 0.001f), Quaternion.Euler(new Vector3(0, 180, 0)));

        //frontOutLine-Renderer
        var frontOutLineRenderer = frontOutLineObj.AddComponent<LineRenderer>();
        frontOutLineRenderer.positionCount = 3;
        frontOutLineRenderer.SetPositions(frontPositions);
        frontOutLineRenderer.alignment = LineAlignment.TransformZ;
        frontOutLineRenderer.useWorldSpace = false;
        frontOutLineRenderer.material = settings.Layout3D.Workflow.EdgeOutlineMaterial;

        AnimationCurve frontOutLineCurve = new();
        frontOutLineCurve.AddKey(0, settings.Layout3D.Workflow.EdgeDimensions.z);
        frontOutLineCurve.AddKey(1, settings.Layout3D.Workflow.EdgeDimensions.z);
        frontOutLineRenderer.widthCurve = frontOutLineCurve;

        //frontFillLine-Renderer
        var frontFillLineRenderer = frontFillLineObj.AddComponent<LineRenderer>();
        frontFillLineRenderer.positionCount = 3;
        frontFillLineRenderer.SetPositions(frontPositions);
        frontFillLineRenderer.alignment = LineAlignment.TransformZ;
        frontFillLineRenderer.useWorldSpace = false;
        frontFillLineRenderer.material = settings.Layout3D.Workflow.EdgeFillMaterial;

        AnimationCurve frontFillLineCurve = new();
        frontFillLineCurve.AddKey(0, settings.Layout3D.Workflow.EdgeDimensions.z - 0.0075f);
        frontFillLineCurve.AddKey(1, settings.Layout3D.Workflow.EdgeDimensions.z - 0.0075f);
        frontFillLineRenderer.widthCurve = frontFillLineCurve;

        //backOutLineObjLine-Renderer
        var backOutLineRenderer = backOutLineObj.AddComponent<LineRenderer>();
        backOutLineRenderer.positionCount = 3;
        backOutLineRenderer.SetPositions(backPositions);
        backOutLineRenderer.alignment = LineAlignment.TransformZ;
        backOutLineRenderer.useWorldSpace = false;
        backOutLineRenderer.material = settings.Layout3D.Workflow.EdgeOutlineMaterial;

        AnimationCurve backCurve = new();
        backCurve.AddKey(0, settings.Layout3D.Workflow.EdgeDimensions.z);
        backCurve.AddKey(1, settings.Layout3D.Workflow.EdgeDimensions.z);
        backOutLineRenderer.widthCurve = backCurve;

        //backFillLine-Renderer
        var backFillLineRenderer = backFillLineObj.AddComponent<LineRenderer>();
        backFillLineRenderer.positionCount = 3;
        backFillLineRenderer.SetPositions(backPositions);
        backFillLineRenderer.alignment = LineAlignment.TransformZ;
        backFillLineRenderer.useWorldSpace = false;
        backFillLineRenderer.material = settings.Layout3D.Workflow.EdgeFillMaterial;

        AnimationCurve backFillLineCurve = new();
        backFillLineCurve.AddKey(0, settings.Layout3D.Workflow.EdgeDimensions.z - 0.01f);
        backFillLineCurve.AddKey(1, settings.Layout3D.Workflow.EdgeDimensions.z - 0.01f);
        backFillLineRenderer.widthCurve = backFillLineCurve;


        return edgeObject;
    }
    private GameObject CreateTimeGridObject()
    {
        var TimegridObj = new GameObject("Time Grid")
        {
            layer = 5
        };

        float maxHeight = float.MinValue;
        Layers.ForEach(layer =>
        {
            float height = layer.GetHeight();
            if(maxHeight < height) maxHeight = height;
        });

        DateTime currentBegin = begin;
        float xPos = -2.1f;

        //TimeBox
        for(int  i = 0; i < 8; i++)
        {
            var hourBoxObj = new GameObject("Hour_" + currentBegin.ToString("HH:mm") + "_" + currentBegin.AddHours(1).ToString("HH:mm"));
            var transform = hourBoxObj.transform;
            transform.SetParent(TimegridObj.transform);
            transform.SetPositionAndRotation(new Vector3(xPos, 0, 0), Quaternion.identity);

            //meshfilter
            var dimensions = new Vector3
            {
                x = 0.6f,
                y = maxHeight + 2 * settings.Layout3D.TimeGrid.Margin,
                z = depth + 2 * settings.Layout3D.TimeGrid.Margin
            };

            var meshFilter = hourBoxObj.AddComponent<MeshFilter>();
            meshFilter.mesh = CreateCuboidMesh(dimensions);

            //meshRenderer
            var meshRenderer = hourBoxObj.AddComponent<MeshRenderer>();
            meshRenderer.material = settings.Layout3D.TimeGrid.TimeBoxMaterial;

            currentBegin = currentBegin.AddHours(1);
            xPos += 0.6f;
        }

        //TimeText
        for( int i = 0; i < 9; i++)
        {
            var frontTopTextObj = CreateTimeGridTextObject(begin.AddHours(i).ToString("HH:mm"));
            var frontBottomTextObj = CreateTimeGridTextObject(begin.AddHours(i).ToString("HH:mm"));
            var backTopTextObj = CreateTimeGridTextObject(begin.AddHours(i).ToString("HH:mm"));
            var backBottomTextObj = CreateTimeGridTextObject(begin.AddHours(i).ToString("HH:mm"));

            var frontTopPos = new Vector3
            {
                x = -2.4f + i * 0.6f,
                y = (maxHeight / 2) + settings.Layout3D.TimeGrid.Margin + settings.Layout3D.TimeGrid.Textspacing,
                z = - (depth + 2 * settings.Layout3D.TimeGrid.Margin) / 2
            };

            var frontBottomPos = new Vector3
            {
                x = -2.4f + i * 0.6f,
                y = -((maxHeight / 2) + settings.Layout3D.TimeGrid.Margin + settings.Layout3D.TimeGrid.Textspacing),
                z = -(depth + 2 * settings.Layout3D.TimeGrid.Margin) / 2
            };

            var backTopPos = new Vector3
            {
                x = -2.4f + i * 0.6f,
                y = (maxHeight / 2) + settings.Layout3D.TimeGrid.Margin + settings.Layout3D.TimeGrid.Textspacing,
                z = (depth + 2 * settings.Layout3D.TimeGrid.Margin) / 2
            };

            var backBottomPos = new Vector3
            {
                x = -2.4f + i * 0.6f,
                y = -((maxHeight / 2) + settings.Layout3D.TimeGrid.Margin + settings.Layout3D.TimeGrid.Textspacing),
                z = (depth + 2 * settings.Layout3D.TimeGrid.Margin) / 2
            };

            //Transform
            var transform = frontTopTextObj.GetComponent<RectTransform>();
            transform.SetParent(TimegridObj.transform);
            transform.SetPositionAndRotation(frontTopPos, Quaternion.identity);
;
            transform = frontBottomTextObj.GetComponent<RectTransform>();
            transform.SetParent(TimegridObj.transform);
            transform.SetPositionAndRotation(frontBottomPos, Quaternion.identity);

            transform = backTopTextObj.GetComponent<RectTransform>();
            transform.SetParent(TimegridObj.transform);
            transform.SetPositionAndRotation(backTopPos, Quaternion.identity);

            transform = backBottomTextObj.GetComponent<RectTransform>();
            transform.SetParent(TimegridObj.transform);
            transform.SetPositionAndRotation(backBottomPos, Quaternion.identity);
        }

        return TimegridObj;
    }
    private GameObject CreateTimeGridTextObject(string text)
    {
        //GameObject
        var textObject = new GameObject("Time Label " + text)
        {
            layer = 5
        };

        //RectTansform
        var tranform = textObject.AddComponent<RectTransform>();
        tranform.sizeDelta = new Vector2(0.2f, settings.Layout3D.TimeGrid.FontSize);

        //TextMesh
        var textMesh = textObject.AddComponent<TextMeshPro>();
        textMesh.text = text;
        textMesh.color = settings.Layout3D.TimeGrid.TextColor;
        textMesh.fontSize = settings.Layout3D.TimeGrid.FontSize;
        textMesh.fontStyle = settings.FontStyle;

        textMesh.verticalAlignment = VerticalAlignmentOptions.Middle;
        textMesh.horizontalAlignment = HorizontalAlignmentOptions.Center;

        textMesh.enableWordWrapping = true;

        return textObject;
    }
    private GameObject CreateFrameObj(Vector3 dimensions, float width)
    {
        var frameObj = new GameObject("Frame")
        {
            layer = 5,
        };

        var xPos = dimensions.x / 2;
        var yPos = dimensions.y / 2;
        var zPos = dimensions.z / 2;

        var frame1 = SubFrame("Frame1");
        var frame2 = SubFrame("Frame2");

        frame1.transform.SetParent(frameObj.transform);
        frame1.transform.SetLocalPositionAndRotation(new Vector3(0,0,zPos), Quaternion.identity);
        frame2.transform.SetParent(frameObj.transform);
        frame2.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -zPos), Quaternion.Euler(new Vector3(0,180,0)));

        var frame3 = new GameObject("Frame3")
        {
            layer = 5,
        };
        frame3.transform.SetParent(frameObj.transform);
        frame3.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        //Edges
        var e1 = new GameObject("e1");
        var e2 = new GameObject("e2");
        var e3 = new GameObject("e3");
        var e4 = new GameObject("e4");

        var list = new List<GameObject>() {e1, e2, e3, e4};
        list.ForEach(obj =>
        {
            obj.transform.SetParent(frame3.transform);
            obj.AddComponent<MeshFilter>();
            var renderer = obj.AddComponent<MeshRenderer>();
            renderer.material = settings.Layout3D.Workflow.FrameMaterial;
        });

        e1.GetComponent<MeshFilter>().mesh = CreateEdgeMesh(new Vector2(width, dimensions.z - width));
        e2.GetComponent<MeshFilter>().mesh = CreateEdgeMesh(new Vector2(width, dimensions.z - width));
        e3.GetComponent<MeshFilter>().mesh = CreateEdgeMesh(new Vector2(width, dimensions.z - width));
        e4.GetComponent<MeshFilter>().mesh = CreateEdgeMesh(new Vector2(width, dimensions.z - width));

        e1.transform.SetLocalPositionAndRotation(new Vector3(-xPos, -yPos, 0), Quaternion.Euler(new Vector3(0, 90, 90)));
        e2.transform.SetLocalPositionAndRotation(new Vector3(-xPos, yPos, 0), Quaternion.Euler(new Vector3(0, 90, -90)));
        e3.transform.SetLocalPositionAndRotation(new Vector3(xPos, -yPos, 0), Quaternion.Euler(new Vector3(0, -90, 90)));
        e4.transform.SetLocalPositionAndRotation(new Vector3(xPos, yPos, 0), Quaternion.Euler(new Vector3(0, -90, -90)));

        GameObject SubFrame(string text)
        {
            var subFrame = new GameObject(text)
            {
                layer = 5
            };
            
            //Corners
            var c1 = new GameObject("c1");
            var c2 = new GameObject("c2");
            var c3 = new GameObject("c3");
            var c4 = new GameObject("c4");
            
            //Edges
            var e1 = new GameObject("e1");
            var e2 = new GameObject("e2");
            var e3 = new GameObject("e3");
            var e4 = new GameObject("e4");
            
            var list = new List<GameObject>()
            {
                c1, c2, c3, c4, 
                e1, e2, e3, e4
            };

            list.ForEach(obj =>
            {
                obj.transform.SetParent(subFrame.transform);
                obj.AddComponent<MeshFilter>();
                var renderer = obj.AddComponent<MeshRenderer>();
                renderer.material = settings.Layout3D.Workflow.FrameMaterial;
            });

            c1.GetComponent<MeshFilter>().mesh = CreateCornerMesh(width);
            c2.GetComponent<MeshFilter>().mesh = CreateCornerMesh(width);
            c3.GetComponent<MeshFilter>().mesh = CreateCornerMesh(width);
            c4.GetComponent<MeshFilter>().mesh = CreateCornerMesh(width);

            c1.transform.SetLocalPositionAndRotation(new Vector3(-xPos, -yPos, 0), Quaternion.Euler(new Vector3(0,0,90)));
            c2.transform.SetLocalPositionAndRotation(new Vector3(xPos, -yPos, 0), Quaternion.Euler(new Vector3(0, 90, 90)));
            c3.transform.SetLocalPositionAndRotation(new Vector3(-xPos, yPos, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
            c4.transform.SetLocalPositionAndRotation(new Vector3(xPos, yPos, 0), Quaternion.Euler(new Vector3(0, 0, -90)));

            e1.GetComponent<MeshFilter>().mesh = CreateEdgeMesh(new Vector2(width, dimensions.x - width));
            e2.GetComponent<MeshFilter>().mesh = CreateEdgeMesh(new Vector2(width, dimensions.x - width));
            e3.GetComponent<MeshFilter>().mesh = CreateEdgeMesh(new Vector2(width, dimensions.y - width));
            e4.GetComponent<MeshFilter>().mesh = CreateEdgeMesh(new Vector2(width, dimensions.y - width));

            e1.transform.SetLocalPositionAndRotation(new Vector3(0, -yPos, 0), Quaternion.Euler(new Vector3(-90, 0, 90)));
            e2.transform.SetLocalPositionAndRotation(new Vector3(0, yPos, 0), Quaternion.Euler(new Vector3(180, 0, 90)));
            e3.transform.SetLocalPositionAndRotation(new Vector3(-xPos, 0, 0), Quaternion.Euler(new Vector3(0, 90, 0)));
            e4.transform.SetLocalPositionAndRotation(new Vector3(xPos, 0, 0), Quaternion.Euler(new Vector3(0, 180,0)));

            return subFrame;
        }

        return frameObj;
    }

    //----------------------------------------------------------------
    //Mesh generation
    //----------------------------------------------------------------

    private Mesh CreateEdgeMesh(Vector2 dimensions)
    {
        var mesh = new Mesh()
        {
            name = "TubeMesh"
        };

        float deltaWidth = dimensions.x / 2;
        float deltaHeight = dimensions.y / 2;
        float deltaDepth = dimensions.x / 2;

        //Front
        var A = new Vector3(-deltaWidth, -deltaHeight, -deltaDepth);
        var B = new Vector3(deltaWidth, -deltaHeight, -deltaDepth);
        var C = new Vector3(deltaWidth, deltaHeight, -deltaDepth);
        var D = new Vector3(-deltaWidth, deltaHeight, -deltaDepth);
        //Left
        var Q = new Vector3(-deltaWidth, -deltaHeight, deltaDepth);
        var R = A;
        var S = D;
        var T = new Vector3(-deltaWidth, deltaHeight, deltaDepth);

        var a = 0;
        var b = 1;
        var c = 2;
        var d = 3;

        var q = 4;
        var r = 5;
        var s = 6;
        var t = 7;

        mesh.vertices = new Vector3[] { A, B, C, D, Q, R, S, T };
        mesh.triangles = new int[]
        {
                //Front Face
                a, d, b,
                d, c, b,
                //LeftFace
                q, t, r,
                t, s, r,
        };
        mesh.normals = new Vector3[]
        {
                //Front
                Vector3.back, Vector3.back, Vector3.back, Vector3.back,
                //Left
                Vector3.left, Vector3.left, Vector3.left, Vector3.left,
        };

        return mesh;
    }
    private Mesh CreateCornerMesh(float dimension)
    {
        var mesh = new Mesh()
        {
            name = "CornerMesh"
        };

        float deltaWidth = dimension / 2;
        float deltaHeight = dimension / 2;
        float deltaDepth = dimension / 2;

        //Back
        var E = new Vector3(-deltaWidth, -deltaHeight, deltaDepth);
        var F = new Vector3(deltaWidth, -deltaHeight, deltaDepth);
        var G = new Vector3(deltaWidth, deltaHeight, deltaDepth);
        var H = new Vector3(-deltaWidth, deltaHeight, deltaDepth);
        //Top
        var I = new Vector3(-deltaWidth, deltaHeight, -deltaDepth);
        var J = new Vector3(deltaWidth, deltaHeight, -deltaDepth);
        var K = G;
        var L = H;
        //Left
        var Q = E;
        var R = new Vector3(-deltaWidth, -deltaHeight, -deltaDepth);
        var S = I;
        var T = H;

        int e = 0;
        int f = 1;
        int g = 2;
        int h = 3;
        int i = 4;
        int j = 5;
        int k = 6;
        int l = 7;
        int q = 8;
        int r = 9;
        int s = 10;
        int t = 11;

        mesh.vertices = new Vector3[] { E, F, G, H, I, J, K, L, Q, R, S, T};
        mesh.triangles = new int[]
        {
                //Back Face
                f, g, e,
                g, h, e,
                //TopFace
                i, l, j,
                l, k, j,
                //LeftFace
                q, t, r,
                t, s, r,
        };
        mesh.normals = new Vector3[]
        {
                //Back
                Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,
                //Top
                Vector3.up, Vector3.up, Vector3.up, Vector3.up,
                //Left
                Vector3.left, Vector3.left, Vector3.left, Vector3.left,
        };

        return mesh;
    }
    private Mesh CreateCuboidMesh(Vector3 dimensions)
    {
        var mesh = new Mesh()
        {
            name = "CuboidMesh"
        };

        float deltaWidth = dimensions.x / 2;
        float deltaHeight = dimensions.y / 2;
        float deltaDepth = dimensions.z / 2;

        //Front
        var A = new Vector3(-deltaWidth, -deltaHeight, -deltaDepth);
        var B = new Vector3(deltaWidth, -deltaHeight, -deltaDepth);
        var C = new Vector3(deltaWidth, deltaHeight, -deltaDepth);
        var D = new Vector3(-deltaWidth, deltaHeight, -deltaDepth);
        //Back
        var E = new Vector3(-deltaWidth, -deltaHeight, deltaDepth);
        var F = new Vector3(deltaWidth, -deltaHeight, deltaDepth);
        var G = new Vector3(deltaWidth, deltaHeight, deltaDepth);
        var H = new Vector3(-deltaWidth, deltaHeight, deltaDepth);
        //Top
        var I = D;
        var J = C;
        var K = G;
        var L = H;
        //Bottom
        var M = E;
        var N = F;
        var O = B;
        var P = A;
        //Left
        var Q = E;
        var R = A;
        var S = D;
        var T = H;
        //Rigth
        var U = B;
        var V = F;
        var W = G;
        var X = C;

        var a = 0;
        var b = 1;
        var c = 2;
        var d = 3;
        var e = 4;
        var f = 5;
        var g = 6;
        var h = 7;
        var i = 8;
        var j = 9;
        var k = 10;
        var l = 11;
        var m = 12;
        var n = 13;
        var o = 14;
        var p = 15;
        var q = 16;
        var r = 17;
        var s = 18;
        var t = 19;
        var u = 20;
        var v = 21;
        var w = 22;
        var x = 23;


        mesh.vertices = new Vector3[] { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X };
        mesh.triangles = new int[]
        {
                //Front Face
                a, d, b,
                d, c, b,
                //Back Face
                f, g, e,
                g, h, e,
                //TopFace
                i, l, j,
                l, k, j,
                //BottomFace
                m, p, n,
                p, o, n,
                //LeftFace
                q, t, r,
                t, s, r,
                //RightFace
                u, x, v,
                x, w, v
        };
        mesh.normals = new Vector3[]
        {
                //Front
                Vector3.back, Vector3.back, Vector3.back, Vector3.back,
                //Back
                Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,
                //Top
                Vector3.up, Vector3.up, Vector3.up, Vector3.up,
                //Bottom
                Vector3.down, Vector3.down, Vector3.down, Vector3.down,
                //Left
                Vector3.left, Vector3.left, Vector3.left, Vector3.left,
                //Rigth
                Vector3.right, Vector3.right, Vector3.right, Vector3.right
        };

        return mesh;
    }
    
    //----------------------------------------------------------------
    //Utility functions
    //----------------------------------------------------------------
    
    private float RoundAndConvert(double value)
    {
        double temp = Math.Round(value, 0, MidpointRounding.AwayFromZero);
        temp /= meterToMillimeter;
        return Convert.ToSingle(temp);
    }

    //----------------------------------------------------------------
    //Time functions
    //----------------------------------------------------------------
    public void NextShift()
    {
        begin = begin.AddHours(8);
        end = end.AddHours(8);
    }

    public void PrevShift()
    {
        begin = begin.AddHours(-8);
        end = end.AddHours(-8);
    }
}