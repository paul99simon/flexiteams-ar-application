using FlexiTeams.ConstructionClasses.Builder.Interface;
using FlexiTeams.ConstructionClasses.Director.Interface;
using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Task.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using System.Xml;
using System.Xml.Linq;
using Task = FlexiTeams.DataClasses.Task.Task;

namespace FlexiTeams.ConstructionClasses.Director.Basic
{
    public class BasicTaskDirector : ITaskDirector
    {
        public Task Construct(XElement taskNode, ITaskBuilder tBuilder)
        {
            string id = taskNode.Attribute("id").Value;
            string type = taskNode.Attribute("type").Value;
            string venue = taskNode.Attribute("venue").Value;
            string begin = taskNode.Attribute("begin").Value;
            string end = taskNode.Attribute("end").Value;

            tBuilder.Set(new TaskId(id));
            tBuilder.Set(new TaskType(type));
            tBuilder.Set(new Venue(venue));
            tBuilder.Set(XmlConvert.ToDateTime(begin, XmlDateTimeSerializationMode.Local), XmlConvert.ToDateTime(end, XmlDateTimeSerializationMode.Local));

            var consumedData = taskNode.Descendants("ConsumedData");
            List<DataName> dataNames = new();
            foreach(var node in consumedData)
            {
                string temp = node.Attribute("type").Value;
                dataNames.Add(new DataName(temp));
            }

            if(dataNames.Count > 0) tBuilder.Set(dataNames);

            var consumedResources = taskNode.Descendants("ConsumedResource");
            List<Profession> professions = new();

            foreach (var node in consumedResources)
            {
                string temp = node.Attribute("type").Value;
                professions.Add(new Profession(temp));
            }

            if(professions.Count > 0) tBuilder.Set(professions);

            return tBuilder.GetTask();
        }
    }
}
