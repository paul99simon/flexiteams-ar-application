
namespace Assets.Scripts.UI.Settings.ResourceUI
{
    public class ResourceUISettings
    {
        private const float Byte = 255;

        public float Spacing { get; set; } = 20f;

        public PortraitSettings PortraitSettings { get; set; } = new();
        public SkillsSettings SkillsSettings { get; set; } = new();
        public TraitsSettings TraitsSettings { get; set; } = new();
    }
}
