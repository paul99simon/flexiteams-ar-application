namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class MaritalState
{
    public string Language { get; }
    public string Get { get; }

    public MaritalState(string language, string maritalState)
    {
        Get = maritalState;
        Language = language;
    }
}