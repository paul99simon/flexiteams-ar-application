namespace FlexiTeams.Data.Wrapper;

public class FirstName
{
    private readonly string _firstName;

    public FirstName(string firstName)
    {
        _firstName = firstName;
    }

    public string Get()
    {
        return _firstName;
    }
}