using FlexiTeams.FlexiTeamsGraph;

namespace FlexiTeams.ConstructionClasses.Builder;

public interface IGraphBuilder
{
    public void Reset();
    public AdjListsGraph GetGraph();

    public void SetWorkflowNodes(IWorkflowBuilder builder);
    public void SetControllFlow(ITaskBuilder taskBuilder);
    public void SetDataNodes(IDataBuilder dataBuilder);
    public void SetResourceNodes(IResourceBuilder builder);
}