public class LanguageSettings
{
    public string Years { get; set; } = "years";
    public string Days { get; set; } = "days";
    public string Hours { get; set; } = "hours";
    public string Minutes { get; set; } = "minutes";

    public ResourceUILanguageSettings ResourceUI { get; set; } = new();
}