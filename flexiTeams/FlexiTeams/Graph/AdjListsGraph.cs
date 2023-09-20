using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Exceptions;
using FlexiTeams.Graph.Nodes;
using FlexiTeams.Util.EqualityComperator;

namespace FlexiTeams.FlexiTeamsGraph;

//Graph class is a directed Graph as shown in the Paper:
//"FlexiTeam: Flexible Team and Work Organization using Process-Oriented Case-Based Reasoning"

public class AdjListsGraph 
{
    private readonly Dictionary<Node, List<Node>> _adjLists = new ();
    private readonly Dictionary<Id, Node> map = new (new AbstractIdEqualityComperator());

    //FlexiTeams graph methods
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
        map.Add(v._id, v);
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
    
    public List<Node> Nodes()
    {
        List<Node> nodes = new();

        foreach(var u in _adjLists)
        {
            nodes.Add(u.Key);
        }

        return nodes;
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
    public List<TaskNode> GetPrevTasks(TaskNode v)
    {
        WorkflowNode wNode = GetWorkflowNode(v);

        List<TaskNode> taskNodes = GetTaskNodes(wNode);

        List<TaskNode> prevTasks = new();

        foreach(var u in taskNodes)
        {
            if(GetNextTasks(u).Contains(v)) prevTasks.Add(u);
        }

        return prevTasks;
    }

    public WorkflowNode? GetWorkflowNode(TaskNode v)
    {
        var adj = Adj(v);
        foreach (var j in adj)
        {
            if (j is WorkflowNode node) return node;
        }

        return null;
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

    public Node? FindNode(Id id)
    {
        if(map.ContainsKey(id)) return map[id];
        return null;
    }

    /// <summary>
    /// returns the <see cref="TaskNode"/>'s from a <see cref="WorkflowNode"/> that make up the longestPath
    /// The taskNodes are stored inside a List <see cref="List{T}"/> and orderd in Order of traversing time.
    /// </summary>
    /// <param name="wNode">represents a WorkflowNode <see cref="WorkflowNode"/></param>
    /// <returns></returns>
    public List<TaskNode> GetLongestPath(WorkflowNode wNode)
    {
        if(wNode.StartNode == null) return new List<TaskNode>();
        return GetLongestPathRecursive(wNode.StartNode);
    }
    private List<TaskNode> GetLongestPathRecursive(TaskNode taskNode)
    {
        List<TaskNode> nextTasks = GetNextTasks(taskNode);

        if (nextTasks.Count == 0) return new List<TaskNode>() { taskNode };
        if (nextTasks.Count == 1)
        {
            var result = new List<TaskNode>() {taskNode };
            var nextTask = nextTasks[0];
            
            result.AddRange(GetLongestPathRecursive(nextTask));
            return result;
        };
        if (nextTasks.Count > 1)
        {
            var result = new List<TaskNode>() { taskNode };
            var temp = new List<TaskNode>();
            int max = 0;            

            nextTasks.ForEach(taskNode =>
            {
                var longestPath = GetLongestPathRecursive(taskNode);
                if (longestPath.Count > max)
                {
                    temp = longestPath;
                    max = longestPath.Count;
                }
            });

            result.AddRange(temp);
            return result;
        }

        return null;
    }
}