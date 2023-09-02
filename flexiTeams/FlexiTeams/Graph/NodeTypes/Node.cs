using FlexiTeams.Util;

namespace FlexiTeams.Graph.Nodes;

public abstract class Node : ILanguageObject
{
    public abstract string GetLanguage();
    public abstract void SetLanguage(string langCode);
}