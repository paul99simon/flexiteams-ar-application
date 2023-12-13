using Assets.Scripts.Settings.DataPoolUI;
using Assets.Scripts.Settings.ResourcePoolUISettings;
using Assets.Scripts.UI.Settings;
using Assets.Scripts.UI.Settings.DataUI;
using Assets.Scripts.UI.Settings.Layout3D;
using Assets.Scripts.UI.Settings.ResourceUI;
using Assets.Scripts.UI.Settings.TaskUI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UISettings
{
    //Constants
    private const float Byte = 255;

    //Settings
    public TitleBarSettings TitleBar { get; set; } = new();
    public HeaderSettings Header { get; set; } = new();
    public ResourceUISettings ResourceUI { get; set; } = new();
    public DataUISettings DataUI { get; set; } = new();
    public TaskUISettings TaskUI { get; set; } = new();
    public LanguageSettings Language { get; set; } = new();
    public ResourcePoolUISettings ResourcePoolUI { get; set; } = new();
    public DataPoolUISettings DataPoolUI { get; set; } = new();
    public WorkflowPoolUISettings WorkflowPoolUI { get; set; } = new();
    public Layout3DSettings Layout3D { get; set; } = new();

    //Sprites
    public Sprite BackgroundSprite { get; set; } = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
    public Sprite ScrollbarSprite { get; set; } = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
    public Sprite HandleSprite { get; set; } = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
    
    //Color
    public Color BackgroundColor = new(150f / Byte, 150f / Byte, 150f / Byte, 100f / Byte);
    public Color PanelBackgroundColor { get; set; } = new Color(1, 1, 1, 200f / Byte);
    public Color ScrollbarColor { get; set; } = new Color(200f / Byte, 200f / Byte, 200 / Byte, Byte);
    public Color HandleColor { get; set; } = Color.white;

    public ColorBlock ButtonColors = new()
    {
        normalColor = Color.white,
        selectedColor = Color.white,
        highlightedColor = new Color(200f / Byte, 200f / Byte, 200f / Byte),
        pressedColor = Color.red,
        colorMultiplier = 1
    };

    public Color TextColor { get; set; } = Color.black;


    //Fonts
    public TMP_FontAsset TMP_FontAsset { get; set; } = Resources.Load("LiberationSans SDF", typeof(TMP_FontAsset)) as TMP_FontAsset;
    public FontStyles FontStyle { get; set; } = FontStyles.Normal;
    public float FontSize { get; set; } = 18;
}