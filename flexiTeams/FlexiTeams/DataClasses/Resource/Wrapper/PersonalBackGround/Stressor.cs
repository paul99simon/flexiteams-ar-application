namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Stressor
{
    public string Language { get; }
    public string Get { get; }

    public Stressor(string language, string stressor)
    {
        Get = stressor;
        Language = language;
    }
}