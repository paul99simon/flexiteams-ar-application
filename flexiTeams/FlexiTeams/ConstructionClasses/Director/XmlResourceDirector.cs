using System.Xml;
using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.ConstructionClasses.Diretor;

public class XMLResourceDirector
{
    //Xml-Construction
    public static void ConstructFromXmlNode(IResourceBuilder builder, XmlNode resource)
    {
        builder.Set(GetResourceId());
        builder.Set(GetPhotos());
        builder.Set(GetAge());
        builder.Set(GetPrefix());
        builder.Set(GetFirstNames());
        builder.Set(GetLastNames());
        builder.Set(GetMaritalStates());
        builder.Set(GetChildren());
        builder.Set(GetStressors());
        builder.Set(GetPersonalInfos());
        builder.Set(GetProfessions());
        builder.Set(GetDepartments());
        builder.Set(GetWorkExperience());
        builder.Set(GetTrainingDuration());
        builder.Set(GetWeeklyHours());
        builder.Set(GetOvertime());
        builder.Set(GetYearlyTimeOf());
        builder.Set(GetYearlyEducation());
        builder.Set(GetTrainings());
        builder.Set(GetQualifications());
        builder.Set(GetWorkAgreement());
        builder.Set(GetStudies());
        builder.Set(GetAdditionalJobs());
        builder.Set(GetArrivalTime());
        builder.Set(GetMeansOfTransport());
        builder.Set(GetProfessionalInfos());
        builder.Set(GetSkills());
        builder.Set(GetTraits());

        ResourceId GetResourceId()
        {
            var node = resource;

            string id = resource.Attributes.GetNamedItem("xml:id").InnerText;

            return new ResourceId(id);
        }
        
        List<Photo> GetPhotos()
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
        Dictionary<string, MaritalState> GetMaritalStates()
        {
            var nodes = resource.SelectNodes("maritalStatus");
            var temp = new Dictionary<string, MaritalState>();
            
            foreach (XmlNode node in nodes)
            {
                string lang = node.Attributes.GetNamedItem("xml:lang").InnerText;
                string value = node.InnerText;
                
                temp.Add(lang, new MaritalState(value));
            }
            
            return temp;
        }
        List<Child> GetChildren()
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
        Dictionary<string, List<Stressor>> GetStressors()
        {
            var nodes = resource.SelectNodes("stressor");
            var temp = new Dictionary<string, List<Stressor>>();
            
            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string lang = node.Attributes.GetNamedItem("xml:lang").InnerText;
                    string value = node.InnerText;
                    
                    if (!temp.ContainsKey(lang))
                    {
                        temp.Add(lang, new List<Stressor>());
                    }
                    temp[lang].Add(new Stressor(value));
                }
            }
            return temp.Any() ? temp : null;
        }
        Dictionary<string, List<PersonalInfo>> GetPersonalInfos()
        {
            var nodes = resource.SelectNodes("personalInfo");
            var temp = new Dictionary<string, List<PersonalInfo>>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string lang = node.Attributes.GetNamedItem("xml:lang").InnerText;
                    string value = node.InnerText;
                    
                    if(! temp.ContainsKey(lang)) temp.Add(lang, new List<PersonalInfo>());
                    temp[lang].Add(new PersonalInfo(value));
                }
            }
            return temp.Any() ? temp : null;
        }
        Dictionary<string, List<Profession>> GetProfessions()
        {
            var nodes = resource.SelectNodes("profession");
            var temp = new Dictionary<string, List<Profession>>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string lang = node.Attributes.GetNamedItem("xml:lang").InnerText;
                    string value = node.InnerText;
                    
                    if(! temp.ContainsKey(lang)) temp.Add(lang, new List<Profession>()); 
                    temp[lang].Add(new Profession(value));
                }
            }
            return temp;
        }
        Dictionary<string, List<Department>> GetDepartments()
        {
            var nodes = resource.SelectNodes("department");
            var temp = new Dictionary<string, List<Department>>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string lang = node.Attributes.GetNamedItem("xml:lang").InnerText;
                    string value = node.InnerText;
                    
                    if(! temp.ContainsKey(lang)) temp.Add(lang, new List<Department>());
                    temp[lang].Add(new Department(value));
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
        Dictionary<string, List<Training>> GetTrainings()
        {
            var nodes = resource.SelectNodes("training");
            var temp = new Dictionary<string, List<Training>>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string lang = node.Attributes.GetNamedItem("xml:lang").InnerText;
                    string value = node.InnerText;
                    if(! temp.ContainsKey(lang)) temp.Add(lang, new List<Training>());
                    temp[lang].Add(new Training(value));
                }
            }
            return temp.Any() ? temp : null;
        }
        Dictionary<string, List<Qualification>> GetQualifications()
        {
            var nodes = resource.SelectNodes("qualification");
            var temp = new Dictionary<string, List<Qualification>>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string lang = node.Attributes.GetNamedItem("xml:lang").InnerText;
                    string value = node.InnerText;
                    
                    if(! temp.ContainsKey(lang)) temp.Add(lang, new List<Qualification>());
                    temp[lang].Add(new Qualification(value));
                }
            }
            return temp.Any() ? temp : null;
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
            return temp.Any() ? temp : null;
        }
        Dictionary<string, List<Studies>> GetStudies()
        {
            var nodes = resource.SelectNodes("studies");
            var temp = new Dictionary<string, List<Studies>>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string lang = node.Attributes.GetNamedItem("xml:lang").InnerText;
                    string name = node.SelectSingleNode("name").InnerText;
                    string location = node.SelectSingleNode("location").InnerText;
                    
                    if(! temp.ContainsKey(lang)) temp.Add(lang, new List<Studies>());
                    temp[lang].Add(new Studies(name, location));
                }
            }
            return temp.Any() ? temp : null;
        }
        Dictionary<string, List<AdditionalJob>> GetAdditionalJobs()
        {
            var nodes = resource.SelectNodes("additionalJob");
            var temp = new Dictionary<string, List<AdditionalJob>>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string lang = node.Attributes.GetNamedItem("xml:lang").InnerText;
                    string value = node.InnerText;
                    
                    if(! temp.ContainsKey(lang)) temp.Add(lang, new List<AdditionalJob>());
                    temp[lang].Add(new AdditionalJob(value));
                }
            }
            return temp.Any() ? temp : null;
        }
        ArrivalTime GetArrivalTime()
        {
            var node = resource.SelectSingleNode("arrivalTime");
            var timespan = XmlConvert.ToTimeSpan(node.InnerText);

            return new ArrivalTime((int)timespan.TotalMinutes);
        }
        Dictionary<string, List<Vehicle>> GetMeansOfTransport()
        {
            var nodes = resource.SelectNodes("meansOfTransport");
            var temp = new Dictionary<string, List<Vehicle>>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string lang = node.Attributes.GetNamedItem("xml:lang").InnerText;
                    string value = node.InnerText;
                    
                    if(!temp.ContainsKey(lang)) temp.Add(lang, new List<Vehicle>());
                    temp[lang].Add(new Vehicle(value));
                }
            }
            return temp.Any() ? temp : null;
        }
        Dictionary<string, List<ProfessionalInfo>> GetProfessionalInfos()
        {
            var nodes = resource.SelectNodes("professionalInfo");
            var temp = new Dictionary<string, List<ProfessionalInfo>>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string lang = node.Attributes.GetNamedItem("xml:lang").InnerText;
                    string value = node.InnerText;
                    
                    if(! temp.ContainsKey(lang)) temp.Add(lang, new List<ProfessionalInfo>());
                    temp[lang].Add(new ProfessionalInfo(value));
                }
            }
            return temp.Any() ? temp : null;
        }
        Dictionary<string, List<Skill>> GetSkills()
        {
            var nodes = resource.SelectNodes("skill");
            var temp = new Dictionary<string, List<Skill>>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string lang = node.Attributes.GetNamedItem("xml:lang").InnerText;
                    string value = node.InnerText;
                    
                    if(! temp.ContainsKey(lang)) temp.Add(lang, new List<Skill>());
                    temp[lang].Add(new Skill(value));
                }
            }
            return temp;
        }
        Dictionary<string, List<Trait>> GetTraits()
        {
            var nodes = resource.SelectNodes("trait");
            var temp = new Dictionary<string, List<Trait>>();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string lang = node.Attributes.GetNamedItem("xml:lang").InnerText;
                    string name = node.SelectSingleNode("name").InnerText;
                    int value = int.Parse(node.SelectSingleNode("value").InnerText);
                    
                    if(! temp.ContainsKey(lang)) temp.Add(lang, new List<Trait>());
                    temp[lang].Add(new Trait(new KeyValuePair<string, int>(name, value)));
                }
            }
            return temp;
        }
    }
}