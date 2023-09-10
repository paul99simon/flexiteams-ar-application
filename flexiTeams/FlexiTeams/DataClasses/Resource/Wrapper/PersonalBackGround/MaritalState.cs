namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class MaritalState
{
    private readonly string Get;

    public MaritalState(string maritalState)
    {
        Get = maritalState;
    }

    public override string ToString()
    {
        return Get;
    }
}