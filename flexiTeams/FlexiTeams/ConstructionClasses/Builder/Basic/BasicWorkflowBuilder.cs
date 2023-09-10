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

    public void Set(Dictionary<string, WorkflowType> types)
    {
        _workflow.AddRange(types);
    }

    public void Set(Duration duration)
    {
        _workflow.Duration = duration;
    }

    public void Set(Dictionary<string, Venue> venues)
    {
        _workflow.AddRange(venues);
    }

    public void Set(Procedures procedures)
    {
        _workflow.Procedures = procedures;
    }
    
    public void SetLanguage(string langCode)
    {
        _workflow.SetLanguage(langCode);
    }

    public string GetLanguage()
    {
        return _workflow.GetLanguage();
    }
}