namespace FlexiTeams.DataClasses.Task.Wrapper;

public class TaskType
{
    private readonly string Get;

    public TaskType(string type)
    {
        Get = type;
    }

    public override string ToString()
    {
        return Get;
    }
}