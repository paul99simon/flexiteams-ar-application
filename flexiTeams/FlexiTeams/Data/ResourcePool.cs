using FlexiTeams.Data.Wrapper;
using System.Collections;
using System.Xml;

namespace FlexiTeams.Data;

public class ResourcePool : IEnumerable<Resource>
{
    public List<Resource> List { get; } = new();
    public Resource this[int index] => List[index];
    public int Count => List.Count;

    public ResourcePool(IResourceBuilder builder, XmlReader reader)
    {


        XmlDocument doc = new XmlDocument();
        while (reader.ReadToFollowing("resource"))
        {
            XmlNode node = doc.ReadNode(reader);
            ResourceDirector.ConstructFromXmlNode(builder, node);
            List.Add(builder.GetResource());
        }
    }

    public void Add(Resource resource)
    {
        List.Add(resource);
    }

    public int Size()
    {
        return List.Count;
    }

    public IEnumerator<Resource> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}