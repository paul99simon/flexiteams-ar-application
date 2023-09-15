using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Diretor;
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

public class Workflow3DManager : MonoBehaviour
{
    [Header("Layout")]
    [Space(5)]

    [SerializeField]
    private GameObject workflow3D;

    [SerializeField]
    [Tooltip("This variable represents the spacing of the workflow objects")]
    private float workflowSpacing;

    [SerializeField]
    [Tooltip("This variable represents the dimensions of the task nodes (x = width, y = heigth , z = depth )")]
    private Vector3 taskDimensions;
    
    [SerializeField]
    [Tooltip("This variable represents the spacing of the task Nodes in their respective Workflow (x = horizontal spacing, y = vertival spacing)")]
    private Vector2 taskSpacing;

    [SerializeField]
    [Tooltip("this variable represents the Thickness of edges")]
    private float edgeWidth;

    [Space(5)]
    [Header("Csv Path")]
    [Space(5)]
    [SerializeField]
    private string path;

    [Space(5)]
    [Header("Font")]
    [Space(5)]

    [SerializeField]
    private TMP_FontAsset fontAsset;
    [SerializeField]
    private FontStyles fontStyles;
    [SerializeField]
    private float fontSize;
    [SerializeField]
    private Color vertexColor;
   
    [Header("Materials")]
    [SerializeField]
    private Material taskMaterial;
    [SerializeField]
    private Material edgeMaterial;

    private const double meterToMillimeter = 1000;
    private readonly AdjListsGraph _graph = new();

    // Start is called before the first frame update
    void Start()
    {
        BasicGraphDirector.ConstructFromCsv(path, _graph, new BasicWorkflowBuilder(), new BasicTaskBuilder());
        Create3DWorkflowLayout();
        var cornerObject = new GameObject("Corner");
        var meshFilter1 = cornerObject.AddComponent<MeshFilter>();
        var meshRenderer1 = cornerObject.AddComponent<MeshRenderer>();

        var tubeObject = new GameObject("Tube");
        var meshFilter2 = tubeObject.AddComponent<MeshFilter>();
        var meshRenderer2 = tubeObject.AddComponent<MeshRenderer>();

        var uObject = new GameObject("U-Profil");
        var meshFilter3 = uObject.AddComponent<MeshFilter>();
        var meshRenderer3 = uObject.AddComponent<MeshRenderer>();

        var planeObject = new GameObject("Planes");
        var meshFilter4 = planeObject.AddComponent<MeshFilter>();
        var meshRenderer4 = planeObject.AddComponent<MeshRenderer>();

        meshFilter1.mesh = CreateCornerMesh(0.05f);
        meshFilter2.mesh = CreateTubeMesh(new Vector2(0.05f, 1));
        meshFilter3.mesh = CreateUMesh(0.05f);
        meshFilter4.mesh = CreateParallelSquarePlaneMesh(0.05f);
    }

    private void Create3DWorkflowLayout()
    {
        List<WorkflowNode> wNodes = _graph.GetWorkflowNodes();

        //Increment for the workflows position
        float positionincrement = workflowSpacing + taskDimensions.z;
        float startingPosition = - (((wNodes.Count-1) * positionincrement) / 2);
        float position = startingPosition;
        wNodes.ForEach(node =>
        {
            var workflowObject = CreateWorkflowObject(node);
            var transform = workflowObject.GetComponent<Transform>();
            transform.SetParent(workflow3D.transform, false);
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
        double width = Convert.ToDouble(taskDimensions.y * meterToMillimeter);
        double heigth = Convert.ToDouble(taskDimensions.x * meterToMillimeter);
        double layerSeperation = Convert.ToDouble(taskSpacing.x * meterToMillimeter);
        double nodeSeperation = Convert.ToDouble(taskSpacing.y * meterToMillimeter);

        //Graph-Data
        var nodes = _graph.GetTaskNodes(wNode);
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
            var nextTaskNodes = _graph.GetNextTasks(u);
            nextTaskNodes.ForEach(v => {
                graph.Edges.Add(new Edge(graph.FindNodeByUserData(u), graph.FindNodeByUserData(v)));
            });

        });

        //Sugiyama-layout settings
        var settings = new SugiyamaLayoutSettings();

        /*Up Down Vertical Constraints*/
        var longestPath = _graph.GetLongestPath(wNode);
        for (int i = 1; i < longestPath.Count; i++)
        {
            settings.AddUpDownVerticalConstraint(graph.FindNodeByUserData(longestPath[i - 1]), graph.FindNodeByUserData(longestPath[i]));
        }

        /*Add Left Right Constraints*/
        /*var notInLongestPath = new List<TaskNode>();
        nodes.ForEach(u => {
            if (!longestPath.Contains(u))  notInLongestPath.Add(u);
        });
        
        notInLongestPath.ForEach(u => {
            settings.AddLeftRightConstraint( graph.FindNodeByUserData(wNode.StartNode), graph.FindNodeByUserData(u));   
        });*/

        settings.LayerSeparation = layerSeperation;
        settings.NodeSeparation = nodeSeperation;

        //Layered Layout
        LayeredLayout layout = new LayeredLayout(graph, settings);
        layout.Run();

        //PlaneTransformation
        double startNodeLeft = graph.FindNodeByUserData(wNode.StartNode).BoundingBox.Left;
        double startNodeTop = graph.FindNodeByUserData(wNode.StartNode).BoundingBox.Top;

        var pT = PlaneTransformation.Rotation(1.57079633) * new PlaneTransformation(1, 0, -startNodeLeft, 0, 1, -startNodeTop);
        graph.Transform(pT);

        return graph;
    }

