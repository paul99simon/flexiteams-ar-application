namespace FlexiTeams.Data.Wrapper;

public class Trait
{
    private readonly string _lang;
    private readonly KeyValuePair<string, int> _trait;

    public Trait(string lang, KeyValuePair<string, int> trait)
    {
        _lang = lang;
        _trait = trait;
    }

    public KeyValuePair<string, int> Get()
    {
        return _trait;
    }

    public string Language()
    {
        return _lang;
    }
}