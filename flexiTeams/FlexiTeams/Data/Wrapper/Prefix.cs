using System.Drawing.Printing;

namespace FlexiTeams.Data.Wrapper;

public class Prefix
{
    private readonly string _prefix;

    public Prefix(string prefix)
    {
        _prefix = prefix;
    }

    public string Get()
    {
        return _prefix;
    }
}