using FlexiTeams.DataClasses.Task.Wrappper;
using FlexiTeams.DataClasses.Wrapper;
using Task = FlexiTeams.DataClasses.Task.Task;

namespace FlexiTeams.ConstructionClasses;



public interface ITaskBuilder
{
    public void Reset();
    public Task GetTask();

    public void SetTaskID(TaskId id);
    public void SetTaskType(TaskType type);
    public void SetVenue(Venue venue);
    public void SetPriority(Priority priority);
    public void SetDuration(Duration duration);
    public void SetResourceQualification(Dictionary<Profession, int> resourceQualification);
}