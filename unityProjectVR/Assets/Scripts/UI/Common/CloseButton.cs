using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Common
{
    class CloseButton : Button
    {
        public GameObject Obj { get; set; }

        public void OnClick()
        {
            DeSelect();
            Destroy(Obj);
        }

        private void DeSelect()
        {
            if (currentSelectionState == SelectionState.Selected) EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
