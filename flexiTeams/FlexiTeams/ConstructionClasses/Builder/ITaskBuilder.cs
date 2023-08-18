using FlexiTeams.DataClasses.Task.Wrappper;
using FlexiTeams.DataClasses.Wrapper;
using Task = FlexiTeams.DataClasses.Task.Task;

namespace FlexiTeams.ConstructionClasses;



public interface ITaskBuilder
{
    public void Reset();
    public Task GetTask();

    public void Set(TaskId id);
    public void Set(Dictionary<string, TaskType> types);
    public void Set(Dictionary<string, Venue> venues);
    public void Set(Priority priority);
    public void Set(Duration duration);
    public void Set(Dictionary<string, List<Profession>> requiredProfessions);
}