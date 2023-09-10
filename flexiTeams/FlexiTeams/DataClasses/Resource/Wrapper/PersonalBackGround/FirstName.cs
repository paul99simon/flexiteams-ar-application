namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class FirstName
{
    private readonly string Get;

    public FirstName(string firstName)
    {
        Get = firstName;
    }

    public override string ToString()
    {
        return Get;
    }
}