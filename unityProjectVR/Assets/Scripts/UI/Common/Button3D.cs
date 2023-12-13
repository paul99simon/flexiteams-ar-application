using Assets.Scripts.Application;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts.UI.Workflow3DUI
{
    public class Button3D : MonoBehaviour
    {
        private Action onClick;

        public XRRayInteractor leftRayInteractor;
        public XRRayInteractor rightRayInteractor;

        private bool leftSelected = false;
        private bool rightSelected = false;

        private bool leftTriggerPressPrev = false;
        private bool leftTriggerPressCurrent = false;

        private bool rightTriggerPressPrev = false;
        private bool rightTriggerPressCurrent = false;

        public VR_AR_Application application;

        public void AddListener(Action method)
        {
            onClick = new Action(method);
        }

        public void Update()
        {
            GetData();
            ToggleSelection();
            Evaluate();
            CleanUp();
        }

        private void GetData()
        {
            if (leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit lhit))
            {
                if (lhit.collider == GetComponent<Collider>()) leftSelected = true;
            }

            if (rightRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit rhit))
            {
                if (rhit.collider == GetComponent<Collider>()) rightSelected = true;
            }

            var leftControllers = new List<UnityEngine.XR.InputDevice>();
            var lcharachteristic = UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;

            var rightControllers = new List<UnityEngine.XR.InputDevice>();
            var rcharachteristic = UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;

            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(lcharachteristic, leftControllers);
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(rcharachteristic, rightControllers);

            if (leftControllers.Count == 1)
            {
                leftControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out leftTriggerPressCurrent);
            }

            if (rightControllers.Count == 1)
            {
                rightControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out rightTriggerPressCurrent);
            }
        }
        private void ToggleSelection()
        {
            if (rightSelected | leftSelected)
            {
                GetComponent<Renderer>().material = application.Settings.Layout3D.Workflow.TaskHighlightedMaterial;
            }

            if (!rightSelected & !leftSelected)
            {
                GetComponent<Renderer>().material = application.Settings.Layout3D.Workflow.TaskNormalMaterial;
            }
        }
        private void Evaluate()
        {
            if (rightSelected & !rightTriggerPressPrev & rightTriggerPressCurrent)
            {
                onClick();
            }
            if (leftSelected & !leftTriggerPressPrev & leftTriggerPressCurrent)
            {
                onClick();
            }
        }
        private void CleanUp()
        {
            leftSelected = false;
            rightSelected = false;

            leftTriggerPressPrev = leftTriggerPressCurrent;
            leftTriggerPressCurrent = false;

            rightTriggerPressPrev = rightTriggerPressCurrent;
            rightTriggerPressCurrent = false;
        }
    }
}