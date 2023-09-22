using FlexiTeams.ConstructionClasses.Builder.Interface;
using System.Xml.Linq;
using Task = FlexiTeams.DataClasses.Task.Task;

namespace FlexiTeams.ConstructionClasses.Director.Interface
{
    public interface ITaskDirector
    {
        public Task Construct(XElement taskNode, ITaskBuilder tBuilder);
    }
}
