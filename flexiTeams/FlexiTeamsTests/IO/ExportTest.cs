using FlexiTeams.IO;
using NUnit.Framework;
using System.Xml.Schema;

namespace FlexiTeamsTests.IO
{
    [TestFixture]
    public class ExportTest
    {

        [Test]
        public void ToXmlTest()
        {
            const string importPath = "C://Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/importTest2.xml";
            const string scenarioXsdPath = "C://Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/scenario.xsd";
            const string resourcePoolXsdPath = "C://Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/resourcePool.xsd";
            const string dataPoolXsdPath = "C://Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/dataPool.xsd";
            const string workflowPoolXsdPath = "C://Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/workflowPool.xsd";
            const string taskPoolXsdPath = "C://Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/taskPool.xsd";
            const string graphXsdPath = "C://Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/graph.xsd";

            const string exportPath = "C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/";
            const string fileName = "exportTest.xml";

            var import = new Import(importPath);

            var schemaSet = new XmlSchemaSet();
            schemaSet.Add("", scenarioXsdPath);
            schemaSet.Add("", resourcePoolXsdPath);
            schemaSet.Add("", dataPoolXsdPath);
            schemaSet.Add("", workflowPoolXsdPath);
            schemaSet.Add("", taskPoolXsdPath);
            schemaSet.Add("", graphXsdPath);

            var doc = Export.ToXml(import.ResourcePool, import.DataPool, import.WorkflowPool, import.TaskPool, import.Graph, schemaSet);
            Export.Save(exportPath, fileName, doc);
        }
    }

}
