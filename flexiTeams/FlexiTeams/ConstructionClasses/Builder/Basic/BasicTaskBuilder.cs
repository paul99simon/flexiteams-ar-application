using FlexiTeams.DataClasses.Data.Wrapper;
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
    public void Set(TaskType type)
    {
        _task.Type = type;
    }
    public void Set(Venue venue)
    {
        _task.Venue = venue;
    }
    public void Set(int minutes)
    {
        _task.Minutes = minutes;
    }
    public void Set(List<Profession> requiredProfessions)
    {
        _task.RequiredProfessions.AddRange(requiredProfessions);
    }
    public void Set(List<DataName> requiredDataNames)
    {
        _task.RequiredData.AddRange(requiredDataNames);
    }
}