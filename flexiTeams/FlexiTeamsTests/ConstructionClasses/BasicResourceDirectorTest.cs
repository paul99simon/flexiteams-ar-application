using System.Linq;
using System.Xml;
using FlexiTeams.ConstructionClasses;
using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Director.Basic;
using NUnit.Framework;

namespace FlexiTeamsTests.ConstructionClasses;

[TestFixture]
public class BasicResourceDirectorTest
{
    [Test]
    public void ConstructFromXmlNodeTest()
    {
        //Arrange
        var builder = new BasicResourceBuilder();
        var doc = new XmlDocument();
        doc.Load("C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/resource_pool_draft.xml");
        var node = doc.DocumentElement.SelectSingleNode("//ResourcePool/Resource");

        //Act
        BasicResourceDirector.ConstructFromXmlNode(builder, node);
        var resource = builder.GetResource();
        
        //Assert
        Assert.AreEqual("Resource_1", resource.Id.ToString());
        
        Assert.AreEqual("../images/Resource_1.jpg", resource.Photos[0].Path);

        Assert.AreEqual(32, resource.Age.Years);
        
        Assert.AreEqual(null, resource.Prefix);
        
        Assert.AreEqual("Anna", resource.FirstNames[0].ToString());
        
        Assert.AreEqual("Schmidt", resource.LastNames[0].ToString());
        
        Assert.AreEqual("verheiratet", resource.MaritalState.ToString());
        
        Assert.AreEqual(5, resource.Children[0].Age);
        Assert.AreEqual(3, resource.Children[1].Age);
        
        Assert.AreEqual(null, resource.Stressors);

        Assert.AreEqual("Beide Kinder gehen in die Krankenhaus-Kita", resource.PersonalInfos[0].ToString());

        Assert.AreEqual("Stationsschwester", resource.Professions[0].ToString());

        Assert.AreEqual("Onkologie", resource.Departments[0].ToString());

        Assert.AreEqual(13, resource.WorkExperience.Years);

        Assert.AreEqual(3, resource.TrainingDuration.Years);
        
        Assert.AreEqual(35, resource.WeeklyHours.Hours);

        Assert.AreEqual(50, resource.Overtime.Hours);
        
        Assert.AreEqual(28, resource.YearlyTimeOf.Days);
        
        Assert.AreEqual(5, resource.YearlyEducation.Days);
        
        Assert.AreEqual("Notfall Medizin", resource.Trainings[0].ToString());
        Assert.AreEqual("Anästhesie", resource.Trainings[1].ToString());
        
        Assert.AreEqual("Ausbildung als Labortechnikerin", resource.Qualifications[0].ToString());
        
        Assert.AreEqual("berufsbegleitendes Studium der Medizin", resource.Studies[0].ToString());
        Assert.AreEqual("Mainz", resource.Studies[0].Location);
        
        Assert.AreEqual("Mitglied im Personalrat", resource.AdditionalJobs[0].ToString());
        
        Assert.AreEqual(20, resource.CommuteTime.Minutes);
        
        Assert.AreEqual("öffentliche Verkehrsmittel", resource.MeansOfTransport[0].ToString());
        Assert.AreEqual("Fahrrad", resource.MeansOfTransport[1].ToString());
        
        Assert.AreEqual(null, resource.ProfessionalInfos);
        
        Assert.AreEqual("Medikamentierung", resource.Skills[0].ToString());
        Assert.AreEqual("Diagn. Schnelltest", resource.Skills[1].ToString());
        Assert.AreEqual("Dokumentation", resource.Skills[2].ToString());
        
        Assert.AreEqual("[Zuverlässigkeit, 85]", resource.Traits[0].ToString());
        Assert.AreEqual("[Entscheidungsvermögen, 25]",resource.Traits[1].ToString());
        Assert.AreEqual("[Belastungsfähigkeit, 65]", resource.Traits[2].ToString());
        Assert.AreEqual("[Veränderungsbereitschaft, 85]", resource.Traits[3].ToString());
    }
}