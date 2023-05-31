using System.Xml;
using FlexiTeams.Data.Wrapper;

namespace FlexiTeams.Data;

public class ResourceDirector
{
    
    //Xml-Construction
    public void ConstructFromXmlNode(IResourceBuilder builder, XmlNode resource)
    {
        var photos = GetPhotos();
        var age = GetAge();
        var  prefix = GetPrefix();
        var firstNames = GetFirstNames();
        var lastNames = GetLastNames();
        var  maritalStates = GetMaritalStates();
        var children = GetChildren();
        var stressors = GetStressors();
        /*var personalInfos = GetPersonalInfos();
        var professions = GetProfessions();
        var departments = GetDepartments();
        var workExperience = GetWorkExperience();
        var trainingDuration = GetTrainingDuration();
        var weeklyHours = GetWeeklyHours();
        var overTime = GetOvertime();
        var yearlyTimeOf = GetYearlyTimeOf();
        var yearlyEducation = GetYearlyEducation();
        var trainings = GetTrainings();
        var qualifications = GetQualifications();
        var workAgreement = GetWorkAgreement();
        var professionalInfos = GetProfessionalInfos();
        var skills = GetSkills();
        var traits = GetTraits();*/
         
        
        builder.SetPhotos(photos);
        builder.SetAge(age);
        builder.SetPrefix(prefix);
        builder.SetFirstNames(firstNames);
        builder.SetLastNames(lastNames);
        builder.SetMaritalStates(maritalStates);
        builder.SetChildren(children);
        builder.SetStressors(stressors);
        /*builder.SetPersonalInfos(personalInfos);
        builder.SetProfessions(professions);
        builder.SetDepartments(departments);
        builder.SetWorkExperience(workExperience);
        builder.SetTrainingDuration(trainingDuration);
        builder.SetWeeklyHours(weeklyHours);
        builder.SetOvertime(overTime);
        builder.SetYearlyTimeOf(yearlyTimeOf);
        builder.SetYearlyEducation(yearlyEducation);
        builder.SetTrainings(trainings);
        builder.SetQualifications(qualifications);
        builder.SetWorkAgreement(workAgreement);
        builder.SetProfessionalInfos(professionalInfos);
        builder.SetSkills(skills);
        builder.SetTraits(traits);*/
        
        Photos? GetPhotos()
        {
            var photoNodes = resource.SelectNodes("photo");
            var temp = new Photos();

            if (photoNodes != null)
            {
                foreach (XmlNode photo in photoNodes)
                {
                    var URI = photo.SelectSingleNode("URI");
                    temp.Add(new Photo(URI.InnerText));
                }
            }
            return temp.Any() ? temp : null;
        }
        Age GetAge()
        {
            var ageNode = resource.SelectSingleNode("age");
            var timespan = XmlConvert.ToTimeSpan(ageNode.InnerText);

            return new Age(timespan.Days / 365);
        }
        Prefix? GetPrefix()
        {
            var prefixNode = resource.SelectSingleNode("prefix");
            return prefixNode == null ? null : new Prefix(prefixNode.InnerText);
        }
        FirstNames GetFirstNames()
        {
            var firstNameNodes = resource.SelectNodes("firstName");
            var temp = new FirstNames();
            
            foreach (XmlNode firstName in firstNameNodes)
            {
                temp.Add(new FirstName(firstName.InnerText));
            }
            
            return temp;
        }
        LastNames GetLastNames()
        {
            var lastNameNodes = resource.SelectNodes("lastName");
            var temp = new LastNames();
            
            foreach (XmlNode lastName in lastNameNodes)
            {
                temp.Add(new LastName(lastName.InnerText));
            }
            
            return temp;
        }
        MaritalStates GetMaritalStates()
        {
            var maritalStateNode = resource.SelectNodes("maritalStatus");
            var temp = new MaritalStates();
            
            foreach (XmlNode maritalState in maritalStateNode)
            {
                temp.Add(new MaritalState(maritalState.Attributes.GetNamedItem("xml:lang").InnerText, maritalState.InnerText));
            }
            
            return temp;
        }
        Children? GetChildren()
        {
            var childNotes = resource.SelectNodes("child");
            var temp = new Children();

            if (childNotes != null)
            {
                foreach (XmlNode child in childNotes)
                {
                    var ageNode = child.SelectSingleNode("age");
                    var timespan = XmlConvert.ToTimeSpan(ageNode.InnerText);
                    temp.Add(new Child(timespan.Days/365));
                }
            }
            return temp.Any() ? temp : null;
        }
        Stressors? GetStressors()
        {
            var stressorNodes = resource.SelectNodes("stressor");
            var temp = new Stressors();

            if (stressorNodes != null)
            {
                foreach (XmlNode stressor in stressorNodes)
                {
                    temp.Add(new Stressor(stressor.Attributes.GetNamedItem("xml:lang").InnerText, stressor.InnerText));
                }
            }
            return temp.Any() ? temp : null;
        }
        PersonalInfos GetPersonalInfos()
        {
            throw new NotImplementedException();
        }
        Professions GetProfessions()
        {
            throw new NotImplementedException();
        }
        Departments GetDepartments()
        {
            throw new NotImplementedException();
        }
        WorkExperience GetWorkExperience()
        {
            throw new NotImplementedException();
        }
        TrainingDuration GetTrainingDuration()
        {
            throw new NotImplementedException();
        }
        WeeklyHours GetWeeklyHours()
        {
            throw new NotImplementedException();
        }
        Overtime GetOvertime()
        {
            throw new NotImplementedException();
        }
        YearlyTimeOf GetYearlyTimeOf()
        {
            throw new NotImplementedException();
        }
        YearlyEducation GetYearlyEducation()
        {
            throw new NotImplementedException();
        }
        Trainings GetTrainings()
        {
            throw new NotImplementedException();
        }
        Qualifications GetQualifications()
        {
            throw new NotImplementedException();
        }
        WorkAgreement GetWorkAgreement()
        {
            throw new NotImplementedException();
        }
        ProfessionalInfos GetProfessionalInfos()
        {
            throw new NotImplementedException();
        }
        Skills GetSkills()
        {
            throw new NotImplementedException();
        }
        Traits GetTraits()
        {
            throw new NotImplementedException();
        }
    }
}