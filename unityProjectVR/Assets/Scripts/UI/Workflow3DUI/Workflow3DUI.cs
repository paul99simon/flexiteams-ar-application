using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Graph.Nodes;

using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Layout.Layered;

using System.Collections.Generic;
using System;

using TMPro;
using UnityEngine;
using Assets.Scripts.Application;
using Assets.Scripts.UI.Workflow3DUI;

public class Workflow3DUI : MonoBehaviour
{
    private VR_AR_Application application;
    
    private GameObject workflow3DObj;

    private UISettings settings;
    
    private const double meterToMillimeter = 1000;

    private float xOffset = 0;


    // Start is called before the first frame update
    void Start()
    {
        application = GameObject.Find("Application").GetComponent<VR_AR_Application>();
        workflow3DObj = GameObject.Find("Workflow 3D");
        settings = application.Settings;
        
        List<WorkflowNode> wNodes = new()
        {
            application.Graph.GetWorkflowNodes()[1],
            application.Graph.GetWorkflowNodes()[2]
        };


        Create3DWorkflowLayout(wNodes);
    }

    private void Create3DWorkflowLayout(List<WorkflowNode> wNodes)
    {
        //Increment for the workflows position
        float positionincrement = settings.Workflow3DSettings.WorkflowSpacing + settings.Workflow3DSettings.TaskDimensions.z;
        float position = -(((wNodes.Count - 1) * positionincrement) / 2); ;

        int maxPathCount = 0;
        wNodes.ForEach(wNode =>
        {
            int pathCount = application.Graph.GetLongestPath(wNode).Count;
            if (maxPathCount < pathCount) maxPathCount = pathCount;
        });

        int maxEdgeCount = maxPathCount - 1;
        float totalLength = maxPathCount * settings.Workflow3DSettings.TaskDimensions.x + maxEdgeCount * settings.Workflow3DSettings.EdgeDimensions.x;
        xOffset = - totalLength / 2;


        wNodes.ForEach(node =>
        {
            var workflowObject = CreateWorkflowObject(node);
            var transform = workflowObject.GetComponent<Transform>();
            transform.SetParent(workflow3DObj.transform);
            transform.SetLocalPositionAndRotation(new Vector3(0,0,position), Quaternion.identity);
            position += positionincrement;
        });
    }

    //----------------------------------------------------------------
    //Layout-Engine Microsoft Automatic Graph Layout
    //----------------------------------------------------------------
    
    private GeometryGraph SugiyamaGraph(WorkflowNode wNode)
    {
        //Layout variables
        double width = Convert.ToDouble(settings.Workflow3DSettings.TaskDimensions.y * meterToMillimeter);
        double heigth = Convert.ToDouble(settings.Workflow3DSettings.TaskDimensions.x * meterToMillimeter);
        double layerSeperation = Convert.ToDouble(settings.Workflow3DSettings.EdgeDimensions.x * meterToMillimeter);
        double nodeSeperation = Convert.ToDouble(settings.Workflow3DSettings.EdgeDimensions.y * meterToMillimeter);

        //Graph-Data
        var nodes = application.Graph.GetTaskNodes(wNode);
        var graph = new GeometryGraph();

        /*nodes*/
        nodes.ForEach(u =>
        {
            var node = new Microsoft.Msagl.Core.Layout.Node(CurveFactory.CreateRectangle(width, heigth, new Point()), u);
            graph.Nodes.Add(node);
        });

        /*edges*/
        nodes.ForEach(u =>
        {
            var nextTaskNodes = application.Graph.GetNextTasks(u);
            nextTaskNodes.ForEach(v => {
                graph.Edges.Add(new Edge(graph.FindNodeByUserData(u), graph.FindNodeByUserData(v)));
            });

        });

        //Sugiyama-layout settings
        var layoutSettings = new SugiyamaLayoutSettings();

        /*Up Down Vertical Constraints*/
        var longestPath = application.Graph.GetLongestPath(wNode);
        for (int i = 1; i < longestPath.Count; i++)
        {
            layoutSettings.AddUpDownVerticalConstraint(graph.FindNodeByUserData(longestPath[i - 1]), graph.FindNodeByUserData(longestPath[i]));
        }

        /*Add Left Right Constraints*/
        /*var notInLongestPath = new List<TaskNode>();
        nodes.ForEach(u => {
            if (!longestPath.Contains(u))  notInLongestPath.Add(u);
        });
        
        notInLongestPath.ForEach(u => {
            settings.AddLeftRightConstraint( graph.FindNodeByUserData(wNode.StartNode), graph.FindNodeByUserData(u));   
        });*/

        layoutSettings.LayerSeparation = layerSeperation;
        layoutSettings.NodeSeparation = nodeSeperation;

        //Layered Layout
        LayeredLayout layout = new(graph, layoutSettings);
        layout.Run();

        //PlaneTransformation
        double startNodeLeft = graph.FindNodeByUserData(application.Graph.FindNode(wNode.StartNodeId)).BoundingBox.Left;
        double startNodeTop = graph.FindNodeByUserData(application.Graph.FindNode(wNode.StartNodeId)).BoundingBox.Top;

        var pT = PlaneTransformation.Rotation(1.57079633) * new PlaneTransformation(1, 0, -startNodeLeft, 0, 1, -startNodeTop);
        graph.Transform(pT);

        return graph;
    }

