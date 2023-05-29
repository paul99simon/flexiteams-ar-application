using System.Xml;
using FlexiTeams.Data.Wrapper;

namespace FlexiTeams.Data;

public class ResourceDirector
{
    
    //Xml-Construction
    public void ConstructFromXmlReader(IResourceBuilder builder, XmlReader reader)
    {
        var photo = GetPhoto(reader);
        var age = GetAge(reader);
        var prefix = GetPrefix(reader);
        var firstNames = GetFirstNames(reader);
        var lastNames = GetLastNames(reader);
        var maritalStates = GetMaritalStates(reader);
        var children = GetChildren(reader);
        var stressors = GetStressors(reader);
        var personalInfos = GetPersonalInfos(reader);
        var professions = GetProfessions(reader);
        var departments = GetDepartments(reader);
        var workExperience = getWorkExperience(reader);
        var traingingDuration = getTrainingDuration(reader);
        var weeklyHours = GetWeeklyHours(reader);
        var overTime = GetOvertime(reader);
        var yearlyTimeOf = GetYearlyTimeOf(reader);
        var yearlyEducation = GetYearlyEducation(reader);
        var trainings = GetTrainings(reader);
        var qualifications = GetQualifications(reader);
        var workAgreement = GetWorkAgreement(reader);
        var professionalInfos = GetProfessionalInfos(reader);
        var skills = GetSkills(reader);
        var traits = GetTraits(reader);
            
        builder.SetPhoto(photo);
        builder.SetAge(age);
        builder.SetPrefix(prefix);
        builder.SetFirstNames(firstNames);
        builder.SetLastNames(lastNames);
        builder.SetMaritalStates(maritalStates);
        builder.SetChildren(children);
        builder.SetStressors(stressors);
        builder.SetPersonalInfos(personalInfos);
        builder.SetProfessions(professions);
        builder.SetDepartments(departments);
        builder.SetWorkExperience(workExperience);
        builder.SetTrainingDuration(traingingDuration);
        builder.SetWeeklyHours(weeklyHours);
        builder.SetOvertime(overTime);
        builder.SetYearlyTimeOf(yearlyTimeOf);
        builder.SetYearlyEducation(yearlyEducation);
        builder.SetTrainings(trainings);
        builder.SetQualifications(qualifications);
        builder.SetWorkAgreement(workAgreement);
        builder.SetProfessionalInfos(professionalInfos);
        builder.SetSkills(skills);
        builder.SetTraits(traits);
    }

    private Photo GetPhoto(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private Age GetAge(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private Prefix GetPrefix(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private FirstNames GetFirstNames(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private LastNames GetLastNames(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private MaritalStates GetMaritalStates(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private Children GetChildren(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private Stressors GetStressors(XmlReader reader)
    {
        
        throw new NotImplementedException();
    }
    private PersonalInfos GetPersonalInfos(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private Professions GetProfessions(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private Departments GetDepartments(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private WorkExperience getWorkExperience(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private TrainingDuration getTrainingDuration(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private WeeklyHours GetWeeklyHours(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private Overtime GetOvertime(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    private YearlyTimeOf GetYearlyTimeOf(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private YearlyEducation GetYearlyEducation(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private Trainings GetTrainings(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private Qualifications GetQualifications(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private WorkAgreement GetWorkAgreement(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private ProfessionalInfos GetProfessionalInfos(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private Skills GetSkills(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    private Traits GetTraits(XmlReader reader)
    {
        throw new NotImplementedException();
    }
}