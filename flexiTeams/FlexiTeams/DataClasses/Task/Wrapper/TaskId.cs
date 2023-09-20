using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.DataClasses.Task.Wrappper;

public class TaskId : Id
{
    public TaskId(string id)
    {
        _id = id;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}