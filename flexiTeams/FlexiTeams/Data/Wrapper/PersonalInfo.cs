namespace FlexiTeams.Data.Wrapper;

public class PersonalInfo
{
    private readonly string _lang;
    private readonly string _personalInfo;

    public PersonalInfo(string lang, string personalInfo)
    {
        _lang = lang;
        _personalInfo = personalInfo;
    }

    public string Get()
    {
        return _personalInfo;
    }

    public string Language()
    {
        return _lang;
    }
}