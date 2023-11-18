using Assets.Scripts.Settings.DataPoolUI;
using Assets.Scripts.Settings.ResourcePoolUISettings;
using Assets.Scripts.UI.Settings;
using Assets.Scripts.UI.Settings.DataUI;
using Assets.Scripts.UI.Settings.ResourceUI;
using Assets.Scripts.UI.Settings.TaskUI;
using Assets.Scripts.UI.Settings.Workflow3D;
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
    public HeaderSettings HeaderSettings { get; set; } = new();
    public ResourceUISettings ResourceUISettings { get; set; } = new();
    public DataUISettings DataUISettings { get; set; } = new();
    public TaskUISettings TaskUISettings { get; set; } = new();
    public LanguageSettings Language { get; set; } = new();
    public ResourcePoolUISettings ResourcePoolUISettings { get; set; } = new();
    public DataPoolUISettings DataPoolUISettings { get; set; } = new();
    public WorkflowPoolUISettings WorkflowPoolUISettings { get; set; } = new();
    public WorkfLow3DSettings Workflow3DSettings { get; set; } = new();

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