using System.Security;
using System.Xml;

namespace FlexiTeams.Util;

public class DataPoolXmlWriter
{
    public static XmlDocument DataXml(List<string> data, int count)
    {
        int id = 0;
        
        var doc = new XmlDocument();
        using (var writer = doc.CreateNavigator()?.AppendChild())
        {
            writer.WriteStartElement("DataPool");
            foreach (var s in data)
            {
                for(int i = 0 ; i < count ; i++)
                {
                    writer.WriteStartElement("Data");
                    writer.WriteAttributeString("xml", "id", null, "Data_" + id++);
                    writer.WriteStartElement("Name");
                    writer.WriteAttributeString("xml", "lang", null, "en");
                    writer.WriteString(s);
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                } 
            }
            writer.WriteEndElement();
            writer.Flush();

        }

        return doc;
    }
}