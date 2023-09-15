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
            builder.Set(GetName());

            DataId GetDataId()
            {
                var node = data;

                string id = node.Attributes.GetNamedItem("xml:id").InnerText;

                return new DataId(id);
            }
            DataName GetName()
            {
                var node = data.SelectSingleNode("Name");
                return new DataName(node.InnerText);
            }
        }

    }
}
