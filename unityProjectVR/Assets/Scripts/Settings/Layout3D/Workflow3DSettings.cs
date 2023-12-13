using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Settings.Layout3D
{
    public class Workflow3DSettings
    {
        public Vector3 TaskDimensions { get; set; } = new Vector3(0.2f, 0.1f, 0.05f);

        public float FrameWidth { get; set; } = 0.005f;

        public Vector3 EdgeDimensions { get; set; } = new Vector3(0.1f, 0.2f, 0.015f);

        public Material TaskNormalMaterial = Resources.Load<Material>("Materials/Layout 3D/Task_Normal");
        public Material TaskHighlightedMaterial = Resources.Load<Material>("Materials/Layout 3D/Task_Highlighted");
        public Material TaskPressedMaterial = Resources.Load<Material>("Materials/Layout 3D/Task_Pressed");
        public Material FrameMaterial = Resources.Load<Material>("Materials/Layout 3D/Frame_Black");

        public Material EdgeOutlineMaterial = Resources.Load<Material>("Materials/Layout 3D/Edge_Black");
        public Material EdgeFillMaterial = Resources.Load<Material>("Materials/Layout 3D/Edge_White");

        public float FontSize { get; set; } = 0.2f;
        public Color TextColor { get; set; } = Color.white;
    }
}
