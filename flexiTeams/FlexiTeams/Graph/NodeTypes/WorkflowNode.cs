using FlexiTeams.DataClasses.Workflow.Wrapper;

namespace FlexiTeams.Graph.Nodes;

public class WorkflowNode : Node
{
    public WorkflowId Id { get => (WorkflowId)_id; }

    public TaskNode? StartNode { get; set; }

    public WorkflowNode(WorkflowId id): base(id) {}
}