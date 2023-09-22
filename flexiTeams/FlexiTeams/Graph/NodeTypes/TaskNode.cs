using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Task.Wrapper;
using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.Graph.Nodes;

public class TaskNode : Node
{
    public TaskId Id { get => (TaskId) _id; }

    public Dictionary<Profession, ResourceId> ResourceAllocation { get; } = new();
    public Dictionary<DataName, DataId> DataAllocation { get; } = new();

    public TaskNode(TaskId id) : base(id) {}

}