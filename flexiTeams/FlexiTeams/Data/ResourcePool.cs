using System.Xml;
using flexiTeams.Data;

namespace FlexiTeams.Data;

public class ResourcePool
{
    private List<Resource> _resources = new();

    public ResourcePool(XmlReader reader)
    {
        while (reader.ReadToFollowing("resource"))
        {
            XmlReader temp = reader.ReadSubtree();
            _resources.Add(new Resource(temp));
        }
    }
}