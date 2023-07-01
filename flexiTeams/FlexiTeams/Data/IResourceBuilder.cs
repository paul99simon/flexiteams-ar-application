using FlexiTeams.Data.Wrapper;

namespace FlexiTeams.Data;

public interface IResourceBuilder
{
    
    public void Reset();
    public Resource GetResource();
    
    public void SetPhotos(Photos? photos);
    public void SetAge(Age age);
    public void SetPrefix(Prefix prefix);
    public void SetFirstNames(FirstNames firstNames);
    public void SetLastNames(LastNames lastName);
    public void SetMaritalStates(MaritalStates maritalStatus);
    public void SetChildren(Children children);
    public void SetStressors(Stressors stressors);
    public void SetPersonalInfos(PersonalInfos personalInfos);

    public void SetProfessions(Professions professions);
    public void SetDepartments(Departments departments);
    public void SetWorkExperience(WorkExperience workExperience);
    public void SetTrainingDuration(TrainingDuration trainingDuration);
    public void SetWeeklyHours(WeeklyHours weeklyHours);
    public void SetOvertime(Overtime overtime);
    public void SetYearlyTimeOf(YearlyTimeOf yearlyTimeOf);
    public void SetYearlyEducation(YearlyEducation yearlyEducation);
    public void SetTrainings(Trainings training);
    public void SetQualifications(Qualifications qualifications);
    public void SetWorkAgreement(WorkAgreement workAgreement);
    public void SetStudies(Studies sttudies);
    public void SetAdditionalJobs(AdditionalJobs additionalJobs);
    public void SetArrivalTime(ArrivalTime arrivalTime);
    public void SetMeansOfTransport(MeansOfTransport meansOfTransport);
    public void SetProfessionalInfos(ProfessionalInfos professionalInfos);

    public void SetSkills(Skills skills);

    public void SetTraits(Traits traits);
}