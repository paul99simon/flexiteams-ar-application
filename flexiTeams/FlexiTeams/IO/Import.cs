using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Task.Wrapper;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Graph.Nodes;
using FlexiTeams.Inventory;
using System.Xml.Linq;
using System.Xml.Schema;

namespace FlexiTeams.IO
{
    public class Import
    {
        public ResourcePool ResourcePool = new();
        public DataPool DataPool = new();
        public WorkflowPool WorkflowPool = new();
        public TaskPool TaskPool = new();
        public AdjListsGraph Graph = new();

        private readonly ImportSettings _importSettings = new();

        /// <summary>
        /// Import class which holds the following Datastructures <see cref="ResourcePool"/>, <see cref="DataPool"/>, <see cref="WorkflowPool"/>, <see cref="TaskPool"/>./>
        /// Import class takes the xml file path and validates it against the schemas <see cref="XmlSchemaSet"/> specified in the xml file.
        /// <see cref="ImportSettings"/> specifies the Builder and Director classes for the Datastructures
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="XmlSchemaValidationException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="FileNotFoundException"/>
        /// <exception cref="XmlSchemaException"
        public Import(string path)
        {
            XDocument document = Validation.Validate(path);
            Create(document);
        }

        /// <summary>
        /// Import class which holds the following Datastructures <see cref="ResourcePool"/>, <see cref="DataPool"/>, <see cref="WorkflowPool"/>, <see cref="TaskPool"/>./>
        /// Import class takes the xml file path and validates it against the schemas <see cref="XmlSchemaSet"/> specified in the xml file.
        /// <see cref="ImportSettings"/> specifies the Builder and Director classes for the Datastructures
        /// </summary>
        /// <param name="path"></param>
        /// <param name="settings"></param>
        /// <exception cref="XmlSchemaValidationException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="FileNotFoundException"/>
        /// <exception cref="XmlSchemaException"
        public Import(string path, ImportSettings settings) {
            _importSettings = settings;
            XDocument document = Validation.Validate(path);
            Create(document);
        }

        /// <summary>
        /// Import class which holds the following Datastructures <see cref="ResourcePool"/>, <see cref="DataPool"/>, <see cref="WorkflowPool"/>, <see cref="TaskPool"/>./>
        /// Import class takes a <see cref="XDocument"/> and validates it against a <see cref="XmlSchemaSet"/>
        /// for this method the client is responsible for matching schemas
        /// </summary>
        /// <param name="document"></param>
        /// <param name="schemaSet"></param>
        /// <exception cref="XmlSchemaValidationException"/>
        public Import(XDocument document, XmlSchemaSet schemaSet)
        {
            Validation.Validate(document, schemaSet);
            Create(document);
        }

        /// <summary>
        /// Import class which holds the following Datastructures <see cref="ResourcePool"/>, <see cref="DataPool"/>, <see cref="WorkflowPool"/>, <see cref="TaskPool"/>./>
        /// Import class takes a <see cref="XDocument"/> and validates it against a <see cref="XmlSchemaSet"/>
        /// <see cref="ImportSettings"/> specifies the Builder and Director classes for the Datastructures
        /// for this method the client is responsible for matching schemas
        /// </summary>
        /// <param name="document"></param>
        /// <param name="schemaSet"></param>
        /// <param name="settings"></param>
        /// <exception cref="XmlSchemaValidationException"/>
        public Import(XDocument document, XmlSchemaSet schemaSet, ImportSettings settings)
        {
            Validation.Validate(document, schemaSet);
            _importSettings = settings; 
            Create(document);
        }

        private void Create(XDocument document) {
            
            GetResourcePool(document.Descendants("ResourcePool").First());
            GetDataPool(document.Descendants("DataPool").First());
            GetWorkflowPool(document.Descendants("WorkflowPool").First());
            GetTaskPool(document.Descendants("TaskPool").First());
            GetGraph(document.Descendants("Graph").First());
        }

        private  void GetResourcePool(XElement resourcePoolNode)
        {
            var nodes = resourcePoolNode.Descendants("Resource");

            foreach (var node in nodes)
            {
                var resource = _importSettings.ResourceDirector.Construct(node, _importSettings.ResourceBuilder);
                ResourcePool.Add(resource);
            }
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
                var id = new WorkflowId(node.Attribute("idref").Value);
                var startID = node.Attribute("startNode").Value;
                var wNode = new WorkflowNode(id)
                {
                    StartNodeId = new TaskId(startID)
                };
                Graph.AddNode(wNode);
            }

            foreach (var node in tNodes)
            {
                var id = new TaskId(node.Attribute("idref").Value);
                var tNode = new TaskNode(id);
                Graph.AddNode(tNode);
            }

            foreach(var node in rNodes)
            {
                var id = new ResourceId(node.Attribute("idref").Value);
                var rNode = new ResourceNode(id);
                Graph.AddNode(rNode);
            }

            foreach(var node in dNodes)
            {
                var id = new DataId(node.Attribute("idref").Value);
                var dNode = new DataNode(id);
                Graph.AddNode(dNode);
            }
        }

        private void GetEdges(XElement edgesNode)
        {
            var edgeNodes = edgesNode.Descendants("Edge");

            foreach (var edge in edgeNodes)
            {
                string ref1 = edge.Attribute("idref1").Value;
                string ref2 = edge.Attribute("idref2").Value;

                var node1 = Graph.FindNode(new Id(ref1));
                var Node2 = Graph.FindNode(new Id(ref2));

                Graph.AddEdge(node1, Node2);
            }
        }
    }
}
