using System.Xml;
using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;


namespace FlexiTeams.ConstructionClasses.Director
{
    public class BasicDataDirector
    {

        public static void ConstructFromXmlNode(IDataBuilder builder, XmlNode data)
        {
            builder.Set(GetDataId());
            builder.Set(GetNames());

            DataId GetDataId()
            {
                var node = data;

                string id = node.Attributes.GetNamedItem("xml:id").InnerText;

                return new DataId(id);
            }

            Dictionary<string, DataName> GetNames()
            {
                var nodes = data.SelectNodes("Name");
                var temp = new Dictionary<string, DataName>();

                foreach (XmlNode node in nodes)
                {
                    string lang = node.Attributes.GetNamedItem("xml:lang").InnerText;
                    string value = node.InnerText;

                    temp.Add(lang, new DataName(value));
                }

                return temp;
            }
        }

    }
}
