namespace FlexiTeams.Data.Wrapper;

public class ProfessionalInfo
{
    public string Language { get; }
    public string Get { get; }

    public ProfessionalInfo(string language, string professionalInfo)
    {
        Language = language;
        Get = professionalInfo;
    }
}