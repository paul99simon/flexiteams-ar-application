using System.Collections;
using FlexiTeams.DataClasses.Resource;
using FlexiTeams.Util;
using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams;

public class ResourcePool : IEnumerable<Resource>
{

    public List<Resource> List
    {
        get
        {
            var temp = new List<Resource>();

            foreach (var pair in _pool)
            {
                temp.Add(pair.Value);
            }

            return temp;
        }
    }
    public Resource this[int i] => List[i];
    public int Count => List.Count;

    public Dictionary<Profession, int> Staff
    {
        get
        {
            var temp = new Dictionary<string, int>();

            foreach (var pair in _pool)
            {
                List<Profession> professions = pair.Value.Professions;
                
                foreach(var profession in professions)
                {
                    if (!temp.ContainsKey(profession.ToString())) temp.Add(profession.ToString(), 0);
                    temp[profession.ToString()]++;
                }
            }

            return temp.ToDictionary(pair => new Profession(pair.Key), pair => pair.Value);
        }
    }
    private readonly Dictionary<string, Resource> _pool = new();

    public void Add(Resource resource)
    {
        if (_pool.ContainsKey(resource.Id.ToString())) return;
        _pool.Add(resource.Id.ToString(), resource);
    }

    public IEnumerator<Resource> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}