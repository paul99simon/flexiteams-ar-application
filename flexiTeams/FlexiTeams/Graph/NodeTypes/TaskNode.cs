using Task = FlexiTeams.DataClasses.Task.Task;

namespace FlexiTeams.Graph.Nodes;

public class TaskNode : Node
{
    public Task Task { get; }

    public TaskNode(Task task)
    {
        Task = task;
    }
    
}