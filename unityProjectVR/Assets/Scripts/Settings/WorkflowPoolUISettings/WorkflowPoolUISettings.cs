using UnityEngine;

namespace Assets.Scripts.UI.Settings
{
    public class WorkflowPoolUISettings
    {
        public Sprite VisibilityOnSprite { get; set; } = Resources.Load<Sprite>("Images/Icons/visibility_white");
        public Sprite VisibilityOfSprite { get; set; } = Resources.Load<Sprite>("Images/Icons/visibility_of_white");
        public Sprite DeleteSprite { get; set; } = Resources.Load<Sprite>("Images/Icons/delete_white");
    }
}
