using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UI.Settings.Layout3D
{
    public class Layout3DSettings
    {
        public float LayerSpacing { get; set; } = 1f;
        public Vector3 TaskDimensions { get; set; } = new Vector3(0.2f, 0.1f, 0.05f);

        public Vector3 EdgeDimensions { get; set; } = new Vector3(0.1f, 0.2f, 0.015f);

        public Material TaskNormalMaterial = Resources.Load <Material>("Materials/Workflow3D/Task_Normal");
        public Material TaskHighlightedMaterial = Resources.Load<Material>("Materials/Workflow3D/Task_Highlighted");
        public Material TaskPressedMaterial = Resources.Load <Material>("Materials/Workflow3D/Task_Pressed");


        public Material EdgeOutlineMaterial = Resources.Load<Material>("Materials/Workflow3D/Edge_Black");
        public Material EdgeFillMaterial = Resources.Load<Material>("Materials/Workflow3D/Edge_White");

        public float FontSize = 0.2f;
        public Color TextColor { get; set; } = Color.white;
    }
}
