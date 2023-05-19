namespace FlexiTeams.Data.Wrapper;

public class MaritalStatus
{
    private readonly string _lang;
    private readonly string _maritalStatus;

    public MaritalStatus(string maritalStatus)
    {
        _maritalStatus = maritalStatus;
    }

    public string Get()
    {
        return _maritalStatus;
    }

    public string Language()
    {
        return _lang;
    }
}