using Assets.Scripts.Application;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts.UI.Common
{
    class DragButton : Button
    {
        private VR_AR_Application _application;

        public GameObject Obj;
        private float movementSpeed = 0.01f;
        private float rotationSpeed = 1f;

        private bool prevPressed = false;

        public void Update()
        {
            DeSelect();

            if (this.IsPressed() & prevPressed)
            {
                Move();
                Rotate();
            }

            if ((!prevPressed & IsPressed()) | (prevPressed & !IsPressed()))
            {
                ToggleCharacterMovement();
            }

            prevPressed = this.IsPressed();
        }
        
        private void DeSelect()
        {
            if (currentSelectionState == SelectionState.Selected) EventSystem.current.SetSelectedGameObject(null);
        }

        private void ToggleCharacterMovement()
        {
            var moveProvider = GameObject.Find("XR Origin/Camera Offset/LeftHand Controller").GetComponent<ActionBasedContinuousMoveProvider>();
            moveProvider.enabled = !moveProvider.enabled;

            var turnProvder = GameObject.Find("XR Origin/Camera Offset/RightHand Controller").GetComponent<ActionBasedContinuousTurnProvider>();
            turnProvder.enabled = !turnProvder.enabled;
        }

        private void Move()
        {
            var leftControllers = new List<UnityEngine.XR.InputDevice>();
            var lcharachteristic = UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;

            var rightControllers = new List<UnityEngine.XR.InputDevice>();

            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(lcharachteristic, leftControllers);

            if (leftControllers.Count == 1)
            {
                if (leftControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 position)) {

                    var translate = new Vector3(position.x, 0 , position.y);
                    translate = movementSpeed * translate;
                    Obj.transform.position += Obj.transform.TransformDirection(translate);
                }
            }
        }

        private void Rotate()
        {
            var rightControllers = new List<UnityEngine.XR.InputDevice>();
            var rcharachteristic = UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;

            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(rcharachteristic, rightControllers);

            if (rightControllers.Count == 1)
            {
                if (rightControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 position))
                {
                    if (position.x == 0) return;
                    float direction = Mathf.Sign(position.x);

                    var currentRoation = Obj.transform.localRotation.eulerAngles;
                    var delta = new Vector3(0, direction, 0) * rotationSpeed;
                    Obj.transform.localRotation = Quaternion.Euler(currentRoation + delta);
                }
            }
        }

    }
}
