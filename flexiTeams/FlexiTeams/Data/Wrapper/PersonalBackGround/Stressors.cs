using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class Stressors : IEnumerable<Stressor>
{
    public List<Stressor> List { get; } = new ();
    public Stressor this[int index] => List[index];

    public void Add(Stressor stressor)
    {
        List.Add(stressor);
    }

    public IEnumerator<Stressor> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}