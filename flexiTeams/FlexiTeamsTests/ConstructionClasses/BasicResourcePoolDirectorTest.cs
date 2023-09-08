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
        string xsdPath = "C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/resourcePools/resource_pool.xsd";
        string xmlxsdPath = "C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/resourcePools/xml.xsd";
        ResourcePool pool = new ResourcePool();

        BasicResourcePoolDirector.ConsructFromXML(pool, xmlpath, xsdPath, xmlxsdPath, new BasicResourceBuilder());

        Assert.AreEqual(3, pool.Count);
    }
}