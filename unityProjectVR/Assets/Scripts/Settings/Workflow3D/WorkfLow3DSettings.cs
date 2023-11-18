using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI.Settings.Workflow3D
{
    public class WorkfLow3DSettings
    {
        public float WorkflowSpacing { get; set; } = 1f;
        public Vector3 TaskDimensions { get; set; } = new Vector3(0.2f, 0.1f, 0.05f);

        public Vector3 EdgeDimensions { get; set; } = new Vector3(0.1f, 0.2f, 0.015f);

        public Material TaskMaterial = Resources.Load <Material>("Materials/Workflow3D/Task_Blue");
        public Material EdgeMaterial = Resources.Load<Material>("Materials/Workflow3D/Edge_Black");

        public float FontSize = 0.2f;
        public Color TextColor { get; set; } = Color.white;
    }
}
