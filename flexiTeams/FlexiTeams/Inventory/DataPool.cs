using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Director;
using FlexiTeams.DataClasses.Data;
using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.Util;
using System.Collections;
using System.Xml;

namespace FlexiTeams.Inventory;

public class DataPool : IEnumerable<Data>, ILanguageObject
{
    public List<Data> List
    {
        get
        {
            var temp = new List<Data>();

            foreach (var pair in _pool)
            {
                temp.Add(pair.Value);
            }

            return temp;
        }
    }
    public Data this[int i] => List[i];
    public int Count => List.Count;
    public Dictionary<DataName, int> Stock
    {
        get
        {
            var temp = new Dictionary<string, int>();

            foreach (var pair in _pool)
            {
             if(!temp.ContainsKey(pair.Value.Name.Get)) temp.Add(pair.Value.Name.Get, 0);
             temp[pair.Value.Name.Get]++;
            }

            return temp.ToDictionary(pair => new DataName(pair.Key), pair => pair.Value);
        }
    }
    private Dictionary<string, Data> _pool = new Dictionary<string, Data>();

    public DataPool(IDataBuilder builder, XmlReader reader)
    {
        XmlDocument doc = new XmlDocument();
        while (reader.ReadToFollowing("Data"))
        {
            XmlNode node = doc.ReadNode(reader);
            BasicDataDirector.ConstructFromXmlNode(builder, node);
            Data data = builder.GetData();
            
            _pool.Add(data.Id.Get, data);
        }
    }

    public DataPool(IDataBuilder builder, string path)
    {
        XmlReader reader = XmlReader.Create(path);
        XmlDocument doc = new XmlDocument();
        while (reader.ReadToFollowing("Data"))
        {
            XmlNode node = doc.ReadNode(reader);
            BasicDataDirector.ConstructFromXmlNode(builder, node);
            Data data = builder.GetData();

            _pool.Add(data.Id.Get, data);
        }
    }
    
    public IEnumerator<Data> GetEnumerator()
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
        if(! ISO_639_1.IsValidCode(langCode)) return;
        _langCode = langCode;
        
        foreach (var pair in this)
        {
            pair.SetLanguage(_langCode);
        }
    }

    public string GetLanguage()
    {
        return _langCode;
    }
}