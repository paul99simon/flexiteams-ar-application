namespace FlexiTeams.DataClasses.Task.Wrappper;

public class TaskType
{
    public string Get { get; }

    public TaskType(string type)
    {
        Get = type;
    }

    public override string ToString()
    {
        return Get;
    }
}