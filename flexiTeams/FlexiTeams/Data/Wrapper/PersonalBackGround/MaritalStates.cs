using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class MaritalStates : IEnumerable<MaritalState>
{
    private readonly List<MaritalState> _maritalStates;

    public MaritalStates(List<MaritalState> states)
    {
        _maritalStates = states;
    }

    public IEnumerator<MaritalState> GetEnumerator()
    {
        return _maritalStates.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}