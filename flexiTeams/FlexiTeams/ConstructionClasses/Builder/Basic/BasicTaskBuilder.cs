using FlexiTeams.ConstructionClasses.Builder.Interface;
using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Task.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;
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

    public void Set(DateTime begin, DateTime end)
    {
        _task.begin = begin;
        _task.end = end;
    }

    public void Set(List<Profession> requiredProfessions)
    {
        _task.RequiredProfessions = requiredProfessions;
    }
    public void Set(List<DataName> requiredDataNames)
    {
        _task.RequiredData = requiredDataNames;
    }
}