using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class MeansOfTransport : IEnumerable<Vehicle>
{
    public List<Vehicle> List { get; } = new();
    public Vehicle this[int index] => List[index];
    

    public void Add(Vehicle vehicle)
    {
        List.Add(vehicle);
    }
    
    public IEnumerator<Vehicle> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}