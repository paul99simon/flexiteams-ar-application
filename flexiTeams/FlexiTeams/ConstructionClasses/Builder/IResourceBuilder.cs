using FlexiTeams.DataClasses.Resource;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.ConstructionClasses.Builder;

public interface IResourceBuilder : ILanguageObject
{
    public void Reset();
    public Resource GetResource();

    public void Set(ResourceId id);
    public void Set(List<Photo> photos);
    public void Set(Age age);
    public void Set(Prefix prefix);
    public void Set(List<FirstName> firstNames);
    public void Set(List<LastName> lastName);
    public void Set(Dictionary<string, MaritalState> maritalStates);
    public void Set(List<Child> children);
    public void Set(Dictionary<string, List<Stressor>> stressors);
    public void Set(Dictionary<string, List<PersonalInfo>> personalInfos);
    public void Set(Dictionary<string, List<Profession>> professions);
    public void Set(Dictionary<string, List<Department>> departments);
    public void Set(WorkExperience workExperience);
    public void Set(TrainingDuration trainingDuration);
    public void Set(WeeklyHours weeklyHours);
    public void Set(Overtime overtime);
    public void Set(YearlyTimeOf yearlyTimeOf);
    public void Set(YearlyEducation yearlyEducation);
    public void Set(Dictionary<string, List<Training>> trainings);
    public void Set(Dictionary<string, List<Qualification>> qualifications);
    public void Set(List<TimeInterval>[] workAgreement);
    public void Set(Dictionary<string, List<Studies>> studies);
    public void Set(Dictionary<string, List<AdditionalJob>> additionalJobs);
    public void Set(ArrivalTime arrivalTime);
    public void Set(Dictionary<string, List<Vehicle>> meansOfTransport);
    public void Set(Dictionary<string, List<ProfessionalInfo>> professionalInfos);
    public void Set(Dictionary<string, List<Skill>> skills);
    public void Set(Dictionary<string, List<Trait>> traits);
}