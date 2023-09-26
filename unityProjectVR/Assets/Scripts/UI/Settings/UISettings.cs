using UnityEngine;

public class UISettings
{
    private const float Byte = 255;

    public float spacing { get; set; } = 20f;

    public Color BackgroundColor = new Color(150f/Byte, 150f/Byte, 150f/Byte, 100f/Byte);

    public TitleBarSettings TitleBarSettings { get; set; } = new();
    public HeaderSettings HeaderSettings { get; set; } = new();
    public DataViewSettings DataViewSettings { get; set; } = new();
}