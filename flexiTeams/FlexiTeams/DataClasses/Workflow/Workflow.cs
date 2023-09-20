using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.DataClasses.Workflow;

public class Workflow
{
    public WorkflowId Id { get; set; }
    public WorkflowType Type { get; set; }
    public Priority Priority { get; set; }
    public int Minutes { get; set; }
    public Venue Venue { get; set; }
    public Procedures Procedures { get; set; }
}