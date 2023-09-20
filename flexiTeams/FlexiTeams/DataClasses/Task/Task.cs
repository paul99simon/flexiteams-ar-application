using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Task.Wrappper;
using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.DataClasses.Task;

public class Task
{
    public TaskId Id { get; set; }

    public TaskType Type { get; set; }
    public Venue? Venue { get; set; }
    public int Minutes { get; set; }

    public List<Profession> RequiredProfessions { get; } = new();
    public List<DataName> RequiredData { get; } = new();
}