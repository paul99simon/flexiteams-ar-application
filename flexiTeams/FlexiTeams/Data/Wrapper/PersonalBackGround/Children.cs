using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class Children : IEnumerable<Child>
{
    public List<Child> List { get; } = new();
    public Child this[int index] => List[index];
    
    public void Add(Child child)
    {
        List.Add(child);
    }
    
    public IEnumerator<Child> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}