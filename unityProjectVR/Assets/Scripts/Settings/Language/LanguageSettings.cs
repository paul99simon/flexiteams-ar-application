using Assets.Scripts.UI.Settings.Language;
using UnityEngine.UIElements;

public class LanguageSettings
{
    public string Name { get; set; } = "name";
    public string Venue { get; set; } = "venue";
    public string Duration { get; set; } = "duration";

    //Time
    public string Years { get; set; } = "years";
    public string Days { get; set; } = "days";
    public string Hours { get; set; } = "hours";
    public string Minutes { get; set; } = "minutes";

    public ResourceUILanguageSettings ResourceUI { get; set; } = new();
    public DataUILanguageSettings DataUI { get; set; } = new();
    public TaskUILanguageSettings TaskUI { get; set; } = new();
}