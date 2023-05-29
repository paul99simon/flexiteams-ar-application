using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class LastNames : IEnumerable<LastName>
{
    private readonly List<LastName> _lastNames;

    public LastNames(List<LastName> lastNames)
    {
        _lastNames = lastNames;
    }

    public IEnumerator<LastName> GetEnumerator()
    {
        return _lastNames.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}