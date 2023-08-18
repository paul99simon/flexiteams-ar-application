using FlexiTeams.DataClasses.Data;
using FlexiTeams.DataClasses.Data.Wrapper;

namespace FlexiTeams.ConstructionClasses.Builder;

public class BasicDataBuilder : IDataBuilder
{
    private Data _data = new ();
    
    public void Reset()
    {
        _data = new Data();
    }

    public Data GetData()
    {
        Data temp = _data;
        Reset();
        return temp;
    }

    public void Set(DataId id)
    {
        _data.Id = id;
    }

    public void Set(Dictionary<string, Name> names)
    {
        _data.AddRange(names);
    }
}