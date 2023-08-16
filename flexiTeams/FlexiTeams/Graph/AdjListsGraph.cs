using FlexiTeams.Graph.Nodes;

namespace FlexiTeams.FlexiTeamsGraph;

//Graph class is a directed Graph as shown in the Paper:
//"FlexiTeam: Flexible Team and Work Organization using Process-Oriented Case-Based Reasoning"

public class AdjListsGraph
{
    private readonly Dictionary<Node, List<Node>> _adjLists = new ();
    
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

    public void AddEdge(ResourceNode v, TaskNode u)
    {
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
}