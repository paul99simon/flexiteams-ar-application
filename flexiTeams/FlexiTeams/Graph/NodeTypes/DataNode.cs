using FlexiTeams.DataClasses.Data;

namespace FlexiTeams.Graph.Nodes;

public class DataNode : Node
{
    public Data Data;

    public DataNode(Data data)
    {
        Data = data;
    }

    public override string GetLanguage()
    {
        return Data.GetLanguage();
    }

    public override void SetLanguage(string langCode)
    {
        Data.SetLanguage(langCode);
    }
}