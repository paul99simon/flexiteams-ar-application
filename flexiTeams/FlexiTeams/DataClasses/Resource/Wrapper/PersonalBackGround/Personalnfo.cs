namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class PersonalInfo
{
    public string Language { get; }
    public string Get { get; }

    public PersonalInfo(string language, string personalInfo)
    {
        Language = language;
        Get = personalInfo;
    }
}