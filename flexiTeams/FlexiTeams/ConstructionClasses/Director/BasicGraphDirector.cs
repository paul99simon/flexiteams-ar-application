using CsvHelper;
using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Task.Wrappper;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Graph.Nodes;
using System.Globalization;

namespace FlexiTeams.ConstructionClasses.Diretor;

public class BasicGraphDirector
{
    private static  Dictionary<string, Dictionary<int, TaskNode>> map = new();
    private static  Dictionary<string, TaskNode> tMap = new();
    private static  Dictionary<string, WorkflowNode> wMap = new();

    private static WorkflowNode currentNode = null;

    public static void ConstructFromCsv(string path, AdjListsGraph graph, IWorkflowBuilder wBuilder, ITaskBuilder tBuilder)
    {
        GetNodes(path, graph, wBuilder, tBuilder);
        GetEdges(path, graph);
        CalcProperties(graph);

        Reset();
    }
    public static AdjListsGraph ConstructFromCsv(string path)
    {
        var graph = new AdjListsGraph();
        var wBuilder = new BasicWorkflowBuilder();
        var tBuilder = new BasicTaskBuilder();

        GetNodes(path, graph, wBuilder, tBuilder);
        GetEdges(path, graph);
        CalcProperties(graph);

        Reset();
        return graph;
    }

    private static void Reset()
    {
        currentNode = null;
        map = new();
        tMap = new();
        wMap = new();
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

            private static void GetWorkflow(CsvReader reader, IWorkflowBuilder wBuilder, ITaskBuilder tBuilder,AdjListsGraph graph ,int wCount, int tCount)
            {

                //workflow properties
                string workflowId = "WORKFLOW_" + wCount;
                string type = reader.GetField(0);
                string venue = reader.GetField(2);

                wBuilder.Set(GetWorkflowId(workflowId));
                wBuilder.Set(GetTypes(type));
                wBuilder.Set(GetVenues(venue));

                //task properties
                string taskId = "TASK_" + tCount;
                string taskType = reader.GetField(5);
                string duration = reader.GetField(7); ;
                string professions = reader.GetField(13);
                string dataNames = reader.GetField(10);
                string taskNumber = reader.GetField(4);

                tBuilder.Set(GetTaskId(taskId));
                tBuilder.Set(GetTaskTypes(taskType));
                tBuilder.Set(GetDuration(duration));
                tBuilder.Set(GetProfessions(professions));
                tBuilder.Set(GetDataNames(dataNames));

                var w = wBuilder.GetWorkflow();
                var t = tBuilder.GetTask();
                var wNode = new WorkflowNode(w);
                var tNode = new TaskNode(t);
                graph.AddNode(wNode);
                graph.AddNode(tNode);

                wNode.StartNode = tNode;
                currentNode = wNode;

                map.Add(wNode.Workflow.Id.ToString(), new Dictionary<int, TaskNode>());
                map[wNode.Workflow.Id.ToString()].Add(int.Parse(taskNumber), tNode);

                tMap.Add(tNode.Task.Id.ToString(), tNode);
                wMap.Add(wNode.Workflow.Id.ToString(), wNode);
            }
            private static void GetTask(CsvReader reader, ITaskBuilder tBuilder, AdjListsGraph graph, int tCount)
            {
                //task properties
                string taskId = "TASK_" + tCount;
                string taskType = reader.GetField(5);
                string duration = reader.GetField(7); ;
                string professions = reader.GetField(13);
                string dataNames = reader.GetField(10);
                string taskNumber = reader.GetField(4);

                tBuilder.Set(GetTaskId(taskId));
                tBuilder.Set(GetTaskTypes(taskType));
                tBuilder.Set(GetDuration(duration));
                tBuilder.Set(GetProfessions(professions));
                tBuilder.Set(GetDataNames(dataNames));

                var t = tBuilder.GetTask();
                var tNode = new TaskNode(t);
                graph.AddNode(tNode);

                map[currentNode.Workflow.Id.ToString()].Add(int.Parse(taskNumber), tNode);
                tMap.Add(tNode.Task.Id.ToString(), tNode);
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
                private static Duration? GetDuration(string duration)
                {
                    if (duration.Equals("")) return null;
                    return new Duration(int.Parse(duration));
                }
                private static List<Profession> GetProfessions(string professions)
                {
                    var result = new List<Profession>();

                    string[] temp = professions.Split(',');

                    foreach (string s in temp)
                    {
                        s.Trim('"', ' ');

                        result.Add(new Profession(s));
                    }

                    return result;
                }
                private static List<DataName> GetDataNames(string dataNames)
                {
                    var result = new List<DataName>();

                    string[] temp = dataNames.Split(',');

                    foreach (string s in temp)
                    {
                        s.Trim('"', ' ');
                        result.Add(new DataName(s));
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
                workflowId = "WORKFLOW_" + wCount++;
                taskId = "TASK_" + tCount++;

                WorkflowNode wNode = wMap[workflowId];
                TaskNode tNode = tMap[taskId];

                graph.AddEdge(tNode, wNode);

                string nextTask = reader.GetField(8);
                string[] nextTasks = nextTask.Split(',');

                for (int i = 0; i < nextTasks.Length; i++)
                {
                    nextTasks[i] = nextTasks[i].Trim('"', ' ');
                    int taskNumber = int.Parse(nextTasks[i]);

                    graph.AddEdge(tNode, map[workflowId][taskNumber]);
                }

            }
            else
            {
                taskId = "TASK_" + tCount++;

                WorkflowNode wNode = wMap[workflowId];
                TaskNode tNode = tMap[taskId];

                graph.AddEdge(tNode, wNode);

                string nextTask = reader.GetField(8);
                string[] nextTasks = nextTask.Split(',');

                for (int i = 0; i < nextTasks.Length; i++)
                {
                    nextTasks[i] = nextTasks[i].Trim('"', ' ');

                    if (!string.IsNullOrEmpty(nextTasks[i]))
                    {
                        int taskNumber = int.Parse(nextTasks[i]);
                        graph.AddEdge(tNode, map[workflowId][taskNumber]);
                    }

                }
            }
        }
    }

    private static void CalcProperties(AdjListsGraph graph)
    {
        List<WorkflowNode> wNodes = graph.GetWorkflowNodes();

        foreach(var wNode in wNodes)
        {
            List<TaskNode> tNodes = graph.GetTaskNodes(wNode);
            var p = new Procedures(0);
            Duration d = new(0);
            foreach(var tNode in tNodes)
            {
                p++;

                if (wNode.Workflow.Venue != null)
                {
                    tNode.Task.Venue = wNode.Workflow.Venue;
                }
                d += tNode.Task.Duration;
            }

            wNode.Workflow.Procedures = p;
            wNode.Workflow.Duration = d; 
        }
    }
}