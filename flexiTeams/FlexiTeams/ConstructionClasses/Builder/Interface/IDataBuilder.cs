using FlexiTeams.DataClasses.Data;
using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.ConstructionClasses.Builder.Interface;

public interface IDataBuilder
{
    public void Reset();
    public Data GetData();

    public void Set(DataId id);
    public void Set(DataName name);
}