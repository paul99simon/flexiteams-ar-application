using FlexiTeams.ConstructionClasses.Builder.Interface;
using FlexiTeams.ConstructionClasses.Director.Interface;
using FlexiTeams.DataClasses.Workflow;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;
using System.Xml.Linq;

namespace FlexiTeams.ConstructionClasses.Director.Basic
{
    public class BasicWorkflowDirector : IWorkflowDirector
    {
        public Workflow Construct(XElement workflowNode, IWorkflowBuilder wBuilder)
        {

            string id       = workflowNode.Attribute("id").Value;
            string type     = workflowNode.Attribute("type").Value;
            string venue    = workflowNode.Attribute("venue").Value;
            XAttribute durationAttribute = workflowNode.Attribute("duration");

            if (durationAttribute != null ) {
                string duration = durationAttribute.Value;
                var iso = new ISO8601(duration);
                int minutes = iso.Minutes;
                wBuilder.Set(minutes);
            }

            wBuilder.Set(new WorkflowId(id));
            wBuilder.Set(new WorkflowType(type));
            wBuilder.Set(new Venue(venue));

            return wBuilder.GetWorkflow();
        }
    }
}
