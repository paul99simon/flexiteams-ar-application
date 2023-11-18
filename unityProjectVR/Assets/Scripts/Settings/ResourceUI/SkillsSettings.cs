using UnityEditor;
using UnityEngine;


namespace Assets.Scripts.UI.Settings.ResourceUI
{
    public class SkillsSettings
    {
        //Layout
        public float SkillHeight { get; set; } = 25;

        //Sprite
        public Sprite Checkmark { get; set; } = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Checkmark.psd");

        //Color
    }
}