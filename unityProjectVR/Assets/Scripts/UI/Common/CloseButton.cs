using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Common
{
    class CloseButton : Button
    {
        public GameObject Obj { get; set; }

        public void OnClick()
        {
            Destroy(Obj);
        }
    }
}
