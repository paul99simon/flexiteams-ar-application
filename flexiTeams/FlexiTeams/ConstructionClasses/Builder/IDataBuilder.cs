using FlexiTeams.DataClasses.Data;
using FlexiTeams.DataClasses.Data.Wrapper;

namespace FlexiTeams.ConstructionClasses.Builder;

public interface IDataBuilder
{
    public void Reset();
    public Data GetData();
    
    public void Set(DataId id);
    public void Set(Dictionary<string, Name> names);
}