using FlexiTeams.DataClasses.Task.Wrappper;
using FlexiTeams.DataClasses.Wrapper;
using Task = FlexiTeams.DataClasses.Task.Task;

namespace FlexiTeams.ConstructionClasses.Builder;

public class BasicTaskBuilder : ITaskBuilder
{
    private Task _task = new ();
    
    public void Reset()
    {
        _task = new Task();
    }

    public Task GetTask()
    {
        Task temp = _task;
        Reset();
        return temp;
    }

    public void Set(TaskId id)
    {
        _task.Id = id;
    }

    public void Set(Dictionary<string, TaskType> types)
    {
        _task.AddRange(types);
    }

    public void Set(Dictionary<string, Venue> venues)
    {
        _task.AddRange(venues);
    }

    public void Set(Priority priority)
    {
        _task.Priority = priority;
    }

    public void Set(Duration duration)
    {
        _task.Duration = duration;
    }
    
    public void Set(Dictionary<string, List<Profession>> requiredProfessions)
    {
        _task.AddRange(requiredProfessions);
    }
}