using System.Collections;
using System.ComponentModel;

namespace FlexiTeams.Data.Wrapper;

public class Qualifications : IEnumerable<Qualification>
{
    public List<Qualification> List { get; } = new ();
    public Qualification this[int index] => List[index];

    public void Add(Qualification qualification)
    {
        List.Add(qualification);
    }

    public IEnumerator<Qualification> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}