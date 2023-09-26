using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleBarSettings
{
    private const float _byte = 255;

    public Color BackgroundColor { get; set; } = Color.black;

    public ColorBlock NormalButtonColors = new ColorBlock()
    {
        normalColor = Color.black,
        highlightedColor = new Color(55f / _byte, 55f / _byte, 55f / _byte),
        pressedColor = new Color(200f / _byte, 200f / _byte, 200f / _byte)
    };

    public ColorBlock CloseButtonColors = new ColorBlock()
    {
        normalColor = Color.black,
        highlightedColor = Color.red,
        pressedColor = new Color(200f / _byte, 200f / _byte, 200f / _byte)
    };

    public Color TextColor { get; set; } = Color.white;

    //Font
    public TMP_FontAsset TMP_FontAsset { get; set; } = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
    public FontStyles FontStyle { get; set; } = FontStyles.Normal;
    public float FontSize { get; set; } = 50;

    //Button sprites
    public Sprite DragSprite { get; set; } = Resources.Load<Sprite>("Images/Icons/drag_handle_white");
    public Sprite ClearSprite { get; set; } = Resources.Load<Sprite>("Images/Icons/clear_all_white");
    public Sprite LanguageSprite { get; set; } = Resources.Load<Sprite>("Images/Icons/language_white");
    public Sprite CloseSprite { get; set; } = Resources.Load<Sprite>("Images/Icons/close_white");

}