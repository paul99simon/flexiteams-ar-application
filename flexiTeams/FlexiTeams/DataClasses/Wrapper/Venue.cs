namespace FlexiTeams.DataClasses.Wrapper;

public class Venue
{
    private readonly string Get;

    public Venue(string venue)
    {
        Get = venue;
    }

    public override string ToString()
    {
        return Get;
    }
}