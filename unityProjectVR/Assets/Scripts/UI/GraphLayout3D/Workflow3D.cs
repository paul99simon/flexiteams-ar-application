using Assets.Scripts.Application;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.Graph.Nodes;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Layout.Layered;
using System;
using UnityEngine;

namespace Assets.Scripts.UI.Workflow3DUI
{
    public class Workflow3D
    {
        public VR_AR_Application Application { get; set; }
        public WorkflowId Id { get; set; }
        public Layer3D Layer { get; set; }
        public UISettings Settings { get; set; }

        public GeometryGraph geometryGraph;
        private SugiyamaLayoutSettings sugiyamaLayoutSettings;
        
        private const double meterToMillimeter = 1000;

        public void ChangeLayer(Layer3D layer)
        {
            Layer.Remove(this);
            layer.Add(this);
            Layer = layer;
            
            Layer.Layout.CalculateLayout();
            Layer.Layout.Draw();
        }

        public void CalculateLayout()
        {
            geometryGraph = new();
            sugiyamaLayoutSettings = new SugiyamaLayoutSettings();  

            //Layout variables
            double width = Convert.ToDouble(Settings.Layout3D.Workflow.TaskDimensions.y * meterToMillimeter);
            //double heigth = Convert.ToDouble(Settings.Layout3D.Workflow.TaskDimensions.x * meterToMillimeter);
            double layerSeperation = Convert.ToDouble(Settings.Layout3D.Workflow.EdgeDimensions.x * meterToMillimeter);
            double nodeSeperation = Convert.ToDouble(Settings.Layout3D.Workflow.EdgeDimensions.y * meterToMillimeter);

            WorkflowNode wNode = Application.Graph.FindNode(Id);

            //Graph-Data
            var nodes = Application.Graph.GetTaskNodes(wNode);

            /*nodes*/
            nodes.ForEach(u =>
            {
                double duration = (Application.TaskPool[u.Id].end - Application.TaskPool[u.Id].begin).TotalMinutes;
                double heigth = duration / 100 * meterToMillimeter;
                var node = new Microsoft.Msagl.Core.Layout.Node(CurveFactory.CreateRectangle(width, heigth, new Point(200,0)), u);
                
                geometryGraph.Nodes.Add(node);
            });

            /*edges*/
            nodes.ForEach(u =>
            {
                var nextTaskNodes = Application.Graph.GetNextTasks(u);
                nextTaskNodes.ForEach(v => {
                    geometryGraph.Edges.Add(new Edge(geometryGraph.FindNodeByUserData(u), geometryGraph.FindNodeByUserData(v)));
                });
            });

            /*Up Down Vertical Constraints*/
            var longestPath = Application.Graph.GetLongestDurationPath(wNode, Application.TaskPool);
            
            for (int i = 1; i < longestPath.Count; i++)
            {
                sugiyamaLayoutSettings.AddUpDownVerticalConstraint(geometryGraph.FindNodeByUserData(longestPath[i - 1]), geometryGraph.FindNodeByUserData(longestPath[i]));
            }

            sugiyamaLayoutSettings.LayerSeparation = layerSeperation;
            sugiyamaLayoutSettings.NodeSeparation = nodeSeperation;

            //Layered Layout
            LayeredLayout layout = new(geometryGraph, sugiyamaLayoutSettings);
            layout.Run();

            //PlaneTransformation
            double startNodeLeft = geometryGraph.FindNodeByUserData(Application.Graph.FindNode(wNode.StartNodeId)).BoundingBox.Left;
            double startNodeTop = geometryGraph.FindNodeByUserData(Application.Graph.FindNode(wNode.StartNodeId)).BoundingBox.Top;

            var pT = PlaneTransformation.Rotation(1.57079633) * new PlaneTransformation(1, 0, -startNodeLeft, 0, 1, -startNodeTop);
            geometryGraph.Transform(pT);
        }

        public float GetHeight()
        {
            return MaxHeight() - MinHeight();
        }

        private float MinHeight()
        {
            double min = double.MaxValue;

            foreach (var node in geometryGraph.Nodes)
            {
                if (node.BoundingBox.Bottom < min)
                {
                    min = node.BoundingBox.Bottom;
                }
            }

            return Convert.ToSingle( min / meterToMillimeter);
        }

        private float MaxHeight()
        {
            double max = double.MinValue;

            foreach (var node in geometryGraph.Nodes)
            {
                if (node.BoundingBox.Top > max)
                {
                    max = node.BoundingBox.Top;
                }
            }

            return Convert.ToSingle(max / meterToMillimeter);
        }

        public float GetWidth()
        {
            return 0;
        }
    }
}
