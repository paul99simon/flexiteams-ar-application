using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Exceptions;
using FlexiTeams.Graph.Nodes;
using FlexiTeams.Util;
using System.CodeDom.Compiler;

namespace FlexiTeams.FlexiTeamsGraph;

//Graph class is a directed Graph as shown in the Paper:
//"FlexiTeam: Flexible Team and Work Organization using Process-Oriented Case-Based Reasoning"

public class AdjListsGraph : ILanguageObject
{

    private readonly Dictionary<Node, List<Node>> _adjLists = new ();
    private string _lang = "";
    
    public void AddNode(TaskNode v)
    {
        AddNodeBase(v);
    }

    public void AddNode(WorkflowNode v)
    {
        AddNodeBase(v);
    }

    public void AddNode(ResourceNode v)
    {
        AddNodeBase(v);
    }

    public void AddNode(DataNode v)
    {
        AddNodeBase(v);
    }
    
    private void AddNodeBase(Node v)
    {
        _adjLists.Add(v, new List<Node>());
    }

    public void AddEdge(TaskNode v, TaskNode u)
    {
        AddEdgeBase(v, u);
    }

    public void AddEdge(ResourceNode v, TaskNode u, Profession specifiedProfession)
    {
        //Checks if the Resource has the specifiedProfession
        bool hasProfession = false;
        foreach (var profession in v.Resource.Professions)
        {
            if(specifiedProfession.Equals(profession)) hasProfession=true;
        }

        if (!hasProfession) throw new HasNotRequiredProfessionException(v.Resource, u.Task, specifiedProfession);

        //Checks if the Task requires any more Resources with the specified Profession
        Dictionary<Profession, int> temp = new();
        bool requiresMoreResources = false;
        foreach (var profession in u.Task.RequiredProfessions)
        {

        }


        
        AddEdgeBase(v, u);
        AddEdgeBase(u, v);
    }

    public void AddEdge(DataNode v, TaskNode u)
    {
        AddEdgeBase(v, u);
        AddEdgeBase(u, v);
    }

    public void AddEdge(TaskNode v, WorkflowNode u)
    {
        AddEdgeBase(v, u);
        AddEdgeBase(u, v);
    }

    private void AddEdgeBase(Node v, Node u)
    {
        if(!_adjLists.ContainsKey(v)) AddNodeBase(v);
        if(!_adjLists.ContainsKey(u)) AddNodeBase(u);
        
        _adjLists[v].Add(u);
    }
    
    public List<Node> Adj(Node v)
    {
        return _adjLists[v];
    }
    
    public List<ResourceNode> AdjResourceNodes(TaskNode v)
    {
        List<Node> temp = Adj(v);
        List<ResourceNode> result = new();

        foreach (var u in temp)
        {
            if (u is ResourceNode node)
            {
                result.Add(node);
            }
        }

        return result;
    }
    
    public List<DataNode> AdjDataNodes(TaskNode v)
    {
        List<Node> temp = Adj(v);
        List<DataNode> result = new();

        foreach (var u in temp)
        {
            if (u is DataNode node)
            {
                result.Add(node);
            }
        }

        return result;
    }

    public List<TaskNode> GetNextTasks(TaskNode v)
    {
        List<Node> temp = Adj(v);
        List<TaskNode> result = new();
        
        foreach (var u in temp)
        {
            if (u is TaskNode node)
            {
                result.Add(node);
            }
        }

        return result;
    }

    public List<TaskNode> GetTaskNodes(WorkflowNode u)
    {
        var adj = Adj(u);
        var temp = new List<TaskNode>();

        foreach(var j  in adj)
        {
            if (j is TaskNode node) temp.Add(node);
        }

        return temp;
    }

    public List<WorkflowNode> GetWorkflowNodes()
    {
        var temp = new List<WorkflowNode>();

        foreach(var pair in _adjLists)
        {
            if (pair.Key is WorkflowNode node) temp.Add(node);
        }

        return temp;
    }

    private void RemoveEdgeBase(Node u, Node v)
    {
        _adjLists[u].Remove(v);
    }

    public void RemoveEdge(TaskNode u, ResourceNode v)
    {
        RemoveEdgeBase(u, v);
        RemoveEdgeBase(v, u);
    }
    
    public void RemoveEdge(TaskNode u, DataNode v)
    {
        RemoveEdgeBase(u, v);
        RemoveEdgeBase(v, u);
    }

    public void SetLanguage(string langCode)
    {
        _lang = langCode;
        foreach(var pair in _adjLists)
        {
            pair.Key.SetLanguage(langCode);
        }
    }

    public string GetLanguage()
    {
        return _lang;
    }
}