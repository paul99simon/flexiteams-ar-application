namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Study
{
    public string Language { get; }
    public string Get { get; }
    public string? Location { get;}

    public Study(string language, string studies, string location)
    {
        Language = language;
        Get = studies;
        Location = location;
    }
    
    public Study(string language, string studies)
    {
        Language = language;
        Get = studies;
        Location = null;
    }
}