using System.Collections;

namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Studies : IEnumerable<Study>
{
    public List<Study> List { get; } = new();
    public Study this[int index] => List[index];

    public void Add(Study study)
    {
        List.Add(study);
    }
    
    public IEnumerator<Study> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}