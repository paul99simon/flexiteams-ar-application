namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Trait
{
    public string Language { get; }
    public KeyValuePair<string, int> Get { get; }

    public Trait(string language, KeyValuePair<string, int> trait)
    {
        Language = language;
        Get = trait;
    }
}