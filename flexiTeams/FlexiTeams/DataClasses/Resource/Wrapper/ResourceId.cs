using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class ResourceId : Id
{
    public ResourceId(string id)
    {
       _id = id;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}