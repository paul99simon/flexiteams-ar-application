using FlexiTeams.DataClasses.Workflow;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.ConstructionClasses;

public interface IWorkflowBuilder
{
    public Workflow GetWorkflow();
    public void Reset();

    public void SetWorkflowType(WorkflowType type);
    public void SetDuration(Duration duration);
    public void SetVenue(Venue venue);
    public void SetProcedures(Procedures procedures);
}