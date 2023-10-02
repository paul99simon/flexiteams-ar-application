using TMPro;
using UnityEditor;
using UnityEngine;

public class DataViewSettings
{
    private const float Byte = 255;

    public Sprite BackgroundSprite { get; set; } = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
    public Sprite ScrollbarSprite { get; set; } = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
    public Sprite HandleSprite { get; set; } = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
    public Sprite PortraitSprite { get; set; } = Resources.Load<Sprite>("Images/Icons/portrait_white");

    public Color BackgroundColor { get; set; } = new Color(1, 1, 1, 200f/Byte);
    public Color ScrollbarColor { get; set; } = new Color(200f/Byte, 200f/Byte, 200/Byte, Byte);
    public Color HandleColor { get; set; } = Color.white;

    public Color NormalColor { get; set; } = new Color(1, 1, 1, 200f/Byte);
    public Color HiglightedColor { get; set; } = new Color(55f/Byte, 55f/Byte, 55f/Byte);
    public Color PressedColor { get; set; } = new Color(200f/Byte, 200f/Byte, 200f/Byte);

    public Color TextColor { get; set; } = Color.black;

    //Font
    public TMP_FontAsset TMP_FontAsset { get; set; } = Resources.Load("LiberationSans SDF", typeof(TMP_FontAsset)) as TMP_FontAsset;
    public FontStyles fontStyles { get; set; } = FontStyles.Normal;
    public float fontSize { get; set; } = 18;
}