    //----------------------------------------------------------------
    //Unity Game Object creation
    //----------------------------------------------------------------
    
    private GameObject CreateWorkflowObject(WorkflowNode wNode)
    {
        var workflowObject = new GameObject(wNode.Id.ToString())
        {
            layer = 5
        };

        var layout = SugiyamaGraph(wNode);

        var taskNodes = application.Graph.GetTaskNodes(wNode);

        taskNodes.ForEach(taskNode =>
        {
            var layoutNode = layout.FindNodeByUserData(taskNode);
            float x = RoundAndConvert(layoutNode.BoundingBox.Center.X);
            float y = RoundAndConvert(layoutNode.BoundingBox.Center.Y);

            var taskObject = CreateTaskObject(taskNode);
            taskObject.transform.SetParent(workflowObject.transform);
            taskObject.transform.SetLocalPositionAndRotation(new Vector3(x + xOffset, y, 0), Quaternion.identity);

        });

        foreach(var edge in layout.Edges)
        {
            float sourceX = RoundAndConvert(edge.SourcePort.Location.X);
            float sourceY = RoundAndConvert(edge.SourcePort.Location.Y);
            float targetX = RoundAndConvert(edge.TargetPort.Location.X);
            float targetY = RoundAndConvert(edge.TargetPort.Location.Y);

            float xPos = (sourceX + targetX) / 2 + xOffset;
            float yPos = (sourceY + targetY) / 2;

            float edgeLength = (targetX - sourceX) - settings.Workflow3DSettings.TaskDimensions.x;
            float edgeHeigth = (sourceY - targetY);

            var sourcePort = new Vector3(0,0,0);
            var targetPort = new Vector3(0,0,0);

            var frontLinePositions = new Vector3[]
            {
                new Vector3(-edgeLength /2 , edgeHeigth / 2, 0),
                new Vector3(0, edgeHeigth / 2, 0),
                new Vector3(0, - edgeHeigth / 2, 0),
                new Vector3(edgeLength / 2, -edgeHeigth/2 ,  0)
            };

            var backLinePositions = new Vector3[]
            {
                new Vector3(edgeLength /2 , edgeHeigth / 2, 0),
                new Vector3(0, edgeHeigth / 2, 0),
                new Vector3(0, - edgeHeigth / 2, 0),
                new Vector3(-edgeLength / 2, -edgeHeigth/2 ,  0)
            };

            var edgeObject = CreateEdgeObject(frontLinePositions, backLinePositions);

            edgeObject.transform.SetParent(workflowObject.transform);
            edgeObject.transform.SetLocalPositionAndRotation(new Vector3(xPos, yPos, 0), Quaternion.identity);
        }

        return workflowObject;
    }
    private GameObject CreateTaskObject(TaskNode taskNode)
    {
    //Gameobject
        var cuboidObject = new GameObject(taskNode.Id.ToString()) {
            layer = 5
        };

        //meshfilter
        var meshFilter = cuboidObject.AddComponent<MeshFilter>();
        meshFilter.mesh = CreateCuboidMesh(settings.Workflow3DSettings.TaskDimensions);

        //meshRenderer
        var meshRenderer = cuboidObject.AddComponent<MeshRenderer>();
        meshRenderer.material = settings.Workflow3DSettings.TaskMaterial;

        //Collider
        var collider = cuboidObject.AddComponent<BoxCollider>();

        //Button
        var taskButton3D = cuboidObject.AddComponent<TaskButton3D>();
        taskButton3D.application = application;
        taskButton3D.ID = taskNode.Id;
        taskButton3D.leftRayInteractor = application.leftRayInteractor;
        taskButton3D.rightRayInteractor = application.rightRayInteractor;
        taskButton3D.AddListener(taskButton3D.onClicK);

        var textObject = CreateTextObject(application.TaskPool[taskNode.Id].Type.ToString());
        textObject.transform.SetParent(cuboidObject.transform);
        textObject.transform.position = new Vector3(0, 0, - (settings.Workflow3DSettings.TaskDimensions.z / 2) - 0.001f);

        return cuboidObject;
    }
    private GameObject CreateTextObject(string text)
    {
    //GameObject
        var textObject = new GameObject("Text")
        {
            layer = 5
        };

        //RectTansform
        var tranform = textObject.AddComponent<RectTransform>();
        tranform.sizeDelta = new Vector2(settings.Workflow3DSettings.TaskDimensions.x - settings.Workflow3DSettings.EdgeDimensions.z, settings.Workflow3DSettings.TaskDimensions.y);

        //TextMesh
        var textMesh = textObject.AddComponent<TextMeshPro>();
        textMesh.text = text;
        textMesh.color = settings.Workflow3DSettings.TextColor;
        textMesh.fontSize = settings.Workflow3DSettings.FontSize;
        textMesh.fontStyle = settings.FontStyle;

        textMesh.verticalAlignment = VerticalAlignmentOptions.Middle;
        textMesh.horizontalAlignment = HorizontalAlignmentOptions.Center;

        textMesh.enableWordWrapping = true;

        return textObject;
    }
    private GameObject CreateEdgeObject(Vector3[] frontPositions, Vector3[] backPositions)
    {
        //GameObjects
        var edgeObject = new GameObject("Edge")
        {
            layer = 5
        };
        var frontLineObject = new GameObject("Line")
        {
            layer = 5
        };
        var backLineObject = new GameObject("Line")
        {
            layer = 5
        };
                    
        //Transform        
        frontLineObject.transform.SetParent(edgeObject.transform);
        backLineObject.transform.SetParent(edgeObject.transform);
        frontLineObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
        backLineObject.transform.SetLocalPositionAndRotation(new Vector3(0,0,0), Quaternion.Euler(new Vector3(0, 180, 0)));

        //FrontLineObject LineRenderer
        var frontLineRenderer = frontLineObject.AddComponent<LineRenderer>();
        frontLineRenderer.positionCount = 4;
        frontLineRenderer.SetPositions(frontPositions);
        frontLineRenderer.alignment = LineAlignment.TransformZ;
        frontLineRenderer.useWorldSpace = false;
        frontLineRenderer.material = settings.Workflow3DSettings.EdgeMaterial;

        AnimationCurve frontCurve = new();
        frontCurve.AddKey(0, settings.Workflow3DSettings.EdgeDimensions.z);
        frontCurve.AddKey(1, settings.Workflow3DSettings.EdgeDimensions.z);
        frontLineRenderer.widthCurve = frontCurve;
        //frontLineRenderer.numCapVertices = 5;
        //frontLineRenderer.numCornerVertices = 5;

        //backLineObject LineRenderer
        var backLineRenderer = backLineObject.AddComponent<LineRenderer>();
        backLineRenderer.positionCount = 4;
        backLineRenderer.SetPositions(backPositions);
        backLineRenderer.alignment = LineAlignment.TransformZ;
        backLineRenderer.useWorldSpace = false;
        backLineRenderer.material = settings.Workflow3DSettings.EdgeMaterial;

        AnimationCurve backCurve = new();
        backCurve.AddKey(0, settings.Workflow3DSettings.EdgeDimensions.z);
        backCurve.AddKey(1, settings.Workflow3DSettings.EdgeDimensions.z);
        backLineRenderer.widthCurve = backCurve;
        backLineRenderer.numCapVertices = 5;
        backLineRenderer.numCornerVertices = 5;

        return edgeObject;
    }

