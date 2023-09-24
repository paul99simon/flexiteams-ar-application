using FlexiTeams.Graph.Nodes;
using FlexiTeams.IO;
using NUnit.Framework;


namespace FlexiTeamsTests.Graph
{
    [TestFixture]
    public class AdjListGraphTest
    {
        private const string path = "C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/importTest.xml";
        
        [Test]
        public void GetLongestPathTest()
        {
            var import = new Import(path);

            var tPool = import.TaskPool;
            var graph = import.Graph;

            var result = graph.GetLongestPath(graph.GetWorkflowNodes()[0]);

            Assert.AreEqual(12, result.Count);

            Assert.AreEqual("Medical examination",                                      tPool[result[0].Id].Type.ToString());
            Assert.AreEqual("Instructions to patient",                                  tPool[result[1].Id].Type.ToString());
            Assert.AreEqual("Pre-operative test",                                       tPool[result[2].Id].Type.ToString());
            Assert.AreEqual("Pre-operative counselling and get consent from patient ",  tPool[result[3].Id].Type.ToString());
            Assert.AreEqual("Patient preparation",                                      tPool[result[4].Id].Type.ToString());
            Assert.AreEqual("Transport to OT",                                          tPool[result[5].Id].Type.ToString());
            Assert.AreEqual("Anesthesia",                                               tPool[result[6].Id].Type.ToString());
            Assert.AreEqual("Surgery",                                                  tPool[result[7].Id].Type.ToString());
            Assert.AreEqual("Transport to post-surgery room",                           tPool[result[8].Id].Type.ToString());
            Assert.AreEqual("Transport to intensive care room",                         tPool[result[9].Id].Type.ToString());
            Assert.AreEqual("Medical examination",                                      tPool[result[10].Id].Type.ToString());
            Assert.AreEqual("Post-surgery instructions to Patient",                     tPool[result[11].Id].Type.ToString());
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
