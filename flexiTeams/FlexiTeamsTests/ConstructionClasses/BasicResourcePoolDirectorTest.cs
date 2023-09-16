using FlexiTeams;
using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Director;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using NUnit.Framework;
using static System.Net.Mime.MediaTypeNames;

namespace FlexiTeamsTests.ConstructionClasses;

[TestFixture]
public class BasicResourcePoolDirectorTest
{
    [Test]
    public void ConstructFromXmlTest()
    {
        string xmlpath = "C://Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/resource_pool_draft.xml";
        ResourcePool pool = BasicResourcePoolDirector.ConstructFromXml(xmlpath);
        Assert.AreEqual(3, pool.Count);
    }
}