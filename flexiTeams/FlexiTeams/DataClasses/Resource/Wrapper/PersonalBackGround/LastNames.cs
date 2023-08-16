using System.Collections;

namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class LastNames : IEnumerable<LastName>
{
    public List<LastName> List { get; } = new();
    public LastName this[int index] => List[index];

    public void Add(LastName lastName)
    {
        List.Add(lastName);
    } 
    
    public IEnumerator<LastName> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}