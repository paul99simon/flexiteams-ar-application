namespace FlexiTeams.DataClasses.Workflow.Wrapper;

public class WorkflowType
{
    private readonly string Get;

    public WorkflowType(string type)
    {
        Get = type;
    }

    public override string ToString()
    {
        return Get;
    }
}