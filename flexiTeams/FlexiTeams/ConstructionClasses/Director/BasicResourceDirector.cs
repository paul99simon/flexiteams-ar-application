using System.Xml;
using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.ConstructionClasses.Diretor;

public class BasicResourceDirector
{
    //Xml-Construction
    public static void ConstructFromXmlNode(IResourceBuilder builder, XmlNode resource)
    {
        builder.Set(GetResourceId());
        builder.Set(GetAge());
        builder.Set(GetFirstNames());
        builder.Set(GetLastNames());
        builder.Set(GetProfessions());
        builder.Set(GetDepartments());
        builder.Set(GetWeeklyHours());
        builder.Set(GetWorkAgreement());
        builder.Set(GetSkills());
        builder.Set(GetTraits());

        //Nullable Types
        var photos = GetPhotos();
        if(photos != null) builder.Set(photos);

        var maritalState = GetMaritalState();
        if (maritalState != null) builder.Set(maritalState); 

        var children = GetChildren();
        if (children != null) builder.Set(children);

        var stressors = GetStressors();
        if(stressors != null) builder.Set(stressors);

        var personalInfos = GetPersonalInfos();
        if(personalInfos != null) builder.Set(personalInfos);

        var prefix = GetPrefix();
        if(prefix != null) builder.Set(prefix);

        var workExperience = GetWorkExperience();
        if(workExperience != null) builder.Set(workExperience);

        var trainingDuration = GetTrainingDuration();
        if(trainingDuration != null) builder.Set(trainingDuration);

        var overtime = GetOvertime();
        if(overtime != null) builder.Set(overtime);

        var yearlyTimeOf = GetYearlyTimeOf();
        if(yearlyTimeOf != null) builder.Set(yearlyTimeOf);

        var yearlyEducation = GetYearlyEducation();
        if(yearlyEducation != null) builder.Set(yearlyEducation);

        var traingings = GetTrainings();
        if (traingings != null) builder.Set(traingings);

        var qualifications = GetQualifications();
        if (qualifications != null) builder.Set(qualifications);

        var studies = GetStudies();
        if (studies != null) builder.Set(studies);

        var additionalJobs = GetAdditionalJobs();
        if (additionalJobs != null) builder.Set(additionalJobs);

        var arrivalTime = GetArrivalTime();
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
            var nodes = resource.SelectNodes("photo");
            var temp = new List<Photo>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    var URI = node.SelectSingleNode("URI");
                    temp.Add(new Photo(URI.InnerText));
                }
            }
            return temp.Any() ? temp : null;
        }
        Age GetAge()
        {
            var node = resource.SelectSingleNode("age");
            var timespan = XmlConvert.ToTimeSpan(node.InnerText);

            return new Age(timespan.Days / 365);
        }
        Prefix? GetPrefix()
        {
            var node = resource.SelectSingleNode("prefix");
            return node == null ? null : new Prefix(node.InnerText);
        }
        List<FirstName> GetFirstNames()
        {
            var nodes = resource.SelectNodes("firstName");
            var temp = new List<FirstName>();
            
            foreach (XmlNode node in nodes)
            {
                string value = node.InnerText;
                temp.Add(new FirstName(value));
            }
            
            return temp;
        }
        List<LastName> GetLastNames()
        {
            var nodes = resource.SelectNodes("lastName");
            var temp = new List<LastName>();
            
            foreach (XmlNode node in nodes)
            {
                string value = node.InnerText;
                temp.Add(new LastName(value));
            }
            
            return temp;
        }
        MaritalState? GetMaritalState()
        {
            var node = resource.SelectSingleNode("maritalStatus");
            return node is null ? null : new MaritalState(node.InnerText);
        }
        List<Child>? GetChildren()
        {
            var nodes = resource.SelectNodes("child");
            var temp = new List<Child>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    var ageNode = node.SelectSingleNode("age");
                    var timespan = XmlConvert.ToTimeSpan(ageNode.InnerText);
                    temp.Add(new Child(timespan.Days/365));
                }
            }
            return temp.Any() ? temp : null;
        }
        List<Stressor> GetStressors()
        {
            var nodes = resource.SelectNodes("stressor");
            var temp = new List<Stressor>();
            
            foreach (XmlNode node in nodes)
            {
                string value = node.InnerText;
                temp.Add(new Stressor(value));
            }
            
            return temp;
        }
        List<PersonalInfo> GetPersonalInfos()
        {
            var nodes = resource.SelectNodes("personalInfo");
            var temp = new List<PersonalInfo>();

            foreach (XmlNode node in nodes)
            {
                string value = node.InnerText;
                temp.Add(new PersonalInfo(value));
            }
            
            return temp;
        }
        List<Profession> GetProfessions()
        {
            var nodes = resource.SelectNodes("profession");
            var temp = new List<Profession>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string value = node.InnerText;
                    temp.Add(new Profession(value));
                }
            }
            return temp;
        }
        List<Department> GetDepartments()
        {
            var nodes = resource.SelectNodes("department");
            var temp = new List<Department>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string value = node.InnerText;
                    temp.Add(new Department(value));
                }
            }
            return temp;
        }
        WorkExperience? GetWorkExperience()
        {
            var node = resource.SelectSingleNode("workExperience");
            var timespan = XmlConvert.ToTimeSpan(node.InnerText);

            return new WorkExperience(timespan.Days / 365);
        }
        TrainingDuration? GetTrainingDuration()
        {
            var node = resource.SelectSingleNode("trainingDuration");
            var timespan = XmlConvert.ToTimeSpan(node.InnerText);

            return new TrainingDuration(timespan.Days / 365);
        }
        WeeklyHours GetWeeklyHours()
        {
            var node = resource.SelectSingleNode("weeklyHours");
            var timespan = XmlConvert.ToTimeSpan(node.InnerText);

            return new WeeklyHours((int) timespan.TotalHours);
        }
        Overtime? GetOvertime()
        {
            var node = resource.SelectSingleNode("overtime");
            var timespan = XmlConvert.ToTimeSpan(node.InnerText);

            return new Overtime((int)timespan.TotalHours);
        }
        YearlyTimeOf? GetYearlyTimeOf()
        {
            var node = resource.SelectSingleNode("yearlyTimeOf");
            var timespan = XmlConvert.ToTimeSpan(node.InnerText);

            return new YearlyTimeOf((int)timespan.TotalDays);
        }
        YearlyEducation? GetYearlyEducation()
        {
            var node = resource.SelectSingleNode("yearlyEducation");
            var timespan = XmlConvert.ToTimeSpan(node.InnerText);

            return new YearlyEducation((int)timespan.TotalDays);
        }
        List<Training> GetTrainings()
        {
            var nodes = resource.SelectNodes("training");
            var temp = new List<Training>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string value = node.InnerText;
                    temp.Add(new Training(value));
                }
            }
            return temp;
        }
        List<Qualification> GetQualifications()
        {
            var nodes = resource.SelectNodes("qualification");
            var temp = new List<Qualification>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string value = node.InnerText;
                    temp.Add(new Qualification(value));
                }
            }
            return temp;
        }
        List<TimeInterval>[] GetWorkAgreement()
        {
            var nodes = resource.SelectNodes("workAgreement");
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
                    string[] xml = node.InnerText.Split('-');
                    int index = int.Parse(xml[0]);
                    var dt1 = new DayTime(xml[1]);
                    var dt2 = new DayTime(xml[2]);
                    var ti = new TimeInterval(dt1, dt2);
                    temp[index].Add(ti);
                }
            }
            return temp;
        }
        List<Studies> GetStudies()
        {
            var nodes = resource.SelectNodes("studies");
            var temp = new List<Studies>();

            foreach (XmlNode node in nodes)
            {
                string name = node.SelectSingleNode("name").InnerText;
                string location = node.SelectSingleNode("location").InnerText;
                temp.Add(new Studies(name, location));
            }

            return temp;
        }
        List<AdditionalJob> GetAdditionalJobs()
        {
            var nodes = resource.SelectNodes("additionalJob");
            var temp = new List<AdditionalJob>();

                foreach (XmlNode node in nodes)
                {
                    string value = node.InnerText;
                    temp.Add(new AdditionalJob(value));
                }
            
            return temp;
        }
        ArrivalTime? GetArrivalTime()
        {
            var node = resource.SelectSingleNode("arrivalTime");
            var timespan = XmlConvert.ToTimeSpan(node.InnerText);

            return new ArrivalTime((int)timespan.TotalMinutes);
        }
        List<Vehicle> GetMeansOfTransport()
        {
            var nodes = resource.SelectNodes("meansOfTransport");
            var temp = new List<Vehicle>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string value = node.InnerText;
                    temp.Add(new Vehicle(value));
                }
            }
            return temp;
        }
        List<ProfessionalInfo> GetProfessionalInfos()
        {
            var nodes = resource.SelectNodes("professionalInfo");
            var temp = new List<ProfessionalInfo>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string value = node.InnerText;
                    temp.Add(new ProfessionalInfo(value));
                }
            }
            return temp;
        }
        List<Skill> GetSkills()
        {
            var nodes = resource.SelectNodes("skill");
            var temp = new List<Skill>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string value = node.InnerText;
                    temp.Add(new Skill(value));
                }
            }
            return temp;
        }
        List<Trait> GetTraits()
        {
            var nodes = resource.SelectNodes("trait");
            var temp = new List<Trait>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string name = node.SelectSingleNode("name").InnerText;
                    int value = int.Parse(node.SelectSingleNode("value").InnerText);
                    temp.Add(new Trait(new KeyValuePair<string, int>(name, value)));
                }
            }
            return temp;
        }
    }
}