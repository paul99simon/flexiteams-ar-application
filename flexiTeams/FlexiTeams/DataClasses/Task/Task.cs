using FlexiTeams.DataClasses.Task.Wrappper;
using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.DataClasses.Task;

public class Task
{
    public TaskId TaskId { get; set; }
    public TaskType TaskType { get; set; }
    public Venue Venue { get; set; }
    public Priority? Priority { get; set; }
    public Duration? Duration { get; set; }
    public Dictionary<Profession, int> ResourceQualifications { get; set; }
}