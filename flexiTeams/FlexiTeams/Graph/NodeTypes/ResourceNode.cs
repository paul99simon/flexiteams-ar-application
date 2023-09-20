using FlexiTeams.DataClasses.Resource.Wrapper;

namespace FlexiTeams.Graph.Nodes;

public class ResourceNode : Node
{
    public ResourceId Id { get => ((ResourceId) _id); }

    public ResourceNode(ResourceId id) : base(id) {}
}