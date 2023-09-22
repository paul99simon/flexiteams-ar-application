using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Task.Wrapper;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using Task = FlexiTeams.DataClasses.Task.Task;

namespace FlexiTeams.ConstructionClasses.Builder.Interface;

public interface ITaskBuilder
{
    public void Reset();
    public Task GetTask();

    public void Set(TaskId id);
    public void Set(TaskType types);
    public void Set(Venue venues);
    public void Set(int minutes);
    public void Set(List<Profession> requiredProfessions);
    public void Set(List<DataName> requiredDataNames);
}