using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

namespace Assets.Scripts.Settings.Layout3D
{
    public class TimeGridSettings
    {
        public float Margin { get; set; } = 0.1f;

        public Material TimeBoxMaterial { get; set; } = Resources.Load<Material>("Materials/Layout 3D/TimeGrid");

        public float FontSize { get; set; } = 0.4f;
        public float Textspacing { get; set; } = 0.05f;
        public Color TextColor { get; set; } = Color.white;
    }
}
