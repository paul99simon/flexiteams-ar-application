using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleBarSettings
{
    private const float Byte = 255;

    //Color
    public Color BackgroundColor { get; set; } = Color.black;
    public Color TextColor { get; set; } = Color.white;

    public ColorBlock ButtonColors = new()
    {
        normalColor = Color.black,
        highlightedColor = new Color(55f / Byte, 55f / Byte, 55f / Byte),
        pressedColor = new Color(200f / Byte, 200f / Byte, 200f / Byte)
    };

    public ColorBlock CloseButtonColors = new()
    {
        normalColor = Color.black,
        highlightedColor = Color.red,
        pressedColor = new Color(200f / Byte, 200f / Byte, 200f / Byte)
    };

    //Sprite
    public Sprite DragSprite { get; set; } = Resources.Load<Sprite>("Images/Icons/drag_handle_white");
    public Sprite ClearSprite { get; set; } = Resources.Load<Sprite>("Images/Icons/clear_all_white");
    public Sprite LanguageSprite { get; set; } = Resources.Load<Sprite>("Images/Icons/language_white");
    public Sprite CloseSprite { get; set; } = Resources.Load<Sprite>("Images/Icons/close_white");

    //Font
    public float FontSize { get; set; } = 50;
}