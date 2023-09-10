namespace FlexiTeams.DataClasses.Task.Wrappper;

public class TaskId
{
    private readonly string Get;

    public TaskId(string id)
    {
        Get = id;
    }

    public override string ToString()
    {
        return Get;
    }
}