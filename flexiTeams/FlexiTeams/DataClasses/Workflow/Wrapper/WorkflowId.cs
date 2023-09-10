namespace FlexiTeams.DataClasses.Workflow.Wrapper;

public class WorkflowId
{
    private readonly string Get;

    public WorkflowId(string id)
    {
        Get = id;
    }

    public override string ToString()
    {
        return Get;
    }
}