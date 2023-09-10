namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class PersonalInfo
{
    private readonly string Get;

    public PersonalInfo(string personalInfo)
    {
        Get = personalInfo;
    }

    public override string ToString()
    {
        return Get;
    }
}