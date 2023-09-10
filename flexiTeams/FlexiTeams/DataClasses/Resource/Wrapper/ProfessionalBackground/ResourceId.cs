namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class ResourceId
{
    private readonly string Get;

    public ResourceId(string id)
    {
        Get = id;
    }

    public override string ToString()
    {
        return Get;
    }
}