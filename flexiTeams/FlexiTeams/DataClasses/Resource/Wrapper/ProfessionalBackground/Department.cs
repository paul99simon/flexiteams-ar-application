namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Department
{
    public string Language { get; }
    public string Get { get; }

    public Department(string language, string department)
    {
        Language = language;
        Get = department;
    }
}