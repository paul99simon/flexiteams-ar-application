using Assets.Scripts.UI.Settings.ResourceUI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UISettings
{
    //Constants
    private const float Byte = 255;

   
    //Settings
    public TitleBarSettings TitleBarSettings { get; set; } = new();
    public ResourceUISettings ResourceUISettings { get; set; } = new();
    public LanguageSettings Language { get; set; } = new();

    //Sprites
    public Sprite BackgroundSprite { get; set; } = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
    public Sprite ScrollbarSprite { get; set; } = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
    public Sprite HandleSprite { get; set; } = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
    
    //Color
    public Color BackgroundColor = new(150f / Byte, 150f / Byte, 150f / Byte, 100f / Byte);
    public Color PanelBackgroundColor { get; set; } = new Color(1, 1, 1, 200f / Byte);
    public Color ScrollbarColor { get; set; } = new Color(200f / Byte, 200f / Byte, 200 / Byte, Byte);
    public Color HandleColor { get; set; } = Color.white;
    public Color NormalColor { get; set; } = new Color(1, 1, 1, 200f / Byte);
    public Color HiglightedColor { get; set; } = new Color(55f / Byte, 55f / Byte, 55f / Byte);
    public Color PressedColor { get; set; } = new Color(200f / Byte, 200f / Byte, 200f / Byte);
    public Color TextColor { get; set; } = Color.black;


    //Fonts
    public TMP_FontAsset TMP_FontAsset { get; set; } = Resources.Load("LiberationSans SDF", typeof(TMP_FontAsset)) as TMP_FontAsset;
    public FontStyles FontStyle { get; set; } = FontStyles.Normal;
    public float FontSize { get; set; } = 18;
}