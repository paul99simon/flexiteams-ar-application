using System.Xml;
using flexiTeams.Data;

namespace FlexiTeams.Data;

public class ResourcePool
{
    private List<Resource> _resources;

    public ResourcePool(String path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        
        Console.Write(doc.InnerXml);
    }
    
    public static void main()
    {
        String path = "../../../../../resourcePools/resource_pool_draft.xml";
        ResourcePool rp = new ResourcePool(path);
    }
}