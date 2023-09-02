using FlexiTeams.DataClasses.Data;
using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.ConstructionClasses.Builder;

public interface IDataBuilder : ILanguageObject
{
    public void Reset();
    public Data GetData();
    
    public void Set(DataId id);
    public void Set(Dictionary<string, DataName> names);
}