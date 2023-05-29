namespace FlexiTeams.Data.Wrapper;

public class Department
{
    private readonly string _lang;
    private readonly string _department;

    public Department(string lang, string department)
    {
        _lang = lang;
        _department = department;
    }

    public string Get()
    {
        return _department;
    }

    public string Language()
    {
        return _lang;
    }
}