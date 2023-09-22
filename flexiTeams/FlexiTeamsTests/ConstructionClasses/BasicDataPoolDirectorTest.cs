
using NUnit.Framework;

using FlexiTeams.Inventory;
using FlexiTeams.ConstructionClasses.Director.Basic;

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
