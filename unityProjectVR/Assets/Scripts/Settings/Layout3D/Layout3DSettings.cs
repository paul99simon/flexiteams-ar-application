using Assets.Scripts.Settings.Layout3D;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UI.Settings.Layout3D
{
    public class Layout3DSettings
    {
        public float Spacing { get; set; } = 1f;

        public Layer3DSettings Layer { get; set; } = new();
        public Workflow3DSettings Workflow { get; set; } = new();
        public TimeGridSettings TimeGrid { get; set; } = new();

    }
}
