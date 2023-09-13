using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Msagl;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.GraphmapsWithMesh;
using Microsoft.Msagl.Layout.MDS;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Core.Layout;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


        var graph = new GeometryGraph();
        var settings = new SugiyamaLayoutSettings();
        settings.
        
        var layout = new LayeredLayout(graph, settings);

        graph.Nodes.Add(new Node(CurveFactory.CreateRectangle(20, 20, new Point())));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
