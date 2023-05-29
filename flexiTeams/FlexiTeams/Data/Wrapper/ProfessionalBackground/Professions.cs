using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class Professions : IEnumerable<Profession>
{
    private readonly List<Profession> _professions;

    public Professions(List<Profession> professions)
    {
        _professions = professions;
    }

    public IEnumerator<Profession> GetEnumerator()
    {
        return _professions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}