using FlexiTeams.ConstructionClasses.Builder.Interface;
using FlexiTeams.ConstructionClasses.Director.Interface;
using FlexiTeams.DataClasses.Workflow;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;
using System.Xml;
using System.Xml.Linq;

namespace FlexiTeams.ConstructionClasses.Director.Basic
{
    public class BasicWorkflowDirector : IWorkflowDirector
    {
        public Workflow Construct(XElement workflowNode, IWorkflowBuilder wBuilder)
        {

            string id       = workflowNode.Attribute("id").Value;
            string type     = workflowNode.Attribute("type").Value;
            string begin    = workflowNode.Attribute("date").Value;

            wBuilder.Set(new WorkflowId(id));
            wBuilder.Set(new WorkflowType(type));
            wBuilder.Set(XmlConvert.ToDateTime(begin));

            return wBuilder.GetWorkflow();
        }
    }
}
