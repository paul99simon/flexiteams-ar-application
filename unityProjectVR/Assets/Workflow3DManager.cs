using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Diretor;
using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Graph.Nodes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.TerrainTools;
using UnityEngine;

public class Workflow3DManager : MonoBehaviour
{
    [Header("Layout")]
    [SerializeField]
    private Vector3 taskSize;
    [SerializeField]
    private Vector2 edgeSize;

    [Header("Csv Path")]
    [SerializeField]
    private string path;

    [Header("Font")]
    [SerializeField]
    private TMP_FontAsset fontAsset;
    [SerializeField]
    private FontStyle fontStyle;
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
        
        var workflowObject = new GameObject("Workflow");
        var transform = workflowObject.GetComponent<Transform>();
        transform.position = Vector3.zero;
        
        GameObject taskObject = CreateTaskObject(new Vector3(0,2,0), Vector3.zero);

        workflowObject.transform.SetParent(taskObject.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject CreateWorkflowObject(WorkflowNode wNode)
    {
        var workflowObject = new GameObject(wNode.Workflow.Type.Get);



        return workflowObject;

    }

    private GameObject CreateTaskObject(Vector3 position, Vector3 rotation)
    {
        //Gameobject
        var cuboidObject = new GameObject("Task");
        
        //Transform
        var transform = cuboidObject.GetComponent<Transform>();
        cuboidObject.transform.position = position;
        cuboidObject.transform.rotation = Quaternion.Euler(rotation);

        //meshfilter
        var meshFilter = cuboidObject.AddComponent<MeshFilter>();
        meshFilter.mesh = CreateCuboidMesh(1, 1, 1);

        //meshRenderer
        var meshRenderer = cuboidObject.AddComponent<MeshRenderer>();
        meshRenderer.material = taskMaterial;
      

        var textObject = CreateTextObject("Test", Color.black, 1);
        textObject.transform.SetParent(cuboidObject.transform, false);

        return cuboidObject;
    }
    private Mesh CreateCuboidMesh(float width, float height, float depth)
    {
        var mesh = new Mesh()
        {
            name = "CuboidMesh"
        };

        float deltaWidth = width / 2;
        float deltaHeight = height / 2;
        float deltaDepth = depth / 2;

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
    private GameObject CreateTextObject(string text, Color color, float fontSize)
    {
        //GameObject
        var textObject = new GameObject("Text");

        //TextMesh
        var textMesh = textObject.AddComponent<TextMeshPro>();
        textMesh.text = text;
        textMesh.color = color;
        textMesh.fontSize = fontSize;
        textMesh.verticalAlignment = VerticalAlignmentOptions.Middle;
        textMesh.horizontalAlignment = HorizontalAlignmentOptions.Center;

        return textObject;
    }
}
