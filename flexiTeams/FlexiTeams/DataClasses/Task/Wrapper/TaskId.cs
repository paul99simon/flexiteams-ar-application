using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.DataClasses.Task.Wrapper;

public class TaskId : Id
{
    public TaskId(string id) : base(id)
    {
    }

    public override string ToString()
    {
        return base.ToString();
    }
}