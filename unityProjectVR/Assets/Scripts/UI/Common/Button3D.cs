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

        public void AddListener(Action method)
        {
            onClick = new Action(method);
        }

        public void OnClick()
        {
            onClick();
        }

        public void Update()
        {
            //Test selection
            if(leftRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit lhit))
            {
                if (lhit.collider == GetComponent<Collider>()) leftSelected = true;
            }

            if(rightRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit rhit))
            {
                if (rhit.collider == GetComponent<Collider>()) rightSelected = true;
            }

            var leftControllers = new List<UnityEngine.XR.InputDevice>();
            var lcharachteristic = UnityEngine.XR.InputDeviceCharacteristics.Left;

            var rightControllers = new List<UnityEngine.XR.InputDevice>();
            var rcharachteristic = UnityEngine.XR.InputDeviceCharacteristics.Right;

            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(lcharachteristic, leftControllers);
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(rcharachteristic, rightControllers);

            if (leftControllers.Count == 1 & !leftTriggerPressPrev)
            {
                leftControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out leftTriggerPressCurrent);
            }

            if(rightControllers.Count == 1 & !rightTriggerPressPrev)
            {
                rightControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out rightTriggerPressCurrent);
            }

            if (leftSelected && !leftTriggerPressPrev && leftTriggerPressCurrent)
            {
                OnClick();
            }

            if (rightSelected && !rightTriggerPressPrev && rightTriggerPressCurrent)
            {
                OnClick();
            }

            leftSelected = false;
            rightSelected = false;

            leftTriggerPressPrev = leftTriggerPressCurrent;
            leftTriggerPressCurrent = false;

            rightTriggerPressPrev = leftTriggerPressCurrent;
            rightTriggerPressCurrent = false;
        }
    }
}
