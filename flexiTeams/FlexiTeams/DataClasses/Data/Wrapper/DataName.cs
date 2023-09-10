namespace FlexiTeams.DataClasses.Data.Wrapper;

public class DataName
{
    private readonly string Get;

    public DataName(string name)
    {
        Get = name;
    }

    public override string ToString()
    {
        return Get;
    }
}