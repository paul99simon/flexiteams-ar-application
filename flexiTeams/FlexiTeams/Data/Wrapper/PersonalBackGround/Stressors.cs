using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class Stressors : IEnumerable<Stressor>
{
    private readonly List<Stressor> _stressors;

    public Stressors(List<Stressor> stressors)
    {
        _stressors = stressors;
    }

    public IEnumerator<Stressor> GetEnumerator()
    {
        return _stressors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}