using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class MaritalStates : IEnumerable<MaritalState>
{
    public List<MaritalState> List { get; } = new();
    public MaritalState this[int index] => List[index];
    
    public void Add(MaritalState maritalState)
    {
        List.Add(maritalState);
    }
    
    public IEnumerator<MaritalState> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}