using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Exceptions;
using FlexiTeams.Graph.Nodes;
using FlexiTeams.Util;
using FlexiTeams.Util.EqualityComperator;
using System.Net.WebSockets;
using System.Xml.Linq;

namespace FlexiTeams.FlexiTeamsGraph;

//Graph class is a directed Graph as shown in the Paper:
//"FlexiTeam: Flexible Team and Work Organization using Process-Oriented Case-Based Reasoning"

public class AdjListsGraph : ILanguageObject
{

    private readonly Dictionary<Node, List<Node>> _adjLists = new ();
    private string _lang = "";
    

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
        _adjLists.Add(v, new List<Node>());
    }

    public void AddEdge(TaskNode v, TaskNode u)
    {
        AddEdgeBase(v, u);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="v">ResourceNode <see cref="ResourceNode"/></param>
    /// <param name="u">TaskNode<see cref="TaskNode"/></param>
    /// <param name="specifiedProfession"></param>
    /// <exception cref="HasNotRequiredProfessionException"></exception>
    /// <exception cref="TaskAlreadySuffentlyStaffedWithProfessionException"></exception>
    /// <exception cref="TaskDoesntNeedThisProfessionException"></exception>
    public void AddEdge(ResourceNode v, TaskNode u, Profession specifiedProfession)
    {
        //Checks if the TaskNode needs the specifiedProfession
        if(! TaskNeedsSpecifiedProfession()) throw new TaskDoesntNeedThisProfessionException(u.Task, specifiedProfession);
        if(TaskIsSufficentlyStaffedWithProfession()) throw new TaskAlreadySuffentlyStaffedWithProfessionException(u.Task, specifiedProfession);
        if(! ResourceHasSpecifiedProfession()) throw new HasNotRequiredProfessionException(v.Resource, u.Task, specifiedProfession);
         
        AddEdgeBase(v, u);
        AddEdgeBase(u, v);

        bool TaskNeedsSpecifiedProfession()
        {
            var comp = new ProfessionEqualityComperator();
            return u.Task.RequiredProfessions.Contains(specifiedProfession, comp);
        }
        bool TaskIsSufficentlyStaffedWithProfession()
        {
            var comp = new ProfessionEqualityComperator();
            Dictionary<Profession, int> requiredProfessions = new(comp);

            u.Task.RequiredProfessions.ForEach(profession =>
            {
                if (!requiredProfessions.ContainsKey(profession)) requiredProfessions.Add(profession, 0);
                requiredProfessions[profession]++;
            });

            Dictionary<Profession, int> allocatedResources = new(comp);
            
            foreach(var pair in u.ResourceAllocation)
            {
                if(pair.Value != null)
                {
                    if (allocatedResources.ContainsKey(pair.Key)) allocatedResources.Add(pair.Key, 0);
                    allocatedResources[pair.Key]++;
                }
            }

            return allocatedResources[specifiedProfession] < requiredProfessions[specifiedProfession];
        }
        bool ResourceHasSpecifiedProfession()
        {
            bool hasProfession = false;

            foreach (var profession in v.Resource.Professions)
            {
                if (specifiedProfession.Equals(profession)) hasProfession = true;
            }

            return hasProfession;
        }
        void UpdateResourceAllocation()
        {

        }
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

    //Ilanguage Object
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

    //utility methods
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