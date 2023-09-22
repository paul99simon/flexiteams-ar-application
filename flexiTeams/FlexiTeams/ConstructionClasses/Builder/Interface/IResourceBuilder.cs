using FlexiTeams.DataClasses.Resource;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.ConstructionClasses.Builder.Interface;

public interface IResourceBuilder
{
    public void Reset();
    public Resource GetResource();

    public void Set(ResourceId id);
    public void Set(List<Photo> photos);
    public void Set(Age age);
    public void Set(Prefix prefix);
    public void Set(List<FirstName> firstNames);
    public void Set(List<LastName> lastName);
    public void Set(MaritalState maritalState);
    public void Set(List<Child> children);
    public void Set(List<Stressor> stressors);
    public void Set(List<PersonalInfo> personalInfos);
    public void Set(List<Profession> professions);
    public void Set(List<Department> departments);
    public void Set(WorkExperience workExperience);
    public void Set(TrainingDuration trainingDuration);
    public void Set(WeeklyHours weeklyHours);
    public void Set(Overtime overtime);
    public void Set(YearlyTimeOf yearlyTimeOf);
    public void Set(YearlyEducation yearlyEducation);
    public void Set(List<Training> trainings);
    public void Set(List<Qualification> qualifications);
    public void Set(List<TimeInterval>[] workAgreement);
    public void Set(List<Studies> studies);
    public void Set(List<AdditionalJob> additionalJobs);
    public void Set(CommuteTime arrivalTime);
    public void Set(List<Vehicle> meansOfTransport);
    public void Set(List<ProfessionalInfo> professionalInfos);
    public void Set(List<Skill> skills);
    public void Set(List<Trait> traits);
}