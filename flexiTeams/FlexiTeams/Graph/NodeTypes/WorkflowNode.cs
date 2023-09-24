using FlexiTeams.DataClasses.Task.Wrapper;
using FlexiTeams.DataClasses.Workflow.Wrapper;

namespace FlexiTeams.Graph.Nodes;

public class WorkflowNode : Node
{
    public WorkflowId Id { get => (WorkflowId)_id; }

    public TaskId StartNodeId { get; set; }

    public WorkflowNode(WorkflowId id): base(id) {}
}