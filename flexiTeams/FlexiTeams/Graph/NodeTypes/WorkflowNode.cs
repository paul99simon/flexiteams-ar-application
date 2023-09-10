using FlexiTeams.DataClasses.Workflow;

namespace FlexiTeams.Graph.Nodes;

public class WorkflowNode : Node
{
    public Workflow Workflow { get; }
    public TaskNode? StartNode { get; set; }

    public WorkflowNode(Workflow workflow)
    {
        Workflow = workflow;
    }

    public override string GetLanguage()
    {
        return Workflow.GetLanguage();
    }

    public override void SetLanguage(string langCode)
    {
        Workflow.SetLanguage(langCode);
    }
}