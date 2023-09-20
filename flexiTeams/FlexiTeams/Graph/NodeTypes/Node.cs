using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.Graph.Nodes;

public abstract class Node
{
    public Id _id { get; set; }

    public Node(Id id)
    {
        _id = id;
    }
}