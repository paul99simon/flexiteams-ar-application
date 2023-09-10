namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class ProfessionalInfo
{
    private readonly string Get;

    public ProfessionalInfo(string professionalInfo)
    {
        Get = professionalInfo;
    }

    public override string ToString()
    {
        return Get;
    }
}