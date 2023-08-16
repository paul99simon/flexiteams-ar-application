using FlexiTeams.DataClasses.Resource;

namespace FlexiTeams.Graph.Nodes;

public class ResourceNode : Node
{
    
    public Resource Resource { get; }

    public ResourceNode(Resource resource)
    {
        Resource = resource;
    }
}