using FlexiTeams.DataClasses.Workflow;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
namespace FlexiTeams.ConstructionClasses.Builder.Interface;

public interface IWorkflowBuilder
{
    public Workflow GetWorkflow();
    public void Reset();

    public void Set(WorkflowId id);
    public void Set(WorkflowType types);
    public void Set(DateTime begin);
}