using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using FlexiTeams.ConstructionClasses.Builder.Interface;
using FlexiTeams.ConstructionClasses.Director.Interface;
using FlexiTeams.DataClasses.Data;
using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;


namespace FlexiTeams.ConstructionClasses.Director.Basic
{
    public class BasicDataDirector : IDataDirector
    {
        public Data Construct(XElement dataNode, IDataBuilder dBuilder)
        {
            dBuilder.Set(GetDataId());
            dBuilder.Set(GetName());

            return dBuilder.GetData();
            
            DataId GetDataId()
            {
                string id = dataNode.Attribute(XNamespace.Xml + "id").Value;
                return new DataId(id);
            }
            DataName GetName()
            {
                string type = dataNode.Attribute("type").Value;
                return new DataName(type);
            }
        }
    }
}
