namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Trait
{
    public KeyValuePair<string, int> Get { get; }

    public Trait(KeyValuePair<string, int> trait)
    {
        Get = trait;
    }
}