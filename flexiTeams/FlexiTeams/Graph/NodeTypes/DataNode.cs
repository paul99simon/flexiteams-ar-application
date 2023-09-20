using FlexiTeams.DataClasses.Data.Wrapper;
namespace FlexiTeams.Graph.Nodes;

public class DataNode : Node
{
    public DataId Id { get => (DataId)_id;}

    public DataNode(DataId id) : base(id) { }
}