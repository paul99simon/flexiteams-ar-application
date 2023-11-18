using FlexiTeams;
using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Inventory;
using FlexiTeams.IO;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts.Application
{
    public class VR_AR_Application : MonoBehaviour
    {
        public string ScenarioPath;

        private Import _import;

        public ResourcePool ResourcePool;
        public DataPool DataPool;
        public TaskPool TaskPool;
        public WorkflowPool WorkflowPool;
        public AdjListsGraph Graph;

        public UISettings Settings { get; set; }

        public XRRayInteractor leftRayInteractor;
        public XRRayInteractor rightRayInteractor;


        void Awake()
        {
            _import = new(ScenarioPath);
            Settings = new();
            ResourcePool = _import.ResourcePool;
            DataPool = _import.DataPool;
            WorkflowPool = _import.WorkflowPool;
            TaskPool = _import.TaskPool;
            Graph = _import.Graph;

            leftRayInteractor = GameObject.Find("XR Origin").transform.Find("Camera Offset/LeftHand Controller").GetComponent<XRRayInteractor>();
            rightRayInteractor = GameObject.Find("XR Origin").transform.Find("Camera Offset/RightHand Controller").GetComponent<XRRayInteractor>();
        }
    }
}
