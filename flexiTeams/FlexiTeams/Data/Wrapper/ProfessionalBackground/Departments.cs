using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class Departments : IEnumerable<Department>
{
    public List<Department> List { get; } = new();
    public Department this[int index] => List[index];

    public void Add(Department department)
    {
        List.Add(department);
    }
    
    public IEnumerator<Department> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}