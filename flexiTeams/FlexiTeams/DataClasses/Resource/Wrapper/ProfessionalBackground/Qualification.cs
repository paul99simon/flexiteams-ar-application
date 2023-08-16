namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Qualification
{
    public string Language { get; }
    public string Get { get; }

    public Qualification(string language, string qualification)
    {
        Language = language;
        Get = qualification;
    }
}