namespace FlexiTeams.Data.Wrapper;

public class LastName
{
    private readonly string _lastName;

    public LastName(string lastName)
    {
        _lastName = lastName;
    }

    public string Get()
    {
        return _lastName;
    }
}