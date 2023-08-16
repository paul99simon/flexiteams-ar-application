using FlexiTeams.DataClasses.Data;
using FlexiTeams.DataClasses.Data.Wrapper;

namespace FlexiTeams.ConstructionClasses.Builder;

public interface IDataBuilder
{
    public void Reset();
    public Data GetData();
    
    public void SetDataID(DataId id);
    public void SetName(Name name);
}