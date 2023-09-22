using System.Xml;
using System.Xml.Linq;
using FlexiTeams.ConstructionClasses.Builder.Interface;
using FlexiTeams.ConstructionClasses.Director.Interface;
using FlexiTeams.DataClasses.Resource;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.ConstructionClasses.Director.Basic;

public class BasicResourceDirector : IResourceDirector
{
    //Xml-Construction
    public static void ConstructFromXmlNode(IResourceBuilder builder, XmlNode resource)
    {
        builder.Set(GetResourceId());
        builder.Set(GetAge());
        builder.Set(GetFirstNames());
        builder.Set(GetLastNames());
        builder.Set(GetMaritalState());
        builder.Set(GetProfessions());
        builder.Set(GetDepartments());
        builder.Set(GetWeeklyHours());
        builder.Set(GetYearlyTimeOf());
        builder.Set(GetCommuteTime());
        builder.Set(GetMeansOfTransport());
        builder.Set(GetWorkAgreement());
        builder.Set(GetSkills());
        builder.Set(GetTraits());

        //Nullable Types
        var photos = GetPhotos();
        if (photos != null) builder.Set(photos);

        var children = GetChildren();
        if (children != null) builder.Set(children);

        var stressors = GetStressors();
        if (stressors != null) builder.Set(stressors);

        var personalInfos = GetPersonalInfos();
        if (personalInfos != null) builder.Set(personalInfos);

        var prefix = GetPrefix();
        if (prefix != null) builder.Set(prefix);

        var workExperience = GetWorkExperience();
        if (workExperience != null) builder.Set(workExperience);

        var trainingDuration = GetTrainingDuration();
        if (trainingDuration != null) builder.Set(trainingDuration);

        var overtime = GetOvertime();
        if (overtime != null) builder.Set(overtime);

        var yearlyTimeOf = GetYearlyTimeOf();
        if (yearlyTimeOf != null) builder.Set(yearlyTimeOf);

        var yearlyEducation = GetYearlyEducation();
        if (yearlyEducation != null) builder.Set(yearlyEducation);

        var traingings = GetTrainings();
        if (traingings != null) builder.Set(traingings);

        var qualifications = GetQualifications();
        if (qualifications != null) builder.Set(qualifications);

        var studies = GetStudies();
        if (studies != null) builder.Set(studies);

        var additionalJobs = GetAdditionalJobs();
        if (additionalJobs != null) builder.Set(additionalJobs);

        var arrivalTime = GetCommuteTime();
        if (arrivalTime != null) builder.Set(arrivalTime);

        var meansOfTransport = GetMeansOfTransport();
        if (meansOfTransport != null) builder.Set(meansOfTransport);

        var professionalInfos = GetProfessionalInfos();
        if (professionalInfos != null) builder.Set(professionalInfos);

        ResourceId GetResourceId()
        {
            string id = resource.Attributes.GetNamedItem("xml:id").InnerText;

            return new ResourceId(id);
        }
        List<Photo>? GetPhotos()
        {
            var nodes = resource.SelectNodes("Photo");
            var temp = new List<Photo>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string path = node.Attributes.GetNamedItem("path").InnerXml;
                    temp.Add(new Photo(path));
                }
            }
            return temp.Any() ? temp : null;
        }
        Age GetAge()
        {
            var node = resource.Attributes.GetNamedItem("age");
            var years = new ISO8601(node.InnerText).Years;

            return new Age(years);
        }
        Prefix? GetPrefix()
        {
            var node = resource.Attributes.GetNamedItem("prefix");
            return node == null ? null : new Prefix(node.InnerText);
        }
        List<FirstName> GetFirstNames()
        {
            var nodes = resource.SelectNodes("FirstName");
            var temp = new List<FirstName>();

            foreach (XmlNode node in nodes)
            {
                string value = node.Attributes.GetNamedItem("value").InnerText;
                temp.Add(new FirstName(value));
            }

            return temp;
        }
        List<LastName> GetLastNames()
        {
            var nodes = resource.SelectNodes("LastName");
            var temp = new List<LastName>();

            foreach (XmlNode node in nodes)
            {
                string value = node.Attributes.GetNamedItem("value").InnerText;
                temp.Add(new LastName(value));
            }

            return temp;
        }
        MaritalState GetMaritalState()
        {
            var node = resource.Attributes.GetNamedItem("maritalState");
            return new MaritalState(node.InnerText);
        }
        List<Child>? GetChildren()
        {
            var nodes = resource.SelectNodes("Child");
            var temp = new List<Child>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    var ageNode = node.Attributes.GetNamedItem("age");
                    var timespan = XmlConvert.ToTimeSpan(ageNode.InnerText);
                    temp.Add(new Child(timespan.Days / 365));
                }
            }
            return temp.Any() ? temp : null;
        }
        List<Stressor>? GetStressors()
        {
            var nodes = resource.SelectNodes("Stressor");
            var temp = new List<Stressor>();

            foreach (XmlNode node in nodes)
            {
                string value = node.Attributes.GetNamedItem("value").InnerText;
                temp.Add(new Stressor(value));
            }

            return temp.Any() ? temp : null;
        }
        List<PersonalInfo>? GetPersonalInfos()
        {
            var nodes = resource.SelectNodes("PersonalInfo");
            var temp = new List<PersonalInfo>();

            foreach (XmlNode node in nodes)
            {
                string value = node.Attributes.GetNamedItem("value").InnerText;
                temp.Add(new PersonalInfo(value));
            }

            return temp.Any() ? temp : null;
        }
        List<Profession> GetProfessions()
        {
            var nodes = resource.SelectNodes("Profession");
            var temp = new List<Profession>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string value = node.Attributes.GetNamedItem("value").InnerText;
                    temp.Add(new Profession(value));
                }
            }
            return temp;
        }
        List<Department> GetDepartments()
        {
            var nodes = resource.SelectNodes("Department");
            var temp = new List<Department>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string value = node.Attributes.GetNamedItem("value").InnerText;
                    temp.Add(new Department(value));
                }
            }
            return temp;
        }
        WorkExperience? GetWorkExperience()
        {
            var node = resource.Attributes.GetNamedItem("workExperience");
            return node is null ? null : new WorkExperience(new ISO8601(node.InnerText).Years);
        }
        TrainingDuration? GetTrainingDuration()
        {
            var node = resource.Attributes.GetNamedItem("trainingDuration");
            return node is null ? null : new TrainingDuration(new ISO8601(node.InnerText).Years);
        }
        WeeklyHours GetWeeklyHours()
        {
            var node = resource.Attributes.GetNamedItem("weeklyHours");
            return new WeeklyHours(new ISO8601(node.InnerText).Hours);
        }
        Overtime? GetOvertime()
        {
            var node = resource.Attributes.GetNamedItem("overtime");
            return node is null ? null : new Overtime(new ISO8601(node.InnerText).Hours);
        }
        YearlyTimeOf GetYearlyTimeOf()
        {
            var node = resource.Attributes.GetNamedItem("yearlyTimeOf");
            return new YearlyTimeOf(new ISO8601(node.InnerText).Days);
        }
        YearlyEducation? GetYearlyEducation()
        {
            var node = resource.Attributes.GetNamedItem("yearlyEducation");
            return node is null ? null : new YearlyEducation(new ISO8601(node.InnerText).Days);
        }
        List<Training>? GetTrainings()
        {
            var nodes = resource.SelectNodes("Training");
            var temp = new List<Training>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string value = node.Attributes.GetNamedItem("value").InnerText;
                    temp.Add(new Training(value));
                }
            }
            return temp.Any() ? temp : null;
        }
        List<Qualification>? GetQualifications()
        {
            var nodes = resource.SelectNodes("Qualification");
            var temp = new List<Qualification>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string value = node.Attributes.GetNamedItem("value").InnerText;
                    temp.Add(new Qualification(value));
                }
            }
            return temp.Any() ? temp : null;
        }
        List<TimeInterval>[] GetWorkAgreement()
        {
            var nodes = resource.SelectNodes("WorkAgreement");
            var temp = new List<TimeInterval>[]
            {
                new List<TimeInterval>(),
                new List<TimeInterval>(),
                new List<TimeInterval>(),
                new List<TimeInterval>(),
                new List<TimeInterval>(),
                new List<TimeInterval>(),
                new List<TimeInterval>(),
            };

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string[] xml = node.Attributes.GetNamedItem("value").InnerText.Split('-');
                    int index = int.Parse(xml[0]);
                    var dt1 = new DayTime(xml[1]);
                    var dt2 = new DayTime(xml[2]);
                    var ti = new TimeInterval(dt1, dt2);
                    temp[index].Add(ti);
                }
            }
            return temp;
        }
        List<Studies>? GetStudies()
        {
            var nodes = resource.SelectNodes("Studies");
            var temp = new List<Studies>();

            foreach (XmlNode node in nodes)
            {
                var name = node.Attributes.GetNamedItem("name").InnerText;
                var locationNode = node.Attributes.GetNamedItem("location");

                if (locationNode != null) temp.Add(new Studies(name, locationNode.InnerText));
                else temp.Add(new Studies(name));
            }

            return temp.Any() ? temp : null;
        }
        List<AdditionalJob>? GetAdditionalJobs()
        {
            var nodes = resource.SelectNodes("AdditionalJob");
            var temp = new List<AdditionalJob>();

            foreach (XmlNode node in nodes)
            {
                var name = node.Attributes.GetNamedItem("name").InnerText;
                var yearlyRequiredDaysNode = node.Attributes.GetNamedItem("yearlyRequiredDays");

                if (yearlyRequiredDaysNode != null)
                {
                    string yearlyRequiredDays = yearlyRequiredDaysNode.InnerText;
                    int days = (int)XmlConvert.ToTimeSpan(yearlyRequiredDays).TotalDays;
                    temp.Add(new AdditionalJob(name, days));
                }
                else temp.Add(new AdditionalJob(name));
            }

            return temp.Any() ? temp : null;
        }
        CommuteTime GetCommuteTime()
        {
            var node = resource.Attributes.GetNamedItem("commuteTime");
            return new CommuteTime(new ISO8601(node.InnerText).Minutes);
        }
        List<Vehicle> GetMeansOfTransport()
        {
            var nodes = resource.SelectNodes("MeansOfTransport");
            var temp = new List<Vehicle>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string value = node.Attributes.GetNamedItem("value").InnerText;
                    temp.Add(new Vehicle(value));
                }
            }
            return temp;
        }
        List<ProfessionalInfo>? GetProfessionalInfos()
        {
            var nodes = resource.SelectNodes("ProfessionalInfo");
            var temp = new List<ProfessionalInfo>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string value = node.Attributes.GetNamedItem("value").InnerText;
                    temp.Add(new ProfessionalInfo(value));
                }
            }
            return temp.Any() ? temp : null;
        }
        List<Skill> GetSkills()
        {
            var nodes = resource.SelectNodes("Skill");
            var temp = new List<Skill>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string value = node.Attributes.GetNamedItem("value").InnerText;
                    temp.Add(new Skill(value));
                }
            }
            return temp;
        }
        List<Trait> GetTraits()
        {
            var nodes = resource.SelectNodes("Trait");
            var temp = new List<Trait>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string name = node.Attributes.GetNamedItem("name").InnerText;
                    int value = int.Parse(node.Attributes.GetNamedItem("value").InnerText);
                    temp.Add(new Trait(name, value));
                }
            }
            return temp;
        }
    }

    public Resource Construct(XElement resourceNode, IResourceBuilder rBuilder)
    {
        throw new NotImplementedException();
    }
}