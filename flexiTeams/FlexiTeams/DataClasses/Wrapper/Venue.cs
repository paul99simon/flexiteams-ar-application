namespace FlexiTeams.DataClasses.Wrapper;

public class Venue
{
    public string Get { get; }
    public string Language { get; }

    public Venue(string venue, string language)
    {
        Get = venue;
        Language = language;
    }
}