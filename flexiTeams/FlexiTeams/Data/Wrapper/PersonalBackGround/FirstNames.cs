using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class FirstNames : IEnumerable<FirstName>
{
    public List<FirstName> List { get; } = new();
    public FirstName this[int index] => List[index];

    public void Add(FirstName firstName)
    {
        List.Add(firstName);
    } 

    public IEnumerator<FirstName> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}