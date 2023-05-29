namespace FlexiTeams.Data.Wrapper;

public class Profession
{
    private readonly string _lang;
    private readonly string _profession;
    
    public Profession(string lang, string profession)
    {
        _lang = lang;
        _profession = profession;
    }

    public string Get()
    {
        return _profession;
    }

    public string Language()
    {
        return _lang;
    }
}