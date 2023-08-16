using FlexiTeams.DataClasses.Workflow;

namespace FlexiTeams.Graph.Nodes;

public class WorkflowNode : Node
{
    public Workflow Workflow { get; }
    public List<TaskNode> StartNodes { get; }

    public WorkflowNode(Workflow workflow, List<TaskNode> nodes)
    {
        Workflow = workflow;
        StartNodes = nodes;
    }
}