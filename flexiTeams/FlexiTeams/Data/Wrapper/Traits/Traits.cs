using System.Collections;
using System.Runtime.CompilerServices;

namespace FlexiTeams.Data.Wrapper;

public class Traits : IEnumerable<Trait>
{
    public List<Trait> List { get; } = new();
    public Trait this[int index] => List[index];

    public void Add(Trait trait)
    {
        List.Add(trait);
    }
    
    public IEnumerator<Trait> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}