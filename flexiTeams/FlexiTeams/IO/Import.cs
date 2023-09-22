using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Task.Wrapper;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Graph.Nodes;
using FlexiTeams.Inventory;
using System.Xml.Linq;

namespace FlexiTeams.IO
{
    public class Import
    {

        public ResourcePool ResourcePool = new();
        public DataPool DataPool = new();
        public WorkflowPool WorkflowPool = new();
        public TaskPool TaskPool = new();
        public AdjListsGraph Graph = new();

        private ImportSettings _importSettings = new();

        public Import(string path)
        {
            XDocument document = XDocument.Load(path);

            GetDataPool(document.Descendants("DataPool").First());
            GetWorkflowPool(document.Descendants("WorkflowPool").First());
            GetTaskPool(document.Descendants("TaskPool").First());
            GetGraph(document.Descendants("Graph").First());
        }

        public Import(string path, ImportSettings settings) {
            
            _importSettings = settings;
        }

        public Import(XDocument document)
        {

        }

        public Import(XDocument document, ImportSettings settings)
        {
            _importSettings = settings; 
        }

        private  void GetResourcePool()
        {
            
        }

        private void GetDataPool(XElement dataPoolNode) {

            var nodes = dataPoolNode.Descendants("Data");

            foreach (var node in nodes)
            {
                var data = _importSettings.DataDirector.Construct(node, _importSettings.DataBuilder);
                DataPool.Add(data);
            }
        }

        private void GetWorkflowPool(XElement workflowPoolNode) {

            var nodes = workflowPoolNode.Descendants("Workflow");

            foreach (var node in nodes)
            {
                var workflow = _importSettings.WorkflowDirector.Construct(node, _importSettings.WorkflowBuilder);
                WorkflowPool.Add(workflow);
            }
        }

        private void GetTaskPool(XElement taskPoolNode) {
            
            var nodes = taskPoolNode.Descendants("Task");

            foreach (var node in nodes)
            {
                var task = _importSettings.TaskDirector.Construct(node, _importSettings.TaskBuilder);
                TaskPool.Add(task);
            }
        }

        private void GetGraph(XElement graphNode) {
            
            var nodesNode = graphNode.Descendants("Nodes").First();
            var edgesNode = graphNode.Descendants("Edges").First();

            GetNodes(nodesNode);
            GetEdges(edgesNode);
        }

        private void GetNodes(XElement nodesNode)
        {
            var wNodes = nodesNode.Descendants("WorkflowNode");
            var tNodes = nodesNode.Descendants("TaskNode");
            var rNodes = nodesNode.Descendants("ResourceNode");
            var dNodes = nodesNode.Descendants("DataNode");

            foreach (var node in wNodes)
            {
                var id = new WorkflowId(node.Attribute("ref").Value);
                var wNode = new WorkflowNode(id);
                Graph.AddNode(wNode);
            }

            foreach (var node in tNodes)
            {
                var id = new TaskId(node.Attribute("ref").Value);
                var tNode = new TaskNode(id);
                Graph.AddNode(tNode);
            }

            foreach(var node in rNodes)
            {
                var id = new ResourceId(node.Attribute("ref").Value);
                var rNode = new ResourceNode(id);
                Graph.AddNode(rNode);
            }

            foreach(var node in dNodes)
            {
                var id = new DataId(node.Attribute("ref").Value);
                var dNode = new DataNode(id);
                Graph.AddNode(dNode);
            }
        }

        private void GetEdges(XElement edgesNode)
        {
            var edgeNodes = edgesNode.Descendants("Edge");

            foreach (var edge in edgeNodes)
            {
                string ref1 = edge.Attribute("ref1").Value;
                string ref2 = edge.Attribute("ref2").Value;

                var node1 = Graph.FindNode(new Id(ref1));
                var Node2 = Graph.FindNode(new Id(ref2));

                Graph.AddEdge(node1, Node2);
            }
        }
    }
}
