using FlexiTeams.DataClasses.Workflow;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
namespace FlexiTeams.ConstructionClasses;

public interface IWorkflowBuilder
{
    public Workflow GetWorkflow();
    public void Reset();

    public void Set(WorkflowId id);
    public void Set(WorkflowType types);
    public void Set(Priority priority);
    public void Set(int minutes);
    public void Set(Venue venues);
    public void Set(Procedures procedures);
}