using TMPro;
using UnityEditor;
using UnityEngine;

public class HeaderSettings
{
    private const float Byte = 255;

    public Sprite BackgroundSprite { get; set; } = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
    public Color BackgroundColor { get; set; } = Color.white;

    public Color NormalColor { get; set; } = new Color(1, 1, 1, 200f/Byte);
    public Color HiglightedColor { get; set; } = new Color(55f/Byte, 55f/Byte, 55f/Byte);
    public Color PressedColor { get; set; } = new Color(200f/Byte, 200/Byte, 200/Byte);

    public Color TextColor { get; set; } = Color.black;

    //Font
    public FontStyles FontStyles { get; set; } = FontStyles.Normal;
    public float FontSize { get; set; } = 36;
    
}