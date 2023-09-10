namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Prefix
{
    private readonly string Get;

    public Prefix(string prefix)
    {
        Get = prefix;
    }

    public override string ToString()
    {
        return Get;
    }
}