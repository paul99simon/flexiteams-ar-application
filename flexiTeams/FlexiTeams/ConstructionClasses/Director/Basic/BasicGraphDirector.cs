using CsvHelper;
using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Builder.Interface;
using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Task.Wrapper;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Graph.Nodes;
using FlexiTeams.Inventory;
using FlexiTeams.Util.EqualityComperator;
using System.Globalization;

namespace FlexiTeams.ConstructionClasses.Director.Basic;

[Obsolete]
public class BasicGraphDirector
{
    private static Dictionary<WorkflowId, Dictionary<int, TaskNode>> map = new(new WorkflowIdEqualityComparer());
    private static Dictionary<TaskId, TaskNode> tMap = new(new TaskIdEqualityComparer());
    private static Dictionary<WorkflowId, WorkflowNode> wMap = new(new WorkflowIdEqualityComparer());

    private static WorkflowPool _wPool;
    private static TaskPool _tPool;

    private static WorkflowNode currentNode = null;

    public static void ConstructFromCsv(string path, AdjListsGraph graph, WorkflowPool wPool, TaskPool tPool, IWorkflowBuilder wBuilder, ITaskBuilder tBuilder)
    {
        _wPool = wPool;
        _tPool = tPool;
        GetNodes(path, graph, wBuilder, tBuilder);
        GetEdges(path, graph);
        CalcProperties(graph);

        Reset();
    }
    public static AdjListsGraph ConstructFromCsv(string path, WorkflowPool wPool, TaskPool tPool)
    {
        var graph = new AdjListsGraph();
        var wBuilder = new BasicWorkflowBuilder();
        var tBuilder = new BasicTaskBuilder();

        _wPool = wPool;
        _tPool = tPool;

        GetNodes(path, graph, wBuilder, tBuilder);
        GetEdges(path, graph);
        CalcProperties(graph);

        Reset();
        return graph;
    }

    private static void Reset()
    {
        currentNode = null;
        map = new(new WorkflowIdEqualityComparer());
        tMap = new(new TaskIdEqualityComparer());
        wMap = new(new WorkflowIdEqualityComparer());
        _wPool = null;
        _tPool = null;
    }


    private static void GetNodes(string path, AdjListsGraph graph, IWorkflowBuilder wBuilder, ITaskBuilder tBuilder)
    {
        int wCount = 0;
        int tCount = 0;

        using var streamReader = new StreamReader(path);
        using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        reader.Read();
        reader.Read();
        while (reader.Read())
        {
            string temp = reader.GetField(0);

            if (!temp.Equals("")) GetWorkflow(reader, wBuilder, tBuilder, graph, wCount++, tCount++);
            else GetTask(reader, tBuilder, graph, tCount++);
        }


    }

    private static void GetWorkflow(CsvReader reader, IWorkflowBuilder wBuilder, ITaskBuilder tBuilder, AdjListsGraph graph, int wCount, int tCount)
    {

        //workflow properties
        string workflowId = "Workflow_" + wCount;
        string type = reader.GetField(0);
        string venue = reader.GetField(2);

        wBuilder.Set(GetWorkflowId(workflowId));
        wBuilder.Set(GetTypes(type));
        wBuilder.Set(GetVenues(venue));

        //task properties
        string taskId = "Task_" + tCount;
        string taskType = reader.GetField(5);
        string duration = reader.GetField(7); ;
        string professions = reader.GetField(12);
        string dataNames = reader.GetField(9);
        string taskNumber = reader.GetField(4);

        tBuilder.Set(GetTaskId(taskId));
        tBuilder.Set(GetTaskTypes(taskType));
        tBuilder.Set(GetDuration(duration));
        tBuilder.Set(GetProfessions(professions));
        tBuilder.Set(GetDataNames(dataNames));

        var w = wBuilder.GetWorkflow();
        var t = tBuilder.GetTask();
        var wNode = new WorkflowNode(w.Id);

        _wPool.Add(w);
        _tPool.Add(t);

        var tNode = new TaskNode(t.Id);
        graph.AddNode(wNode);
        graph.AddNode(tNode);

        wNode.StartNodeId = tNode.Id;
        currentNode = wNode;

        map.Add(wNode.Id, new Dictionary<int, TaskNode>());
        map[wNode.Id].Add(int.Parse(taskNumber), tNode);

        tMap.Add(tNode.Id, tNode);
        wMap.Add(wNode.Id, wNode);
    }
    private static void GetTask(CsvReader reader, ITaskBuilder tBuilder, AdjListsGraph graph, int tCount)
    {
        //task properties
        string taskId = "Task_" + tCount;
        string taskType = reader.GetField(5);
        string duration = reader.GetField(7); ;
        string professions = reader.GetField(12);
        string dataNames = reader.GetField(9);
        string taskNumber = reader.GetField(4);

        tBuilder.Set(GetTaskId(taskId));
        tBuilder.Set(GetTaskTypes(taskType));
        tBuilder.Set(GetDuration(duration));
        tBuilder.Set(GetProfessions(professions));
        tBuilder.Set(GetDataNames(dataNames));

        var t = tBuilder.GetTask();
        var tNode = new TaskNode(t.Id);

        _tPool.Add(t);

        graph.AddNode(tNode);

        map[currentNode.Id].Add(int.Parse(taskNumber), tNode);
        tMap.Add(tNode.Id, tNode);
    }

