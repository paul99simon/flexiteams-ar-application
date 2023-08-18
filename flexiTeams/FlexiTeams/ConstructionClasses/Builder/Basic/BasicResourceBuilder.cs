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
        _resource.FirstNames = firstNames;
    }

    public void Set(List<LastName> lastName)
    {
        _resource.LastNames = lastName;
    }

    public void Set(Dictionary<string, MaritalState> maritalStates)
    {
        _resource.AddRange(maritalStates);
    }

    public void Set(List<Child> children)
    {
        _resource.Children = children;
    }

    public void Set(Dictionary<string, List<Stressor>> stressors)
    {
        _resource.AddRange(stressors);
    }

    public void Set(Dictionary<string, List<PersonalInfo>> personalInfos)
    {
        _resource.AddRange(personalInfos);
    }

    public void Set(Dictionary<string, List<Profession>> professions)
    {
        _resource.AddRange(professions);
    }

    public void Set(Dictionary<string, List<Department>> departments)
    {
        _resource.AddRange(departments);
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

    public void Set(Dictionary<string, List<Training>> trainings)
    {
        _resource.AddRange(trainings);
    }

    public void Set(Dictionary<string, List<Qualification>> qualifications)
    {
        _resource.AddRange(qualifications);
    }

    public void Set(List<TimeInterval>[] workAgreement)
    {
        _resource.AddRange(workAgreement);
    }

    public void Set(Dictionary<string, List<Studies>> studies)
    {
        _resource.AddRange(studies);
    }

    public void Set(Dictionary<string, List<AdditionalJob>> additionalJobs)
    {
        _resource.AddRange(additionalJobs);
    }

    public void Set(ArrivalTime arrivalTime)
    {
        _resource.ArrivalTime = arrivalTime;
    }

    public void Set(Dictionary<string, List<Vehicle>> meansOfTransport)
    {
        _resource.AddRange(meansOfTransport);
    }

    public void Set(Dictionary<string, List<ProfessionalInfo>> professionalInfos)
    {
        _resource.AddRange(professionalInfos);
    }

    public void Set(Dictionary<string, List<Skill>> skills)
    {
        _resource.AddRange(skills);
    }

    public void Set(Dictionary<string, List<Trait>> traits)
    {
        _resource.AddRange(traits);
    }

    public void SetLanguage(string langCode)
    {
        _resource.SetLanguage(langCode);
    }

    public string GetLanguage()
    {
        return _resource.GetLanguage();
    }
}