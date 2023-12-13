using FlexiTeams.ConstructionClasses.Builder.Interface;
using FlexiTeams.DataClasses.Workflow;
using FlexiTeams.DataClasses.Workflow.Wrapper;

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
}