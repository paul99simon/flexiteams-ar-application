using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Task.Wrappper;
using FlexiTeams.DataClasses.Wrapper;
using Task = FlexiTeams.DataClasses.Task.Task;

namespace FlexiTeams.Graph.Nodes;

public class TaskNode : Node
{
    public TaskId Id { get; }

    public Dictionary<Profession, ResourceId> ResourceAllocation { get; } = new();
    public Dictionary<DataName, DataId> DataAllocation { get; } = new();

    public TaskNode(TaskId id)
    {
        Id = id;
    }

}