using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Diretor;
using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Graph.Nodes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

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
    private float edgeThickness;

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

    private AdjListsGraph _graph = new AdjListsGraph();

    // Start is called before the first frame update
    void Start()
    {
        BasicGraphDirector.ConstructFromCsv(path, _graph, new BasicWorkflowBuilder(), new BasicTaskBuilder());
        Create3DWorkflowLayout();

        _graph.GetWorkflowNodes().ForEach(n => _graph.GetTaskNodes(n).ForEach(x => Debug.Log(x.Task.Type.Get)));
       
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
            position = position + positionincrement;
        });

    }

    private GameObject CreateWorkflowObject(WorkflowNode wNode)
    {
        var workflowObject = new GameObject(wNode.Workflow.Id.Get);



        return workflowObject;

    }

    private GameObject CreateTaskObject(TaskNode taskNode)
    {
        //Gameobject
        var cuboidObject = new GameObject(taskNode.Task.Id.Get);
        
        //meshfilter
        var meshFilter = cuboidObject.AddComponent<MeshFilter>();
        meshFilter.mesh = CreateCuboidMesh(taskDimensions);

        //meshRenderer
        var meshRenderer = cuboidObject.AddComponent<MeshRenderer>();
        meshRenderer.material = taskMaterial;

        var textObject = CreateTextObject(taskNode.Task.Type.Get);
        textObject.transform.SetParent(cuboidObject.transform, false);

        return cuboidObject;
    }
    private Mesh CreateCuboidMesh(Vector3 dimensions)
    {
        var mesh = new Mesh()
        {
            name = "CuboidMesh"
        };

        float deltaWidth    = dimensions.x / 2;
        float deltaHeight   = dimensions.y / 2;
        float deltaDepth    = dimensions.z / 2;

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
    private GameObject CreateTextObject(string text)
    {
        //GameObject
        var textObject = new GameObject("Text");

        //TextMesh
        var textMesh = textObject.AddComponent<TextMeshPro>();
        textMesh.text = text;
        textMesh.color = vertexColor;
        textMesh.fontSize = fontSize;
        textMesh.fontStyle = fontStyles;

        textMesh.verticalAlignment = VerticalAlignmentOptions.Middle;
        textMesh.horizontalAlignment = HorizontalAlignmentOptions.Center;

        return textObject;
    }
}
