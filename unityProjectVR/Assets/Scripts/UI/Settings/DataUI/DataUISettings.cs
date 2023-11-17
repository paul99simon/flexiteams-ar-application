using Assets.Scripts.UI.Settings.ResourceUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UI.Settings.DataUI
{
    public class DataUISettings
    {
        private const float Byte = 255;

        public float Spacing { get; set; } = 20f;

        public IconSettings IconSettings { get; set; } = new();
        public SkillsSettings SkillsSettings { get; set; } = new();
        public TraitsSettings TraitsSettings { get; set; } = new();
    }
}
