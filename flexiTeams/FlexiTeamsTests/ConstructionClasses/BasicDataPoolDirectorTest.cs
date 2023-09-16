using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiTeams.Inventory;
using FlexiTeams.ConstructionClasses.Director;

namespace FlexiTeamsTests.ConstructionClasses
{
    [TestFixture]
    public class BasicDataPoolDirectorTest
    {
        [Test]
        public void GetDataPool()
        {
            string xmlPath = "C://Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/20DataPool.xml";
            DataPool dp = BasicDataPoolDirector.ConstructFromXml(xmlPath);

            Assert.AreEqual(480, dp.Count);
        }

    }
}
