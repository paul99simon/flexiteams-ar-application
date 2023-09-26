using TMPro;
using UnityEngine;

public class HeaderSettings
{
    private const float Byte = 255;

    public Color BackgroundColor { get; set; } = new Color(1,1,1,200f/Byte);

    public Color NormalColor { get; set; } = new Color(1, 1, 1, 200f/Byte);
    public Color HiglightedColor { get; set; } = new Color(55f/Byte, 55f/Byte, 55f/Byte);
    public Color PressedColor { get; set; } = new Color(200f/Byte, 200/Byte, 200/Byte);

    public Color TextColor { get; set; } = Color.black;

    //Font
    public TMP_FontAsset TMP_FontAsset { get; set; } = Resources.Load("LiberationSans SDF", typeof(TMP_FontAsset)) as TMP_FontAsset;
    public FontStyles fontStyles { get; set; } = FontStyles.Normal;
    public float fontSize { get; set; } = 36;
}