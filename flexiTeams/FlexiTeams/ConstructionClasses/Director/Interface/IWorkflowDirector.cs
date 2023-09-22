using FlexiTeams.ConstructionClasses.Builder.Interface;
using FlexiTeams.DataClasses.Workflow;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FlexiTeams.ConstructionClasses.Director.Interface
{
    public interface IWorkflowDirector
    {
        public Workflow Construct(XElement workflowNode, IWorkflowBuilder wBuilder);
    }
}
