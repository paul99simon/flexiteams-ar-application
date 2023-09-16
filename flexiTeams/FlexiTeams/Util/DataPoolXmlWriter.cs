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
            writer.WriteAttributeString("xml", "en", "en");
            writer.WriteAttributeString("xsi", "noNamespaceSchemaLocation", "data_pool.xsd");

            foreach (var s in data)
            {
                for(int i = 0 ; i < count ; i++)
                {
                    writer.WriteStartElement("Data");
                    writer.WriteAttributeString("xml", "id", null, "Data_" + id++);
                    writer.WriteStartElement("Name");
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