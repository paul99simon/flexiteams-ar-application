using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.DataClasses.Data.Wrapper;

public class DataId : Id
{
    
    public DataId(string dataId)
    {
        _id = dataId;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}