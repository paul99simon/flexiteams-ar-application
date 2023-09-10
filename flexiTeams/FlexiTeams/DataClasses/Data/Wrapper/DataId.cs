namespace FlexiTeams.DataClasses.Data.Wrapper;

public class DataId
{
    private readonly string Get;

    public DataId(string dataId)
    {
        Get = dataId;
    }

    public override string ToString()
    {
        return Get;
    }
}