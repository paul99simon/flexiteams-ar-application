namespace FlexiTeams.Data.Wrapper;

public class Stressor
{
    private readonly string _lang;
    private readonly string _stressor;

    public Stressor(string lang, string stressor)
    {
        _stressor = stressor;
        _lang = lang;
    }

    public string Get()
    {
        return _stressor;
    }

    public string Language()
    {
        return _lang;
    }
}