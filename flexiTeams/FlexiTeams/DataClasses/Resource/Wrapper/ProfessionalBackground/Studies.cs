namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Studies
{
    private readonly string Get;
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

    public override string ToString()
    {
        return Get;
    }
}