    //----------------------------------------------------------------
    //Unity Game Object creation
    //----------------------------------------------------------------
    
    private GameObject CreateWorkflowObject(WorkflowNode wNode)
    {
        var workflowObject = new GameObject(wNode.Workflow.Id.ToString());

        var layout = SugiyamaGraph(wNode);

        var taskNodes = _graph.GetTaskNodes(wNode);

        taskNodes.ForEach(taskNode =>
        {
            var layoutNode = layout.FindNodeByUserData(taskNode);
            float x = RoundAndConvert(layoutNode.BoundingBox.Center.X);
            float y = RoundAndConvert(layoutNode.BoundingBox.Center.Y);

            var taskObject = CreateTaskObject(taskNode);
            taskObject.transform.SetParent(workflowObject.transform, false);
            taskObject.transform.SetLocalPositionAndRotation(new Vector3(x, y, 0), Quaternion.identity);

        });

        foreach(var edge in layout.Edges)
        {
            float sourceX = RoundAndConvert(edge.SourcePort.Location.X);
            float sourceY = RoundAndConvert(edge.SourcePort.Location.Y);
            float targetX = RoundAndConvert(edge.TargetPort.Location.X);
            float targetY = RoundAndConvert(edge.TargetPort.Location.Y);

            float xPos = (sourceX + targetX) / 2;
            float yPos = (sourceY + targetY) / 2;

            float edgeLength = (targetX - sourceX) - taskDimensions.x;
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

            var edgeObject = CreateEdgeObject(edge, frontLinePositions, backLinePositions);

            edgeObject.transform.SetParent(workflowObject.transform, false);
            edgeObject.transform.SetLocalPositionAndRotation(new Vector3(xPos, yPos, 0), Quaternion.identity);
        }

        return workflowObject;
    }
    private GameObject CreateTaskObject(TaskNode taskNode)
    {
        //Gameobject
        var cuboidObject = new GameObject(taskNode.Task.Id.ToString());

        //meshfilter
        var meshFilter = cuboidObject.AddComponent<MeshFilter>();
        meshFilter.mesh = CreateCuboidMesh(taskDimensions);

        //meshRenderer
        var meshRenderer = cuboidObject.AddComponent<MeshRenderer>();
        meshRenderer.material = taskMaterial;

        var textObject = CreateTextObject(taskNode.Task.Type.ToString());
        textObject.transform.SetParent(cuboidObject.transform, false);

        return cuboidObject;
    }
    private GameObject CreateTextObject(string text)
    {
        //GameObject
        var textObject = new GameObject("Text");

        //RectTansform
        var tranform = textObject.AddComponent<RectTransform>();
        tranform.sizeDelta = new Vector2(taskDimensions.x - edgeWidth, taskDimensions.y);

        //TextMesh
        var textMesh = textObject.AddComponent<TextMeshPro>();
        textMesh.text = text;
        textMesh.color = vertexColor;
        textMesh.fontSize = fontSize;
        textMesh.fontStyle = fontStyles;

        textMesh.verticalAlignment = VerticalAlignmentOptions.Middle;
        textMesh.horizontalAlignment = HorizontalAlignmentOptions.Center;

        textMesh.enableWordWrapping = true;

        return textObject;
    }
    private GameObject CreateEdgeObject(Edge edge, Vector3[] frontPositions, Vector3[] backPositions)
    {
        //GameObjects
        var edgeObject = new GameObject("Edge");
        var frontLineObject = new GameObject("Line");
        var backLineObject = new GameObject("Line");

        //Transform        
        frontLineObject.transform.SetParent(edgeObject.transform, false);
        backLineObject.transform.SetParent(edgeObject.transform, false);
        frontLineObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
        backLineObject.transform.SetLocalPositionAndRotation(new Vector3(0,0,0), Quaternion.Euler(new Vector3(0, 180, 0)));

        //FrontLineObject LineRenderer
        var frontLineRenderer = frontLineObject.AddComponent<LineRenderer>();
        frontLineRenderer.positionCount = 4;
        frontLineRenderer.SetPositions(frontPositions);
        frontLineRenderer.alignment = LineAlignment.TransformZ;
        frontLineRenderer.useWorldSpace = false;
        frontLineRenderer.material = edgeMaterial;

        AnimationCurve frontCurve = new AnimationCurve();
        frontCurve.AddKey(0, edgeWidth);
        frontCurve.AddKey(1, edgeWidth);
        frontLineRenderer.widthCurve = frontCurve;
        frontLineRenderer.numCapVertices = 5;
        frontLineRenderer.numCornerVertices = 5;

        //backLineObject LineRenderer
        var backLineRenderer = backLineObject.AddComponent<LineRenderer>();
        backLineRenderer.positionCount = 4;
        backLineRenderer.SetPositions(backPositions);
        backLineRenderer.alignment = LineAlignment.TransformZ;
        backLineRenderer.useWorldSpace = false;
        backLineRenderer.material = edgeMaterial;

        AnimationCurve backCurve = new AnimationCurve();
        backCurve.AddKey(0, edgeWidth);
        backCurve.AddKey(1, edgeWidth);
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