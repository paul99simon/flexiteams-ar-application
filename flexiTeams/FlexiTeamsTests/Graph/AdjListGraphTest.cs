using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Graph.Nodes;
using FlexiTeams.Inventory;
using FlexiTeams.IO;
using NUnit.Framework;

namespace FlexiTeamsTests.Graph
{
    [TestFixture]
    public class AdjListGraphTest
    {
        private const string path = "C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/importTest2.xml";
        Import import = new Import(path);

        [Test]
        public void GetLongestPathTest()
        {
            TaskPool tPool = import.TaskPool;
            AdjListsGraph graph = import.Graph;

            var result = graph.GetLongestPath(graph.GetWorkflowNodes()[0]);

            Assert.AreEqual(12, result.Count);
        }

        [Test]
        public void GetLongestDurationPathTest()
        {
            TaskPool tPool = import.TaskPool;
            AdjListsGraph graph = import.Graph;

            var result = graph.GetLongestDurationPath(graph.GetWorkflowNodes()[0], tPool);

            Assert.AreEqual("07:45", graph.GetPathDuration(result, tPool).ToString("HH:mm"));
        }

        [Test]
        public void GetWorkflowNodeTest()
        {
            var import = new Import(path);
            var wPool = import.WorkflowPool;
            var graph = import.Graph;

            if(graph.FindNode(graph.GetWorkflowNodes()[0].StartNodeId) is TaskNode tNode)
            {
                WorkflowNode wNode = graph.GetWorkflowNode(tNode);

                Assert.AreEqual("General Surgery", wPool[wNode.Id].Type.ToString());
                return;
            }
            Assert.Fail();
        }

        [Test]
        public void GetPreviousTasksTest()
        {
            var import = new Import(path);

            var graph = import.Graph;
            if(graph.FindNode(graph.GetWorkflowNodes()[0].StartNodeId) is TaskNode tNode)
            {
                tNode = graph.GetNextTasks(tNode)[0];
                tNode = graph.GetNextTasks(tNode)[0];
                tNode = graph.GetNextTasks(tNode)[0];

                var prevNodes = graph.GetPrevTasks(tNode);

                Assert.AreEqual(prevNodes.Count, 2);
                return;
            }

            Assert.Fail();
        }
    }
}
