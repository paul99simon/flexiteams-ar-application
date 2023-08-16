using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.DataClasses.Workflow;

public class Workflow
{
    public WorkflowType Type { get; set; }
    public Duration? Duration { get; set; }
    public Venue? Venue { get; set; }
    public Procedures? Procedures { get; set; }
    
}