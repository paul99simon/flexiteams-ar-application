namespace FlexiTeams.DataClasses.Task.Wrappper;

public class TaskId
{
    public string Get { get; set; }

    public TaskId(string id)
    {
        Get = id;
    }
}