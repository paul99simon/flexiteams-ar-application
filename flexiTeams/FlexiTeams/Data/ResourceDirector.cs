using System.Xml;
using flexiTeams.Util;
using FlexiTeams.Data.Wrapper;

namespace FlexiTeams.Data;

public class ResourceDirector
{
    
    //Xml-Construction
    public void ConstructFromXmlNode(IResourceBuilder builder, XmlNode resource)
    {        
        builder.SetPhotos(GetPhotos());
        builder.SetAge(GetAge());
        builder.SetPrefix(GetPrefix());
        builder.SetFirstNames(GetFirstNames());
        builder.SetLastNames(GetLastNames());
        builder.SetMaritalStates(GetMaritalStates());
        builder.SetChildren(GetChildren());
        builder.SetStressors(GetStressors());
        builder.SetPersonalInfos(GetPersonalInfos());
        builder.SetProfessions(GetProfessions());
        builder.SetDepartments(GetDepartments());
        builder.SetWorkExperience(GetWorkExperience());
        builder.SetTrainingDuration(GetTrainingDuration());
        builder.SetWeeklyHours(GetWeeklyHours());
        builder.SetOvertime(GetOvertime());
        builder.SetYearlyTimeOf(GetYearlyTimeOf());
        builder.SetYearlyEducation(GetYearlyEducation());
        builder.SetTrainings(GetTrainings());
        builder.SetQualifications(GetQualifications());
        builder.SetWorkAgreement(GetWorkAgreement());
        builder.SetStudies(GetStudies());
        builder.SetAdditionalJobs(GetAdditionalJobs());
        builder.SetArrivalTime(GetArrivalTime());
        builder.SetMeansOfTransport(GetMeansOfTransport());
        builder.SetProfessionalInfos(GetProfessionalInfos());
        builder.SetSkills(GetSkills());
        builder.SetTraits(GetTraits());
        
        Photos? GetPhotos()
        {
            var nodes = resource.SelectNodes("photo");
            var temp = new Photos();

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
        FirstNames GetFirstNames()
        {
            var nodes = resource.SelectNodes("firstName");
            var temp = new FirstNames();
            
            foreach (XmlNode node in nodes)
            {
                temp.Add(new FirstName(node.InnerText));
            }
            
            return temp;
        }
        LastNames GetLastNames()
        {
            var nodes = resource.SelectNodes("lastName");
            var temp = new LastNames();
            
            foreach (XmlNode node in nodes)
            {
                temp.Add(new LastName(node.InnerText));
            }
            
            return temp;
        }
        MaritalStates GetMaritalStates()
        {
            var nodes = resource.SelectNodes("maritalStatus");
            var temp = new MaritalStates();
            
            foreach (XmlNode node in nodes)
            {
                temp.Add(new MaritalState(node.Attributes.GetNamedItem("xml:lang").InnerText, node.InnerText));
            }
            
            return temp;
        }
        Children? GetChildren()
        {
            var nodes = resource.SelectNodes("child");
            var temp = new Children();

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
        Stressors? GetStressors()
        {
            var nodes = resource.SelectNodes("stressor");
            var temp = new Stressors();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    temp.Add(new Stressor(node.Attributes.GetNamedItem("xml:lang").InnerText, node.InnerText));
                }
            }
            return temp.Any() ? temp : null;
        }
        PersonalInfos? GetPersonalInfos()
        {
            var nodes = resource.SelectNodes("personalInfo");
            var temp = new PersonalInfos();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    temp.Add(new PersonalInfo(node.Attributes.GetNamedItem("xml:lang").InnerText, node.InnerText));
                }
            }
            return temp.Any() ? temp : null;
        }
        Professions GetProfessions()
        {
            var nodes = resource.SelectNodes("profession");
            var temp = new Professions();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    temp.Add(new Profession(node.Attributes.GetNamedItem("xml:lang").InnerText, node.InnerText));
                }
            }
            return temp;
        }
        Departments GetDepartments()
        {
            var nodes = resource.SelectNodes("department");
            var temp = new Departments();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    temp.Add(new Department(node.Attributes.GetNamedItem("xml:lang").InnerText, node.InnerText));
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
        Trainings? GetTrainings()
        {
            var nodes = resource.SelectNodes("training");
            var temp = new Trainings();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    temp.Add(new Training(node.Attributes.GetNamedItem("xml:lang").InnerText, node.InnerText));
                }
            }
            return temp.Any() ? temp : null;
        }
        Qualifications? GetQualifications()
        {
            var nodes = resource.SelectNodes("qualification");
            var temp = new Qualifications();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    temp.Add(new Qualification(node.Attributes.GetNamedItem("xml:lang").InnerText, node.InnerText));
                }
            }
            return temp.Any() ? temp : null;
        }
        WorkAgreement? GetWorkAgreement()
        {
            var nodes = resource.SelectNodes("workAgreement");
            var temp = new WorkAgreement();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string[] xml = node.InnerText.Split('-');
                    temp.Add(int.Parse(xml[0]), new TimeInterval(new DayTime(xml[1]), new DayTime(xml[2])));
                }
            }
            return temp.Any() ? temp : null;
        }
        Studies? GetStudies()
        {
            var nodes = resource.SelectNodes("studies");
            var temp = new Studies();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    temp.Add(new Study(node.Attributes.GetNamedItem("xml:lang").InnerText, node.SelectSingleNode("name").InnerText, node.SelectSingleNode("location").InnerText));
                }
            }
            return temp.Any() ? temp : null;
        }
        AdditionalJobs? GetAdditionalJobs()
        {
            var nodes = resource.SelectNodes("additionalJob");
            var temp = new AdditionalJobs();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    temp.Add(new AdditionalJob(node.Attributes.GetNamedItem("xml:lang").InnerText, node.InnerText));
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
        MeansOfTransport GetMeansOfTransport()
        {
            var nodes = resource.SelectNodes("meansOfTransport");
            var temp = new MeansOfTransport();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    temp.Add(new Vehicle(node.Attributes.GetNamedItem("xml:lang").InnerText, node.InnerText));
                }
            }
            return temp.Any() ? temp : null;
        }
        ProfessionalInfos? GetProfessionalInfos()
        {
            var nodes = resource.SelectNodes("professionalInfo");
            var temp = new ProfessionalInfos();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    temp.Add(new ProfessionalInfo(node.Attributes.GetNamedItem("xml:lang").InnerText, node.InnerText));
                }
            }
            return temp.Any() ? temp : null;
        }
        Skills GetSkills()
        {
            var nodes = resource.SelectNodes("skill");
            var temp = new Skills();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    temp.Add(new Skill(node.Attributes.GetNamedItem("xml:lang").InnerText, node.InnerText));
                }
            }
            return temp;
        }
        Traits GetTraits()
        {
            var nodes = resource.SelectNodes("trait");
            var temp = new Traits();

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    temp.Add(
                        new Trait(
                            node.Attributes.GetNamedItem("xml:lang").InnerText,
                            new KeyValuePair<string, int>(
                                node.SelectSingleNode("name").InnerText,
                                int.Parse(node.SelectSingleNode("value").InnerText)
                                )
                            )
                        );
                }
            }
            return temp;
        }
    }
}