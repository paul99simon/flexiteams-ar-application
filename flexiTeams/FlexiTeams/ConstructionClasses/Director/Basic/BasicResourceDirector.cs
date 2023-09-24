using System.Xml;
using System.Xml.Linq;
using FlexiTeams.ConstructionClasses.Builder.Interface;
using FlexiTeams.ConstructionClasses.Director.Interface;
using FlexiTeams.DataClasses.Resource;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Graph.Nodes;
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
        //required Attributes
        string id           = resourceNode.Attribute("id").Value;
        string age          = resourceNode.Attribute("age").Value;
        string maritalState = resourceNode.Attribute("maritalState").Value;
        string weeklyHours  = resourceNode.Attribute("weeklyHours").Value;
        string yearlyTimeOf = resourceNode.Attribute("yearlyTimeOf").Value;
        string commuteTime  = resourceNode.Attribute("commuteTime").Value;

        rBuilder.Set(new ResourceId(id));
        rBuilder.Set(new Age(new ISO8601(age).Years));
        rBuilder.Set(new MaritalState(maritalState));
        rBuilder.Set(new WeeklyHours(new ISO8601(weeklyHours).Hours));
        rBuilder.Set(new YearlyTimeOf(new ISO8601(yearlyTimeOf).Days));
        rBuilder.Set(new CommuteTime(new ISO8601(commuteTime).Minutes));

        //optional Attributes
        var prefixAttr = resourceNode.Attribute("prefix");
        var workExperienceAttr = resourceNode.Attribute("workExperience");
        var trainingDurationAttr = resourceNode.Attribute("trainingDuration");
        var overtimeAttr = resourceNode.Attribute("overtime");
        var yearlyEducationAttr = resourceNode.Attribute("yearlyEducation");

        if (prefixAttr != null)             rBuilder.Set(new Prefix(prefixAttr.Value));
        if (workExperienceAttr != null)     rBuilder.Set(new WorkExperience(new ISO8601(workExperienceAttr.Value).Years));
        if (trainingDurationAttr != null)   rBuilder.Set(new TrainingDuration(new ISO8601(trainingDurationAttr.Value).Years));
        if (overtimeAttr != null)           rBuilder.Set(new Overtime(new ISO8601(overtimeAttr.Value).Hours));
        if(yearlyEducationAttr != null)     rBuilder.Set(new YearlyEducation(new ISO8601(yearlyEducationAttr.Value).Days));

        //required Elements
        var firstNames = resourceNode.Descendants("FirstName");
        var firstNameList = new List<FirstName>();
        foreach ( var firstName in firstNames)
        {
            firstNameList.Add(new FirstName(firstName.Attribute("value").Value));
        }
        rBuilder.Set(firstNameList);

        var lastNames = resourceNode.Descendants("LastName");
        var lastNameList = new List<LastName>();
        foreach (var lastName in lastNames)
        {
            lastNameList.Add(new LastName(lastName.Attribute("value").Value));
        }
        rBuilder.Set(lastNameList);

        var professions = resourceNode.Descendants("Profession");
        var professionList = new List<Profession>();
        foreach (var profession in professions)
        {
            professionList.Add(new Profession(profession.Attribute("value").Value));
        }
        rBuilder.Set(professionList);

        var departments = resourceNode.Descendants("Department");
        var departmentList = new List<Department>();
        foreach (var department in departments)
        {
            departmentList.Add(new Department(department.Attribute("value").Value));
        }
        rBuilder.Set(departmentList);

        var meansOfTransport = resourceNode.Descendants("MeansOfTransport");
        var meansOfTransportList = new List<Vehicle>();
        foreach (var vehicle in meansOfTransport)
        {
            meansOfTransportList.Add(new Vehicle(vehicle.Attribute("value").Value));
        }
        rBuilder.Set(meansOfTransportList);

        var skills = resourceNode.Descendants("Skill");
        var skillsList = new List<Skill>();
        foreach (var skill in skills)
        {
            skillsList.Add(new Skill(skill.Attribute("value").Value));
        }
        rBuilder.Set(skillsList);

        var traits = resourceNode.Descendants("Trait");
        var traitList = new List<Trait>();
        foreach (var trait in traits)
        {
            traitList.Add(new Trait(trait.Attribute("name").Value, int.Parse(trait.Attribute("value").Value)));
        }
        rBuilder.Set(traitList);

        var workAgreements = resourceNode.Descendants("WorkAgreement");
        List<TimeInterval>[] schedule = new List<TimeInterval>[]
        {
            new (), new (), new (), new (), new (), new (),new()
        };
        foreach(var node in workAgreements)
        {
            string[] xml = node.Attribute("value").Value.Split('-');
            int index = int.Parse(xml[0]);
            var dt1 = new DayTime(xml[1]);
            var dt2 = new DayTime(xml[2]);
            var ti = new TimeInterval(dt1, dt2);
            schedule[index].Add(ti);
        }
        rBuilder.Set(schedule);

        //optional Elements
        var photos = resourceNode.Descendants("Photo");
        var photoList = new List<Photo>();
        foreach (var photo in photos)
        {
            photoList.Add(new Photo(photo.Attribute("path").Value));
        }
        if (photoList.Any()) rBuilder.Set(photoList);

        var children = resourceNode.Descendants("Child");
        var childList = new List<Child>();
        foreach (var child in children)
        {
            childList.Add(new Child(new ISO8601(child.Attribute("age").Value).Years));
        }
        if (childList.Any()) rBuilder.Set(childList);

        var stressors = resourceNode.Descendants("Stressor");
        var stressorList = new List<Stressor>();
        foreach (var stressor in stressors)
        {
            stressorList.Add(new Stressor(stressor.Attribute("value").Value));
        }
        if (stressorList.Any()) rBuilder.Set(stressorList);

        var personalInfos = resourceNode.Descendants("PersonalInfo");
        var personalInfoList = new List<PersonalInfo>();
        foreach (var personalInfo in personalInfos)
        {
            personalInfoList.Add(new PersonalInfo(personalInfo.Attribute("value").Value));
        }
        if (personalInfoList.Any()) rBuilder.Set(personalInfoList);

        var trainings = resourceNode.Descendants("Training");
        var trainingList = new List<Training>();
        foreach (var training in trainings)
        {
            trainingList.Add(new Training(training.Attribute("value").Value));
        }
        if (trainingList.Any()) rBuilder.Set(trainingList);

        var qualifications = resourceNode.Descendants("Qualification");
        var qualificationList = new List<Qualification>();
        foreach (var qualification in qualifications)
        {
            qualificationList.Add(new Qualification(qualification.Attribute("value").Value));
        }
        if (qualificationList.Any()) rBuilder.Set(qualificationList);

        var professionalInfos = resourceNode.Descendants("ProfessionalInfo");
        var professionalInfosList = new List<ProfessionalInfo>();
        foreach (var professionalInfo in professionalInfos)
        {
            professionalInfosList.Add(new ProfessionalInfo(professionalInfo.Attribute("value").Value));
        }
        if (professionalInfosList.Any()) rBuilder.Set(professionalInfosList);

        var studies = resourceNode.Descendants("Studies");
        var studiesList = new List<Studies>();
        foreach (var study in studies)
        {
            var locationAttr = study.Attribute("location");
            if (locationAttr != null) 
            {
                studiesList.Add(new Studies(study.Attribute("name").Value, locationAttr.Value));
            }
            else
            {
                studiesList.Add(new Studies(study.Attribute("name").Value));
            }
        }
        if (studiesList.Any()) rBuilder.Set(studiesList);

        var additionalJobs = resourceNode.Descendants("AdditionalJob");
        var additionalJobsList = new List<AdditionalJob>();
        foreach (var additionalJob in additionalJobs)
        {

            additionalJobsList.Add(new AdditionalJob(additionalJob.Attribute("name").Value));
        }
        if (additionalJobsList.Any()) rBuilder.Set(additionalJobsList);

        return rBuilder.GetResource();
    }
}