    //----------------------------------------------------------------
    //Mesh generation
    //----------------------------------------------------------------

    private Mesh CreateParallelSquarePlaneMesh(float dimension)
    {
        var mesh = new Mesh()
        {
            name = "ParallelPlaneMesh"
        };

        float deltaWidth = dimension / 2;
        float deltaHeight = dimension / 2;
        float deltaDepth = dimension / 2;

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

        var a = 0;
        var b = 1;
        var c = 2;
        var d = 3;
        var e = 4;
        var f = 5;
        var g = 6;
        var h = 7;

        mesh.vertices = new Vector3[] { A, B, C, D, E, F, G, H };
        mesh.triangles = new int[]
        {
                //Front Face
                a, d, b,
                d, c, b,
                //Back Face
                f, g, e,
                g, h, e,
        };
        mesh.normals = new Vector3[]
        {
                //Front
                Vector3.back, Vector3.back, Vector3.back, Vector3.back,
                //Back
                Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,
        };

        return mesh;
    }
    private Mesh CreateUMesh(float dimension)
    {
        var mesh = new Mesh()
        {
            name = "UMesh"
        };

        float deltaWidth = dimension / 2;
        float deltaHeight = dimension / 2;
        float deltaDepth = dimension / 2;

        //Back
        var E = new Vector3(-deltaWidth, -deltaHeight, deltaDepth);
        var F = new Vector3(deltaWidth, -deltaHeight, deltaDepth);
        var G = new Vector3(deltaWidth, deltaHeight, deltaDepth);
        var H = new Vector3(-deltaWidth, deltaHeight, deltaDepth);

        //Left
        var Q = E;
        var R = new Vector3(-deltaWidth, -deltaHeight, -deltaDepth); ;
        var S = new Vector3(-deltaWidth, deltaHeight, -deltaDepth); ;
        var T = H;
        //Rigth
        var U = new Vector3(deltaWidth, -deltaHeight, -deltaDepth); ;
        var V = F;
        var W = G;
        var X = new Vector3(deltaWidth, deltaHeight, -deltaDepth); ;

        var e = 0;
        var f = 1;
        var g = 2;
        var h = 3;
        var q = 4;
        var r = 5;
        var s = 6;
        var t = 7;
        var u = 8;
        var v = 9;
        var w = 10;
        var x = 11;


        mesh.vertices = new Vector3[] { E, F, G, H, Q, R, S, T, U, V, W, X };
        mesh.triangles = new int[]
        {
                //Back Face
                f, g, e,
                g, h, e,
                //LeftFace
                q, t, r,
                t, s, r,
                //RightFace
                u, x, v,
                x, w, v
        };
        mesh.normals = new Vector3[]
        {
                //Back
                Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,
                //Left
                Vector3.left, Vector3.left, Vector3.left, Vector3.left,
                //Rigth
                Vector3.right, Vector3.right, Vector3.right, Vector3.right
        };

        return mesh;
    }
    private Mesh CreateTubeMesh(Vector2 dimensions)
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
        //Back
        var E = new Vector3(-deltaWidth, -deltaHeight, deltaDepth);
        var F = new Vector3(deltaWidth, -deltaHeight, deltaDepth);
        var G = new Vector3(deltaWidth, deltaHeight, deltaDepth);
        var H = new Vector3(-deltaWidth, deltaHeight, deltaDepth);

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

