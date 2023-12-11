using Assets.Scripts.Application;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.FlexiTeamsGraph;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Layout.Layered;
using UnityEngine;

namespace Assets.Scripts.UI.Workflow3DUI
{
    public class Workflow3D : MonoBehaviour
    {
        public VR_AR_Application Application;
        public WorkflowId WorkflowId;

        public Layer3D Layer;

        public GameObject WorkflowObject;

        public UISettings Settings;

        private GeometryGraph geometryGraph;
        private SugiyamaLayoutSettings sugiyamaLayoutSettings;
        public float XOffset;
        public float YOffset;

        public void Start()
        {
            
        }

        public float GetHeight()
        {
            return 0;
        }

        public float GetWidth()
        {
            return 0;
        }

        public void Delete()
        {
            
        }

        public void Draw()
        {

        }
    }
}
