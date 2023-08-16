using FlexiTeams.DataClasses.Resource;
using FlexiTeams.DataClasses.Resource.Wrapper;

namespace FlexiTeams.ConstructionClasses.Builder;

public class BasicResourceBuilder : IResourceBuilder
{

    private Resource _resource = new();
    
    public void Reset()
    {
        _resource = new Resource();
    }
    public Resource GetResource()
    {
        Resource resource = _resource;
        Reset();
        return resource;
    }

    public void SetPhotos(Photos? photos)
    {
        _resource.Photos = photos;
    }
    public void SetAge(Age age)
    {
        _resource.Age = age;
    }
    public void SetPrefix(Prefix prefix)
    {
        _resource.Prefix = prefix;
    }
    public void SetFirstNames(FirstNames firstNames)
    {
        _resource.FirstNames = firstNames;
    }
    public void SetLastNames(LastNames lastNames)
    {
        _resource.LastNames = lastNames;
    }
    public void SetMaritalStates(MaritalStates maritalStates)
    {
        _resource.MaritalStates = maritalStates;
    }
    public void SetChildren(Children children)
    {
        _resource.Children = children;
    }
    public void SetStressors(Stressors stressors)
    {
        _resource.Stressors = stressors;
    }
    public void SetPersonalInfos(PersonalInfos personalInfos)
    {
        _resource.PersonalInfos = personalInfos;
    }
    public void SetProfessions(Professions professions)
    {
        _resource.Professions = professions;
    }
    public void SetDepartments(Departments departments)
    {
        _resource.Departments = departments;
    }
    public void SetWorkExperience(WorkExperience workExperience)
    {
        _resource.WorkExperience = workExperience;
    }
    public void SetTrainingDuration(TrainingDuration trainingDuration)
    {
        _resource.TrainingDuration = trainingDuration;
    }
    public void SetWeeklyHours(WeeklyHours weeklyHours)
    {
        _resource.WeeklyHours = weeklyHours;
    }
    public void SetOvertime(Overtime overtime)
    {
        _resource.Overtime = overtime;
    }
    public void SetYearlyTimeOf(YearlyTimeOf yearlyTimeOf)
    {
        _resource.YearlyTimeOf = yearlyTimeOf;
    }
    public void SetYearlyEducation(YearlyEducation yearlyEducation)
    {
        _resource.YearlyEducation = yearlyEducation;
    }
    public void SetTrainings(Trainings trainings)
    {
        _resource.Trainings = trainings;
    }
    public void SetQualifications(Qualifications qualifications)
    {
        _resource.Qualifications = qualifications;
    }
    public void SetWorkAgreement(WorkAgreement workAgreement)
    {
        _resource.WorkAgreement = workAgreement;
    }
    public void SetStudies(Studies studies)
    {
        _resource.Studies = studies;
    }
    public void SetAdditionalJobs(AdditionalJobs additionalJobs)
    {
        _resource.AdditionalJobs = additionalJobs;
    }
    public void SetArrivalTime(ArrivalTime arrivalTime)
    {
        _resource.ArrivalTime = arrivalTime;
    }
    public void SetMeansOfTransport(MeansOfTransport meansOfTransport)
    {
        _resource.MeansOfTransport = meansOfTransport;
    }
    public void SetProfessionalInfos(ProfessionalInfos professionalInfos)
    {
        _resource.ProfessionalInfos = professionalInfos;
    }
    public void SetSkills(Skills skills)
    {
        _resource.Skills = skills;
    }
    public void SetTraits(Traits traits)
    {
        _resource.Traits = traits;
    }
}