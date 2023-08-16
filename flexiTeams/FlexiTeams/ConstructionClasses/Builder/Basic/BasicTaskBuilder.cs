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

    public void SetTaskID(TaskId id)
    {
        _task.TaskId = id;
    }

    public void SetTaskType(TaskType type)
    {
        _task.TaskType = type;
    }

    public void SetVenue(Venue venue)
    {
        _task.Venue = venue;
    }

    public void SetPriority(Priority priority)
    {
        _task.Priority = priority;
    }

    public void SetDuration(Duration duration)
    {
        _task.Duration = duration;
    }

    public void SetResourceQualification(Dictionary<Profession, int> resourceQualification)
    {
        _task.ResourceQualifications = resourceQualification;
    }
}