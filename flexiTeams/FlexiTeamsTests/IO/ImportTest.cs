using FlexiTeams.IO;
using NuGet.Frameworks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiTeamsTests.IO
{
    [TestFixture]
    public class ImportTest
    {
        const string path = "C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/Test.xml";

        [Test]
        public void ResourcePoolTest()
        {
            var import = new Import (path);
            
        }

        [Test]
        public void DataPoolTest()
        {

            var import = new Import(path);

            Assert.AreEqual(480, import.DataPool.Count);
            
        }

        [Test]
        public void WorkflowPoolTest()
        {
            var import = new Import(path);

            Assert.AreEqual(9, import.WorkflowPool.Count);
        }

        [Test]
        public void TaskPoolTest()
        {
            var import = new Import(path);

            Assert.AreEqual(72, import.TaskPool.Count);
        }

        [Test]
        public void GraphTest()
        {
            var import = new Import(path);

            Assert.AreEqual(81, import.Graph.Nodes.Count);
        }
    }
}
