using FlexiTeams.DataClasses.Workflow;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.ConstructionClasses.Builder;

public class BasicWorkflowBuilder : IWorkflowBuilder
{
    private Workflow _workflow = new();

    public Workflow GetWorkflow()
    {
        
        var temp = _workflow;
        Reset();
        return temp;
    }
    public void Reset()
    {
        _workflow = new Workflow();
    }

    public void Set(WorkflowId id)
    {
        _workflow.Id = id;
    }
    public void Set(WorkflowType type)
    {
        _workflow.Type = type;
    }
    public void Set(int minutes)
    {
        _workflow.Minutes = minutes;
    }
    public void Set(Priority priority)
    {
        _workflow.Priority = priority;
    }
    public void Set(Venue venues)
    {
        _workflow.Venue = venues;
    }
    public void Set(Procedures procedures)
    {
        _workflow.Procedures = procedures;
    }
}