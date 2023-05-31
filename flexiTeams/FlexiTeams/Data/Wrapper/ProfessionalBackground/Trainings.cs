using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class Trainings : IEnumerable<Training>
{
    public List<Training> List { get; } = new();
    public Training this[int index] => List[index];

    public void Add(Training training)
    {
        List.Add(training);
    }

    public IEnumerator<Training> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}