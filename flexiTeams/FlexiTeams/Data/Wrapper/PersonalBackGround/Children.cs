using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class Children : IEnumerable<Child>
{
    private readonly List<Child> _children;

    public Children(List<Child> children)
    {
        _children = children;
    }

    public IEnumerator<Child> GetEnumerator()
    {
        return _children.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}