    private static WorkflowId GetWorkflowId(string workflowId)
    {
        return new WorkflowId(workflowId);
    }
    private static WorkflowType GetTypes(string type)
    {
        return new WorkflowType(type);
    }
    private static Venue GetVenues(string venue)
    {
        return new Venue(venue);
    }
    private static TaskId GetTaskId(string taskId)
    {
        return new TaskId(taskId);
    }
    private static TaskType GetTaskTypes(string type)
    {
        return new TaskType(type);
    }
    private static int GetDuration(string duration)
    {
        if (duration.Equals("")) return 0;
        return int.Parse(duration);
    }
    private static List<Profession> GetProfessions(string professions)
    {
        var result = new List<Profession>();

        string[] temp = professions.Split(',');

        foreach (string s in temp)
        {
            string s2 = s.Trim('"', ' ');

            result.Add(new Profession(s2));
        }

        return result;
    }
    private static List<DataName> GetDataNames(string dataNames)
    {
        var result = new List<DataName>();

        string[] temp = dataNames.Split(',');

        foreach (string s in temp)
        {
            string s2 = s.Trim('"', ' ');
            result.Add(new DataName(s2));
        }

        return result;
    }

    private static void GetEdges(string path, AdjListsGraph graph)
    {
        int wCount = 0;
        int tCount = 0;

        string workflowId = "";
        using var streamReader = new StreamReader(path);
        using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        reader.Read();
        reader.Read();
        while (reader.Read())
        {
            string temp = reader.GetField(0);

            string taskId;
            if (!temp.Equals(""))
            {
                workflowId = "Workflow_" + wCount++;
                taskId = "Task_" + tCount++;

                WorkflowNode wNode = wMap[new WorkflowId(workflowId)];
                TaskNode tNode = tMap[new TaskId(taskId)];

                graph.AddEdge(tNode, wNode);

                string nextTask = reader.GetField(8);
                string[] nextTasks = nextTask.Split(',');

                for (int i = 0; i < nextTasks.Length; i++)
                {
                    nextTasks[i] = nextTasks[i].Trim('"', ' ');
                    int taskNumber = int.Parse(nextTasks[i]);

                    graph.AddEdge(tNode, map[new WorkflowId(workflowId)][taskNumber]);
                }

            }
            else
            {
                taskId = "Task_" + tCount++;

                WorkflowNode wNode = wMap[new WorkflowId(workflowId)];
                TaskNode tNode = tMap[new TaskId(taskId)];

                graph.AddEdge(tNode, wNode);

                string nextTask = reader.GetField(8);
                string[] nextTasks = nextTask.Split(',');

                for (int i = 0; i < nextTasks.Length; i++)
                {
                    nextTasks[i] = nextTasks[i].Trim('"', ' ');

                    if (!string.IsNullOrEmpty(nextTasks[i]))
                    {
                        int taskNumber = int.Parse(nextTasks[i]);
                        graph.AddEdge(tNode, map[new WorkflowId(workflowId)][taskNumber]);
                    }

                }
            }
        }
    }

    private static void CalcProperties(AdjListsGraph graph)
    {
        List<WorkflowNode> wNodes = graph.GetWorkflowNodes();

        foreach (var wNode in wNodes)
        {
            List<TaskNode> tNodes = graph.GetTaskNodes(wNode);
            var p = new Procedures(0);
            int min = 0;
            foreach (var tNode in tNodes)
            {
                p++;

                var venue = _wPool[wNode.Id].Venue;
                if (venue != null)
                {
                    _tPool[tNode.Id].Venue = venue;
                }
                min += _tPool[tNode.Id].Minutes;
            }

            _wPool[wNode.Id].Procedures = p;
            _wPool[wNode.Id].Minutes = min;
        }
    }
}