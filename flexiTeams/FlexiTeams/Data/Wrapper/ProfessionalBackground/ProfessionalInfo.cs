namespace FlexiTeams.Data.Wrapper;

public class ProfessionalInfo
{
    private readonly string _lang;
    private readonly string _professionalInfo;

    public ProfessionalInfo(string lang, string professionalInfo)
    {
        _lang = lang;
        _professionalInfo = professionalInfo;
    }

    public string Get()
    {
        return _professionalInfo;
    }

    public string Language()
    {
        return _lang;
    }
}