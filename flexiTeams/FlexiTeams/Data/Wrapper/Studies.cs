namespace FlexiTeams.Data.Wrapper;

public class Studies
{
    private readonly string _lang;
    private readonly string _studies;
    private readonly string? _location;

    public Studies(string lang, string studies, string? location)
    {
        _lang = lang;
        _studies = studies;
        _location = location;
    }
    
    public Studies(string lang, string studies)
    {
        _lang = lang;
        _studies = studies;
        _location = null;
    }

    public string Get()
    {
        return _studies;
    }

    public string Language()
    {
        return _lang;
    }

    public string? Location()
    {
        return _location;
    }
}