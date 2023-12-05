using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.DataClasses.Workflow;

public class Workflow
{
    public WorkflowId Id { get; set; }
    public WorkflowType Type { get; set; }
    public DateTime Begin { get; set; }
}