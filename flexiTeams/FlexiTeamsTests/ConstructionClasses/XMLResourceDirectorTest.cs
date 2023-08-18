using System.Xml;
using FlexiTeams.ConstructionClasses;
using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Diretor;
using NUnit.Framework;

namespace FlexiTeamsTests;

[TestFixture]
public class XMLResourceDirectorTest
{
    [Test]
    public void ConstructFromXmlNodeTest()
    {
        //Arrange
        var builder = new BasicResourceBuilder();
        var doc = new XmlDocument();
        doc.Load("C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/resourcePools/resource_pool_draft.xml");
        var node = doc.DocumentElement.SelectSingleNode("//resourcePool/resource");

        //Act
        XMLResourceDirector.ConstructFromXmlNode(builder, node);
        var resource = builder.GetResource();
        resource.SetLanguage("de");
        
        //Assert
        Assert.AreEqual("RESOURCE_1", resource.Id.Get);
        
        Assert.AreEqual("../images/Resource_1.jpg", resource.Photos[0].Path);

        Assert.AreEqual(32, resource.Age.Get);
        
        Assert.AreEqual(null, resource.Prefix);
        
        Assert.AreEqual("Anna", resource.FirstNames[0].Get);
        
        Assert.AreEqual("Schmidt", resource.LastNames[0].Get);
        
        Assert.AreEqual("Verheiratet", resource.MaritalState.Get);
        
        Assert.AreEqual(5, resource.Children[0].Age);
        Assert.AreEqual(3, resource.Children[1].Age);
        
        Assert.AreEqual(null, resource.Stressors);

        Assert.AreEqual("Beide Kinder gehen in die Krankenhaus-Kita", resource.PersonalInfos[0].Get);

        Assert.AreEqual("Stationsschwester", resource.Professions[0].Get);

        Assert.AreEqual("Onkologie", resource.Departments[0].Get);

        Assert.AreEqual(13, resource.WorkExperience.Get);

        Assert.AreEqual(3, resource.TrainingDuration.Get);
        
        Assert.AreEqual(35, resource.WeeklyHours.Get);

        Assert.AreEqual(50, resource.Overtime.Get);
        
        Assert.AreEqual(28, resource.YearlyTimeOf.Get);
        
        Assert.AreEqual(5, resource.YearlyEducation.Get);
        
        Assert.AreEqual("Notfall Medizin", resource.Trainings[0].Get);
        Assert.AreEqual("Anästhesie", resource.Trainings[1].Get);
        
        Assert.AreEqual("Ausbildung als Labortechnikerin", resource.Qualification[0].Get);
        
        Assert.AreEqual("berufsbegleitendes Studium der Medizin", resource.Studies[0].Get);
        Assert.AreEqual("Mainz", resource.Studies[0].Location);
        
        Assert.AreEqual("Mitglied im Personalrat", resource.AdditionalJobs[0].Get);
        
        Assert.AreEqual(20, resource.ArrivalTime.Get);
        
        Assert.AreEqual("öffentliche Verkehrsmittel", resource.MeansOfTransport[0].Get);
        Assert.AreEqual("Fahrrad", resource.MeansOfTransport[1].Get);
        
        Assert.AreEqual(null, resource.ProfessionalInfos);
        
        Assert.AreEqual("Medikamentierung", resource.Skills[0].Get);
        Assert.AreEqual("Diagn. Schnelltest", resource.Skills[1].Get);
        Assert.AreEqual("Dokumentation", resource.Skills[2].Get);
        
        Assert.AreEqual("[Zuverlässigkeit, 85]", resource.Traits[0].Get.ToString());
        Assert.AreEqual("[Entscheidungsvermögen, 25]",resource.Traits[1].Get.ToString());
        Assert.AreEqual("[Belastungsfähigkeit, 65]", resource.Traits[2].Get.ToString());
        Assert.AreEqual("[Veränderungsbereitschaft, 85]", resource.Traits[3].Get.ToString());
    }
}