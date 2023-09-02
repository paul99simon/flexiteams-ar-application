using FlexiTeams.DataClasses.Workflow;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.ConstructionClasses;

public interface IWorkflowBuilder : ILanguageObject
{
    public Workflow GetWorkflow();
    public void Reset();

    public void Set(WorkflowId id);
    public void Set(Dictionary<string, WorkflowType> types);
    public void Set(Duration duration);
    public void Set(Dictionary<string, Venue> venues);
    public void Set(Procedures procedures);
}