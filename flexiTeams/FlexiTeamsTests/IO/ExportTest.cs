using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Director.Basic;
using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Inventory;
using FlexiTeams.IO;
using NUnit.Framework;

namespace FlexiTeamsTests.IO
{
    [TestFixture]
    public class ExportTest
    {

        [Test]
        public void ToXmlTest()
        {

            const string xmlDataPath = "C://Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/20DataPool.xml";
            const string xmlResourcePath = "C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/resource_pool_draft.xml";
            const string csvPath = "C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/workflows.csv";

            var rPool = BasicResourcePoolDirector.ConstructFromXml(xmlResourcePath);
            var dPool = BasicDataPoolDirector.ConstructFromXml(xmlDataPath);

            var wPool = new WorkflowPool();
            var tPool = new TaskPool();
            var graph = new AdjListsGraph();

            BasicGraphDirector.ConstructFromCsv(csvPath, graph, wPool, tPool, new SamePriorityWorkflowBuilder(), new BasicTaskBuilder());


            const string path = "C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/";
            const string fileName = "Test.xml";

            var doc = Export.ToXml(rPool, dPool, wPool, tPool, graph);
            Export.Save(path, fileName, doc);
        }
    }

}
