using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class Departments : IEnumerable<Department>
{
    private readonly List<Department> _departments;

    public Departments(List<Department> departments)
    {
        _departments = departments;
    }

    public IEnumerator<Department> GetEnumerator()
    {
        return _departments.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}