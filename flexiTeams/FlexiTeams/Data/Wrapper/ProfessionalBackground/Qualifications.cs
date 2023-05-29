using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class Qualifications : IEnumerable<Qualification>
{
    private readonly List<Qualification> _qualifications;

    public Qualifications(List<Qualification> qualifications)
    {
        _qualifications = qualifications;
    }

    public IEnumerator<Qualification> GetEnumerator()
    {
        return _qualifications.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}