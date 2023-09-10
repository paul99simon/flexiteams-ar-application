namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Qualification
{
    private readonly string Get;

    public Qualification(string qualification)
    {
        Get = qualification;
    }

    public override string ToString()
    {
        return Get;
    }
}