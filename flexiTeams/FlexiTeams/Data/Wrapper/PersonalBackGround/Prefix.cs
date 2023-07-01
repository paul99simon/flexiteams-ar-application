using System.Drawing.Printing;

namespace FlexiTeams.Data.Wrapper;

public class Prefix
{
    public string Get { get; }

    public Prefix(string prefix)
    {
        Get = prefix;
    }
}