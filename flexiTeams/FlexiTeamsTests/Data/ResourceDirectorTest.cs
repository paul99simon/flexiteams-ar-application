using System.Xml;
using FlexiTeams.Data;
using NUnit.Framework;

namespace FlexiTeamsTests;

[TestFixture]
public class ResourceDirectorTest
{
    [Test]
    public void ConstructFromXmlNodeTest()
    {
        //Arrange
        var builder = new ResourceBuilder();
        var doc = new XmlDocument();
        doc.Load("C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/resourcePools/resource_pool_draft.xml");
        var node = doc.DocumentElement.SelectSingleNode("//resourcePool/resource");

        //Act
        ResourceDirector.ConstructFromXmlNode(builder, node);
        var resource = builder.GetResource();
        
        //Assert
        Assert.AreEqual("../images/Resource_1.jpg", resource.Photos[0].Path);

        Assert.AreEqual(32, resource.Age.Get);
        
        Assert.AreEqual(null, resource.Prefix);
        
        Assert.AreEqual("Anna", resource.FirstNames[0].Get);
        
        Assert.AreEqual("Schmidt", resource.LastNames[0].Get);
        
        Assert.AreEqual("de", resource.MaritalStates[0].Language);
        Assert.AreEqual("Verheiratet", resource.MaritalStates[0].Get);
        
        Assert.AreEqual(5, resource.Children[0].Age);
        Assert.AreEqual(3, resource.Children[1].Age);
        
        Assert.AreEqual(null, resource.Stressors);

        Assert.AreEqual("de", (object)resource.PersonalInfos[0].Language);
        Assert.AreEqual("Beide Kinder gehen in die Krankenhaus-Kita", (object)resource.PersonalInfos[0].Get);

        Assert.AreEqual("de", (object)resource.Professions[0].Language);
        Assert.AreEqual("Stationsschwester", (object)resource.Professions[0].Get);

        Assert.AreEqual("de", resource.Departments[0].Language);
        Assert.AreEqual("Onkologie", resource.Departments[0].Get);

        Assert.AreEqual(13, resource.WorkExperience.Get);

        Assert.AreEqual(3, resource.TrainingDuration.Get);
        
        Assert.AreEqual(35, resource.WeeklyHours.Get);

        Assert.AreEqual(50, resource.Overtime.Get);
        
        Assert.AreEqual(28, resource.YearlyTimeOf.Get);
        
        Assert.AreEqual(5, resource.YearlyEducation.Get);
        
        Assert.AreEqual("de", resource.Trainings[0].Language);
        Assert.AreEqual("Notfall Medizin", resource.Trainings[0].Get);
        Assert.AreEqual("de", resource.Trainings[1].Language);
        Assert.AreEqual("Anästhesie", resource.Trainings[1].Get);
        
        Assert.AreEqual("de", resource.Qualifications[0].Language);
        Assert.AreEqual("Ausbildung als Labortechnikerin", resource.Qualifications[0].Get);
        
        Assert.AreEqual(
            "monday: [22:00, 06:00], [06:00, 12:00]\n" +
                    "tuesday: [22:00, 06:00], [06:00, 12:00]\n"+
                    "wednesday: [22:00, 06:00], [06:00, 12:00]\n"+
                    "thursday: [22:00, 06:00], [06:00, 12:00]\n"+
                    "friday: [22:00, 06:00], [06:00, 12:00]\n"+
                    "saturday: \n" +
                    "sunday: "
            ,resource.WorkAgreement.ToString()
        );
        
        Assert.AreEqual("de", resource.Studies[0].Language);
        Assert.AreEqual("berufsbegleitendes Studium der Medizin", resource.Studies[0].Get);
        Assert.AreEqual("Mainz", resource.Studies[0].Location);
        
        Assert.AreEqual("de", resource.AdditionalJobs[0].Language);
        Assert.AreEqual("Mitglied im Personalrat", resource.AdditionalJobs[0].Get);
        
        Assert.AreEqual(20, resource.ArrivalTime.Get);
        
        Assert.AreEqual("de", resource.MeansOfTransport[0].Language);
        Assert.AreEqual("öffentliche Verkehrsmittel", resource.MeansOfTransport[0].Get);
        Assert.AreEqual("de", resource.MeansOfTransport[1].Language);
        Assert.AreEqual("Fahrrad", resource.MeansOfTransport[1].Get);
        
        Assert.AreEqual(null, resource.ProfessionalInfos);
        
        Assert.AreEqual("de", resource.Skills[0].Language);
        Assert.AreEqual("Medikamentierung", resource.Skills[0].Get);
        Assert.AreEqual("de", resource.Skills[1].Language);
        Assert.AreEqual("Diagn. Schnelltest", resource.Skills[1].Get);
        Assert.AreEqual("de", resource.Skills[2].Language);
        Assert.AreEqual("Dokumentation", resource.Skills[2].Get);
        
        Assert.AreEqual("de", resource.Traits[0].Language);
        Assert.AreEqual("[Zuverlässigkeit, 85]", resource.Traits[0].Get.ToString());
        Assert.AreEqual("de", resource.Traits[1].Language);
        Assert.AreEqual("[Entscheidungsvermögen, 25]",resource.Traits[1].Get.ToString());
        Assert.AreEqual("de", resource.Traits[2].Language);
        Assert.AreEqual("[Belastungsfähigkeit, 65]", resource.Traits[2].Get.ToString());
        Assert.AreEqual("de", resource.Traits[3].Language);
        Assert.AreEqual("[Veränderungsbereitschaft, 85]", resource.Traits[3].Get.ToString());
    }
}