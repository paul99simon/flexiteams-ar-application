using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class Studies : IEnumerable<Study>
{
    private readonly List<Study> _studies;

    public Studies(List<Study> studies)
    {
        _studies = studies;
    }
    
    public IEnumerator<Study> GetEnumerator()
    {
        return _studies.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}