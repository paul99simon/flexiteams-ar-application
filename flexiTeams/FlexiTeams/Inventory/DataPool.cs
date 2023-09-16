using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Director;
using FlexiTeams.DataClasses.Data;
using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Resource;
using FlexiTeams.Util;
using FlexiTeams.Util.EqualityComperator;
using System.Collections;
using System.Xml;

namespace FlexiTeams.Inventory;

public class DataPool : IEnumerable<Data>
{
    public List<Data> List
    {
        get
        {
            var temp = new List<Data>();

            foreach (var pair in _pool)
            {
                temp.Add(pair.Value);
            }

            return temp;
        }
    }
    public int Count => List.Count;
    private readonly Dictionary<DataId, Data> _pool = new(new DataIdEqualityComparer());
    public Data this[DataId id] => _pool[id];

    public Dictionary<DataName, int> Stock
    {
        get
        {
            var temp = new Dictionary<DataName, int>(new DataNameEqualityComparer());

            foreach (var pair in _pool)
            {
                if (!temp.ContainsKey(pair.Value.Name)) temp.Add(pair.Value.Name, 0);
                temp[pair.Value.Name]++;
            }

            return temp;
        }
    }

    public void Add(Data data)
    {
        if (_pool.ContainsKey(data.Id)) return;
        _pool.Add(data.Id, data);
    }
    
    public IEnumerator<Data> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}