using FlexiTeams.ConstructionClasses.Builder.Interface;
using FlexiTeams.DataClasses.Data;
using System.Xml.Linq;

namespace FlexiTeams.ConstructionClasses.Director.Interface
{
    public interface IDataDirector
    {
        public Data Construct(XElement dataNode, IDataBuilder dBuilder);
    }
}
