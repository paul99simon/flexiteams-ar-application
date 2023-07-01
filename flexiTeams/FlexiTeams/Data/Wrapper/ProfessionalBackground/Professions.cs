using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class Professions : IEnumerable<Profession>
{
    public List<Profession> List { get; } = new();
    public Profession this[int index] => List[index];

    public void Add(Profession profession)
    {
        List.Add(profession);
    }

    public IEnumerator<Profession> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}