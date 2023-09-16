using FlexiTeams.DataClasses.Data.Wrapper;

namespace FlexiTeams.Graph.Nodes;

public class DataNode : Node
{
    public DataId Id;

    public DataNode(DataId id)
    {
        Id = id;
    }
}