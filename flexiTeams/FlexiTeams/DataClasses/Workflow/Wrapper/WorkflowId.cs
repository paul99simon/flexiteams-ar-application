using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.DataClasses.Workflow.Wrapper;

public class WorkflowId : Id
{
    public WorkflowId(string id) : base(id) {}

    public override string ToString()
    {
        return base.ToString();
    }
}