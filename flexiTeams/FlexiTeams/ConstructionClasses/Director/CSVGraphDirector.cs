using CsvHelper;
using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Task.Wrappper;
using FlexiTeams.DataClasses.Workflow;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Graph.Nodes;
using System.Globalization;
using System.Runtime;

namespace FlexiTeams.ConstructionClasses.Diretor;

public class CSVGraphDirector
{
    private const string _lang = "en";

    static Dictionary<string, Dictionary<int, TaskNode>> map = new();
    static Dictionary<string, TaskNode> tMap = new();
    static Dictionary<string, WorkflowNode> wMap = new();

    static WorkflowNode currentNode = null;

    public static void ConstructFromCsv(string path, AdjListsGraph graph, IWorkflowBuilder wBuilder, ITaskBuilder tBuilder)
    {
        GetNodes(path, graph, wBuilder, tBuilder);
        GetEdges(path, graph);
        CalcProperties(graph);

        currentNode = null;
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
                wBuilder.SetLanguage(_lang);

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
                tBuilder.SetLanguage(_lang);

                var w = wBuilder.GetWorkflow();
                var t = tBuilder.GetTask();
                var wNode = new WorkflowNode(w);
                var tNode = new TaskNode(t);
                graph.AddNode(wNode);
                graph.AddNode(tNode);

                wNode.StartNodes.Add(tNode);
                currentNode = wNode;

                map.Add(wNode.Workflow.Id.Get, new Dictionary<int, TaskNode>());
                map[wNode.Workflow.Id.Get].Add(int.Parse(taskNumber), tNode);

                tMap.Add(tNode.Task.Id.Get, tNode);
                wMap.Add(wNode.Workflow.Id.Get, wNode);
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

                map[currentNode.Workflow.Id.Get].Add(int.Parse(taskNumber), tNode);
                tMap.Add(tNode.Task.Id.Get, tNode);
            }

                private static WorkflowId GetWorkflowId(string workflowId)
                {
                    return new WorkflowId(workflowId);
                }
                private static Dictionary<string, WorkflowType> GetTypes(string type)
                {
                    var temp = new Dictionary<string, WorkflowType>();

                    temp.Add(_lang, new WorkflowType(type));

                    return temp;
                }
                private static Dictionary<string, Venue> GetVenues(string venue)
                {
                    var temp = new Dictionary<string, Venue>();

                    temp.Add(_lang, new Venue(venue));

                    return temp;
                }
                private static TaskId GetTaskId(string taskId)
                {
                    return new TaskId(taskId);
                }
                private static Dictionary<string, TaskType> GetTaskTypes(string type)
                {
                    Dictionary<string, TaskType> temp = new();

                    temp.Add(_lang, new TaskType(type));

                    return temp;
                }
                private static Duration GetDuration(string duration)
                {
                    if (duration.Equals("")) return null;
                    return new Duration(int.Parse(duration));
                }
                private static Dictionary<string, List<Profession>> GetProfessions(string professions)
                {
                    Dictionary<string, List<Profession>> result = new();

                    string[] temp = professions.Split(',');

                    foreach (string s in temp)
                    {
                        s.Trim('"', ' ');

                        if (!result.ContainsKey(_lang)) result.Add(_lang, new List<Profession>());
                        result[_lang].Add(new Profession(s));
                    }

                    return result;
                }
                private static Dictionary<string, List<DataName>> GetDataNames(string dataNames)
                {
                    Dictionary<string, List<DataName>> result = new();

                    string[] temp = dataNames.Split(',');

                    foreach (string s in temp)
                    {
                        s.Trim('"', ' ');

                        if (!result.ContainsKey(_lang)) result.Add(_lang, new List<DataName>());
                        result[_lang].Add(new DataName(s));
                    }

                    return result;
                }


    private static void GetEdges(string path, AdjListsGraph graph)
    {
        int wCount = 0;
        int tCount = 0;

        string workflowId = "";
        string taskId = "";

        using var streamReader = new StreamReader(path);
        using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        reader.Read();
        reader.Read();
        while (reader.Read())
        {
            string temp = reader.GetField(0);

            if (!temp.Equals(""))
            {
                workflowId = "WORKFLOW_" + wCount++;
                taskId = "TASK_" + tCount++;

                WorkflowNode wNode = wMap[workflowId];
                TaskNode tNode = tMap[taskId];

                graph.AddEdge(tNode, wNode);

                string nextTask = reader.GetField(8);
                string[] nextTasks = nextTask.Split(',');

                for(int i = 0;i < nextTasks.Length; i++)
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

                    if (! string.IsNullOrEmpty(nextTasks[i]))
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
            Duration d = new Duration(0);
            foreach(var tNode in tNodes)
            {

                p++;
                tNode.Task.Add(_lang, wNode.Workflow.Venue);
                d += tNode.Task.Duration;
            }

            wNode.Workflow.Procedures = p;
            wNode.Workflow.Duration = d; 
        }
    }
}