using System.Collections;
using FlexiTeams.DataClasses.Resource;
using FlexiTeams.Util;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.Util.EqualityComperator;

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
    public int Count => List.Count;
    private readonly Dictionary<ResourceId, Resource> _pool = new(new ResourceIdEqualityComparer());
    public Resource this[ResourceId id] => _pool[id];

    public Dictionary<Profession, int> Staff
    {
        get
        {
            var temp = new Dictionary<string, int>();

            foreach (var pair in _pool)
            {
                List<Profession> professions = pair.Value.Professions;

                foreach (var profession in professions)
                {
                    if (!temp.ContainsKey(profession.ToString())) temp.Add(profession.ToString(), 0);
                    temp[profession.ToString()]++;
                }
            }

            return temp.ToDictionary(pair => new Profession(pair.Key), pair => pair.Value);
        }
    }

    public void Add(Resource resource)
    {
        if (_pool.ContainsKey(resource.Id)) return;
        _pool.Add(resource.Id, resource);
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