        var q = 8;
        var r = 9;
        var s = 10;
        var t = 11;
        var u = 12;
        var v = 13;
        var w = 14;
        var x = 15;

        mesh.vertices = new Vector3[] { A, B, C, D, E, F, G, H, Q, R, S, T, U, V, W, X };
        mesh.triangles = new int[]
        {
                //Front Face
                a, d, b,
                d, c, b,
                //Back Face
                f, g, e,
                g, h, e,
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
                //Left
                Vector3.left, Vector3.left, Vector3.left, Vector3.left,
                //Rigth
                Vector3.right, Vector3.right, Vector3.right, Vector3.right
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
        //Rigth
        var U = new Vector3(deltaWidth, -deltaHeight, -deltaDepth);
        var V = F;
        var W = G;
        var X = J;

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
        int u = 12;
        int v = 13;
        int w = 14;
        int x = 15;

        mesh.vertices = new Vector3[] { E, F, G, H, I, J, K, L, Q, R, S, T, U, V, W, X };
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
                //RightFace
                u, x, v,
                x, w, v
        };
        mesh.normals = new Vector3[]
        {
                //Back
                Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,
                //Top
                Vector3.up, Vector3.up, Vector3.up, Vector3.up,
                //Left
                Vector3.left, Vector3.left, Vector3.left, Vector3.left,
                //Rigth
                Vector3.right, Vector3.right, Vector3.right, Vector3.right
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
}