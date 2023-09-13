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
            var result = graph.GetLongestPath(graph.GetWorkflowNodes()[0]);

            Assert.AreEqual(12, result.Count);

            Assert.AreEqual("Medical examination",                                      result[0].Task.Type.ToString());
            Assert.AreEqual("Instructions to patient",                                  result[1].Task.Type.ToString());
            Assert.AreEqual("Pre-operative test",                                       result[2].Task.Type.ToString());
            Assert.AreEqual("Pre-operative counselling and get consent from patient ",  result[3].Task.Type.ToString());
            Assert.AreEqual("Patient preparation",                                      result[4].Task.Type.ToString());
            Assert.AreEqual("Transport to OT",                                          result[5].Task.Type.ToString());
            Assert.AreEqual("Anesthesia",                                               result[6].Task.Type.ToString());
            Assert.AreEqual("Surgery",                                                  result[7].Task.Type.ToString());
            Assert.AreEqual("Transport to post-surgery room",                           result[8].Task.Type.ToString());
            Assert.AreEqual("Transport to intensive care room",                         result[9].Task.Type.ToString());
            Assert.AreEqual("Medical examination",                                      result[10].Task.Type.ToString());
            Assert.AreEqual("Post-surgery instructions to Patient",                     result[11].Task.Type.ToString());
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
