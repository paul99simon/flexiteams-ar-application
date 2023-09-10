namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class LastName
{
    private readonly string Get;

    public LastName(string lastName)
    {
        Get  = lastName;
    }

    public override string ToString()
    {
        return Get;
    }
}