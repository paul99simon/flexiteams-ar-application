using Assets.Scripts.Application;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Workflow3DUI
{
    public class Layer3D : MonoBehaviour
    {
        public VR_AR_Application Application { get; set; }
        public GameObject Layer{get; set;}
        public Layout3D Layout {get; set;}
        public Dictionary<WorkflowId, Workflow3D> Workflows = new();
        public float ZOffset {get; set;}

        public void Add(WorkflowId wId)
        {
            Workflows.Add(wId, null);
            Draw();
        }

        public void Remove(WorkflowId wId)
        {
            Workflows.Remove(wId);
            if( Workflows.Count == 0 ) Delete();
            else Draw();
        }

        public void Draw()
        {
            
        }

        public void Delete()
        {

        }
    }
}
