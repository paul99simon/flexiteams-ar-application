namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Studies
{
    public string Get { get; }
    public string? Location { get;}

    public Studies(string studies, string location)
    {
        Get = studies;
        Location = location;
    }
    
    public Studies(string studies)
    {
        Get = studies;
        Location = null;
    }
}