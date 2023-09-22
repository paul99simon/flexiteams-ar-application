using FlexiTeams.ConstructionClasses.Builder.Interface;
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

    public void Set(DataName name)
    {
        _data.Name = name;
    }
}