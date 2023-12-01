using Assets.Scripts.UI.Common;
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

        public GameObject MainCamera;

        public XRRayInteractor leftRayInteractor;
        public XRRayInteractor rightRayInteractor;


        void Awake()
        {
            DataSetup();
            MainCameraSetuo();
            RayInteractorSetup();
            DragButtonSetup();
        }

        private void MainCameraSetuo()
        {
            MainCamera = GameObject.Find("XR Origin/Camera Offset/Main Camera");
        }
        
        private void RayInteractorSetup()
        {
            leftRayInteractor = GameObject.Find("XR Origin").transform.Find("Camera Offset/LeftHand Controller").GetComponent<XRRayInteractor>();
            rightRayInteractor = GameObject.Find("XR Origin").transform.Find("Camera Offset/RightHand Controller").GetComponent<XRRayInteractor>();
        }

        private void DataSetup()
        {
            _import = new(ScenarioPath);
            Settings = new();
            ResourcePool = _import.ResourcePool;
            DataPool = _import.DataPool;
            WorkflowPool = _import.WorkflowPool;
            TaskPool = _import.TaskPool;
            Graph = _import.Graph;
        }

        private void DragButtonSetup()
        {
            var ResourcePoolObj = GameObject.Find("ResourcePoolUI");
            var DataPoolObj = GameObject.Find("DataPoolUI");
            var WorkflowPoolObj = GameObject.Find("WorkflowPoolUI");

            var dragButton = ResourcePoolObj.GetComponentInChildren<DragButton>();
            dragButton.Obj = ResourcePoolObj;

            dragButton = DataPoolObj.GetComponentInChildren<DragButton>();
            dragButton.Obj = DataPoolObj;

            dragButton = WorkflowPoolObj.GetComponentInChildren<DragButton>();
            dragButton.Obj = WorkflowPoolObj;
        }

        public Vector3 GetInFrontOfCameraPosition(float distance, float heigth)
        {
            var orientation = MainCamera.transform.forward;
            var direction = new Vector3(orientation.x, 0 , orientation.z);
            direction = Vector3.Normalize(direction) * distance;
            
            var position = MainCamera.transform.position + direction;
            position.y = heigth;
            return position;
        }

        public Vector3 GetCameraOrientation()
        {
            return new Vector3(0,MainCamera.transform.rotation.eulerAngles.y, 0);
        }
    }
}
