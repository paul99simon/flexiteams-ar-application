using System.Collections;
using System.Xml;
using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Diretor;
using FlexiTeams.DataClasses.Resource;
using FlexiTeams.Util;

namespace FlexiTeams;

public class ResourcePool : IEnumerable<Resource> , ILanguageObject
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
            XMLResourceDirector.ConstructFromXmlNode(builder, node);
            List.Add(builder.GetResource());
        }
    }

    public ResourcePool(IResourceBuilder builder, string path)
    {
        XmlReader reader = XmlReader.Create(path);
        XmlDocument doc = new XmlDocument();
        while (reader.ReadToFollowing("resource"))
        {
            XmlNode node = doc.ReadNode(reader);
            XMLResourceDirector.ConstructFromXmlNode(builder, node);
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

    private string _langCode = "";
    public void SetLanguage(string langCode)
    {
        if(!ISO_639_1.IsValidCode(langCode)) return;
        _langCode = langCode;
        
        foreach (var resource in this)
        {
            resource.SetLanguage(_langCode);
        }
    }
    public string GetLanguage()
    {
        return _langCode;
    }
}