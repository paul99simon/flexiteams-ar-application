using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Task.Wrapper;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Graph.Nodes;
using FlexiTeams.Inventory;
using FlexiTeams.Util;
using FlexiTeams.Util.EqualityComperator;

namespace FlexiTeams.FlexiTeamsGraph;

//Graph class is a directed Graph as shown in the Paper:
//"FlexiTeam: Flexible Team and Work Organization using Process-Oriented Case-Based Reasoning"

public class AdjListsGraph 
{
    private readonly Dictionary<Node, List<Node>> _adjLists = new ();
    private readonly Dictionary<Id, Node> map = new (new AbstractIdEqualityComperator());

    public List<Node> Nodes { 
        get
        {
            List<Node> nodes = new();
            foreach (var u in _adjLists) { nodes.Add(u.Key); }
            return nodes;
        }
    }

    //FlexiTeams graph methods

    public void AddNode(Node v)
    {
        map.Add(v._id, v);
        _adjLists.Add(v, new List<Node>());
    }

    public void AddEdge(Node v, Node u)
    {
        if(!_adjLists.ContainsKey(v)) AddNode(v);
        if(!_adjLists.ContainsKey(u)) AddNode(u);
        
        _adjLists[v].Add(u);
    }

    public void RemoveEdge(Node u, Node v)
    {
        _adjLists[u].Remove(v);
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
        WorkflowNode wNode = null;
        foreach (var j in adj)
        {
            if (j is WorkflowNode node) wNode =  node;
        }

        return wNode;
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


    public Node? FindNode(Id id)
    {
        if(map.ContainsKey(id)) return map[id];
        return null;
    }
    public WorkflowNode? FindNode(WorkflowId id)
    {
        if (map.ContainsKey(id)) return (WorkflowNode) map[id];
        return null;
    }
    public TaskNode? FindNode(TaskId id)
    {
        if (map.ContainsKey(id)) return (TaskNode) map[id];
        return null;
    }
    public DataNode? FindNode(DataId id) 
    {
        if (map.ContainsKey(id)) return (DataNode) map[id];
        return null;
    }
    public ResourceNode? FindNode(ResourceId id)
    {
        if (map.ContainsKey(id)) return (ResourceNode)map[id];
        return null;
    }

    /// <summary>
    /// returns the <see cref="TaskNode"/>'s from a <see cref="WorkflowNode"/> that make up the longestPath
    /// The taskNodes are stored inside a List <see cref="List{T}"/> and orderd in Order of traversing time.
    /// </summary>
    /// <param name="wNode">represents a WorkflowNode <see cref="WorkflowNode"/></param>
    /// <returns></returns>
    public List<TaskNode>? GetLongestPath(WorkflowNode wNode)
    {
        if(wNode.StartNodeId == null) return null;
        if (map[wNode.StartNodeId]is TaskNode tNode)
        {
            return GetLongestPathRecursive(tNode);
        }
        return null;
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

    public List<TaskNode>? GetLongestDurationPath(WorkflowNode wNode, TaskPool taskPool)
    {
        if (wNode.StartNodeId == null) return null;
        if (map[wNode.StartNodeId] is TaskNode tNode)
        {
            return GetLongestDurationPathRecursive(tNode, taskPool);
        }
        return null;
    }
    private List<TaskNode> GetLongestDurationPathRecursive(TaskNode taskNode, TaskPool taskPool)
    {
        List<TaskNode> nextTasks = GetNextTasks(taskNode);

        if (nextTasks.Count == 0) return new List<TaskNode>() { taskNode };
        if (nextTasks.Count == 1)
        {
            var result = new List<TaskNode>() { taskNode };
            var nextTask = nextTasks[0];

            result.AddRange(GetLongestPathRecursive(nextTask));
            return result;
        };
        if (nextTasks.Count > 1)
        {
            var result = new List<TaskNode>() { taskNode };
            var temp = new List<TaskNode>();
            DateTime max = DateTime.MinValue;

            nextTasks.ForEach(taskNode =>
            {
                var longestPath = GetLongestPathRecursive(taskNode);
                var currentDuration = GetPathDuration(longestPath, taskPool);
                if (currentDuration > max)
                {
                    temp = longestPath;
                    max = currentDuration;
                }
            });

            result.AddRange(temp);
            return result;
        }

        return null;
    }

    public DateTime GetPathDuration(List<TaskNode> tasks, TaskPool taskPool)
    {
        DateTime result = new();

        tasks.ForEach(taskNode =>
        {
            result += taskPool[taskNode.Id].end - taskPool[taskNode.Id].begin;
        });

        return result;
    }
}