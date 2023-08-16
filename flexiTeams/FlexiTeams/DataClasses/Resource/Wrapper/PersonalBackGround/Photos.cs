using System.Collections;

namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Photos : IEnumerable<Photo>
{
    public List<Photo> List { get; } = new();
    public Photo this[int index] => List[index];

    public void Add(Photo photo)
    {
        List.Add(photo);
    }
    
    public IEnumerator<Photo> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}