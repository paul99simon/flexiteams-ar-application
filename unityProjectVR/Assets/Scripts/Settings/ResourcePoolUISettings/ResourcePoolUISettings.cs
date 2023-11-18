using UnityEngine;

namespace Assets.Scripts.Settings.ResourcePoolUISettings
{
    public class ResourcePoolUISettings
    {
        public Sprite NameSprite { get; set; } = Resources.Load<Sprite>("Images/Icons/name_white");
        public Sprite RoleSprite { get; set; } = Resources.Load<Sprite>("Images/Icons/role_white");
    }
}
