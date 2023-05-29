namespace FlexiTeams.Data.Wrapper;

public class MaritalState
{
    private readonly string _lang;
    private readonly string _maritalState;

    public MaritalState(string maritalState)
    {
        _maritalState = maritalState;
    }

    public string Get()
    {
        return _maritalState;
    }

    public string Language()
    {
        return _lang;
    }
}