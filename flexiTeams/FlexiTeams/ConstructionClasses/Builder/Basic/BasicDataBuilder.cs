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

    public void SetDataID(DataId id)
    {
        _data._dataId = id;
    }

    public void SetName(Name name)
    {
        _data.Name = name;
    }
}