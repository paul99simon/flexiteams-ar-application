using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class FirstNames : IEnumerable<FirstName>
{
    private readonly List<FirstName> _firstNames;

    public FirstNames(List<FirstName> firstNames)
    {
        _firstNames = firstNames;
    }

    public IEnumerator<FirstName> GetEnumerator()
    {
        return _firstNames.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}