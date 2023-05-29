namespace FlexiTeams.Data.Wrapper;

public class Study
{
    private readonly string _lang;
    private readonly string _study;
    private readonly string? _location;

    public Study(string lang, string studies, string? location)
    {
        _lang = lang;
        _study = studies;
        _location = location;
    }
    
    public Study(string lang, string studies)
    {
        _lang = lang;
        _study = studies;
        _location = null;
    }

    public string Get()
    {
        return _study;
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