namespace FlexiTeams.Data.Wrapper;

public class Qualification
{
    private readonly string _lang;
    private readonly string _qualification;

    public Qualification(string lang, string qualification)
    {
        _lang = lang;
        _qualification = qualification;
    }

    public string Get()
    {
        return _qualification;
    }

    public string Language()
    {
        return _lang;
    }
}