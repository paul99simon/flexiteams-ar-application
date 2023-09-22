using FlexiTeams.ConstructionClasses.Builder.Interface;
using FlexiTeams.DataClasses.Resource;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;

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

    public void Set(ResourceId id)
    {
        _resource.Id = id;
    }
    public void Set(List<Photo> photos)
    {
        _resource.Photos = photos;
    }
    public void Set(Age age)
    {
        _resource.Age = age;
    }
    public void Set(Prefix prefix)
    {
        _resource.Prefix = prefix;
    }
    public void Set(List<FirstName> firstNames)
    {
        _resource.FirstNames.AddRange(firstNames);
    }
    public void Set(List<LastName> lastName)
    {
        _resource.LastNames.AddRange(lastName);
    }
    public void Set(MaritalState maritalState)
    {
        _resource.MaritalState = maritalState;
    }
    public void Set(List<Child> children)
    {
        _resource.Children = children;
    }
    public void Set(List<Stressor> stressors)
    {
        _resource.Stressors = stressors;
    }
    public void Set(List<PersonalInfo> personalInfos)
    {
        _resource.PersonalInfos = personalInfos;
    }
    public void Set(List<Profession> professions)
    {
        _resource.Professions = professions;
    }
    public void Set(List<Department> departments)
    {
        _resource.Departments = departments;
    }
    public void Set(WorkExperience workExperience)
    {
        _resource.WorkExperience = workExperience;
    }
    public void Set(TrainingDuration trainingDuration)
    {
        _resource.TrainingDuration = trainingDuration;
    }
    public void Set(WeeklyHours weeklyHours)
    {
        _resource.WeeklyHours = weeklyHours;
    }
    public void Set(Overtime overtime)
    {
        _resource.Overtime = overtime;
    }
    public void Set(YearlyTimeOf yearlyTimeOf)
    {
        _resource.YearlyTimeOf = yearlyTimeOf;
    }
    public void Set(YearlyEducation yearlyEducation)
    {
        _resource.YearlyEducation = yearlyEducation;
    }
    public void Set(List<Training> trainings)
    {
        _resource.Trainings = trainings;
    }
    public void Set(List<Qualification> qualifications)
    {
        _resource.Qualifications = qualifications;
    }
    public void Set(List<TimeInterval>[] workAgreement)
    {
        _resource.AddRange(workAgreement);
    }
    public void Set(List<Studies> studies)
    {
        _resource.Studies = studies;
    }
    public void Set(List<AdditionalJob> additionalJobs)
    {
        _resource.AdditionalJobs = additionalJobs;
    }
    public void Set(CommuteTime arrivalTime)
    {
        _resource.CommuteTime = arrivalTime;
    }
    public void Set(List<Vehicle> meansOfTransport)
    {
        _resource.MeansOfTransport = meansOfTransport;
    }
    public void Set(List<ProfessionalInfo> professionalInfos)
    {
        _resource.ProfessionalInfos = professionalInfos;
    }
    public void Set(List<Skill> skills)
    {
        _resource.Skills.AddRange(skills);
    }
    public void Set(List<Trait> traits)
    {
        _resource.Traits.AddRange(traits);
    }
}