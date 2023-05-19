namespace FlexiTeams.Data.Wrapper;

public class MeansOfTransport
{
    private readonly string _lang;
    private readonly string _meansOfTransport;

    public MeansOfTransport(string lang, string meansOfTransport)
    {
        _lang = lang;
        _meansOfTransport = meansOfTransport;
    }
    
    public string Get()
    {
        return _meansOfTransport;
    }

    public string Language()
    {
        return _lang;
    }
}