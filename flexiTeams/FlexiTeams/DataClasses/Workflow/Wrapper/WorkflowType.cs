namespace FlexiTeams.DataClasses.Workflow.Wrapper;

public class WorkflowType
{
    public string Get { get;}
    public string Language { get; }

    public WorkflowType(string type, string language)
    {
        Get = type;
        Language = language;
    }
}