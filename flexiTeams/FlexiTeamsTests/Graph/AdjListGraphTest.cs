using FlexiTeams.ConstructionClasses.Diretor;
using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Graph.Nodes;
using NUnit.Framework;


namespace FlexiTeamsTests.Graph
{
    [TestFixture]
    public class AdjListGraphTest
    {
        private const string path = "C:/Users/paul9/OneDrive/FlexiTeams/Resourcen/workflows.csv";
        private readonly AdjListsGraph graph = BasicGraphDirector.ConstructFromCsv(path);


        [Test]
        public void GetLongestPathTest()
        {
            var result = graph.GetLongestPath();

            Assert.AreEqual(12, result);
        }

        [Test]
        public void GetWorkflowNodeTest()
        {
            TaskNode tNode = graph.GetWorkflowNodes()[0].StartNode;
            WorkflowNode wNode = graph.GetWorkflowNode(tNode);

            Assert.AreEqual("General Surgery", wNode.Workflow.Type.ToString());

        }

        [Test]
        public void GetPreviousTasksTest()
        {
            var tNode = graph.GetWorkflowNodes()[0].StartNode;


            tNode = graph.GetNextTasks(tNode)[0];
            tNode = graph.GetNextTasks(tNode)[0];
            tNode = graph.GetNextTasks(tNode)[0];

            var prevNodes = graph.GetPrevTasks(tNode);

            Assert.AreEqual(prevNodes.Count, 2);
        }
    }
}
