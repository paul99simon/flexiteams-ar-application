using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class Skills : IEnumerable<Skill>
{
    public List<Skill> List { get; } = new();
    public Skill this[int index] => List[index];

    public void Add(Skill skill)
    {
        List.Add(skill);
    }
    
    public IEnumerator<Skill> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}