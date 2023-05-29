namespace FlexiTeams.Data.Wrapper;

public class Training
{
    private readonly string _lang;
    private readonly string _training;

    public Training(string lang, string training)
    {
        _lang = lang;
        _training = training;
    }

    public string Get()
    {
        return _training;
    }

    public string Language()
    {
        return _lang;
    }
}