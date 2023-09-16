using FlexiTeams.DataClasses.Resource.Wrapper;

namespace FlexiTeams.Graph.Nodes;

public class ResourceNode : Node
{
    
    public ResourceId Id { get; }

    public ResourceNode(ResourceId id)
    {
        Id = id;
    }

}