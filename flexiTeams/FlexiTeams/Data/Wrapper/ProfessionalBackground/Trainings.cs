using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class Trainings : IEnumerable<Training>
{
    private readonly List<Training> _trainings;

    public Trainings(List<Training> trainings)
    {
        _trainings = trainings;
    }

    public IEnumerator<Training> GetEnumerator()
    {
        return _trainings.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}