namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Stressor
{
    private readonly string Get;

    public Stressor(string stressor)
    {
        Get = stressor;
    }

    public override string ToString()
    {
        return Get;
    }
}