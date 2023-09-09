using FlexiTeams;
using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Director;
using NUnit.Framework;

namespace FlexiTeamsTests.ConstructionClasses;

[TestFixture]
public class BasicResourcePoolDirectorTest
{
    [Test]
    public void ConstructFromXmlTest()
    {
        string xmlpath = "C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/resourcePools/resource_pool_draft.xml";
        ResourcePool pool = BasicResourcePoolDirector.ConstructFromXml(xmlpath);
        Assert.AreEqual(3, pool.Count);
    }
}