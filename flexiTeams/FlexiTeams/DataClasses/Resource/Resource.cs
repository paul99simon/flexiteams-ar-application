using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.DataClasses.Resource;

public class Resource
{
    //personal Info
    public Photos? Photos { get; set; }
    public Age? Age { get; set; }
    public Prefix? Prefix { get; set; }
    public FirstNames FirstNames { get; set; }
    public LastNames? LastNames { get; set; }
    public MaritalStates? MaritalStates { get; set; }
    public Children? Children { get; set; }
    public Stressors? Stressors { get; set; }
    public PersonalInfos? PersonalInfos { get; set; }

    //professional Info
    public Professions? Professions { get; set; }
    public Departments? Departments { get; set; }
    public WorkExperience? WorkExperience { get; set; }
    public TrainingDuration? TrainingDuration { get; set; }
    public WeeklyHours? WeeklyHours { get; set; }
    public Overtime? Overtime { get; set; }
    public YearlyTimeOf YearlyTimeOf { get; set; }
    public YearlyEducation? YearlyEducation { get; set; }
    public Trainings? Trainings { get; set; }
    public Qualifications? Qualifications { get; set; }
    public WorkAgreement? WorkAgreement { get; set; }
    public Studies? Studies { get; set; }
    public AdditionalJobs? AdditionalJobs { get; set; }
    public ArrivalTime? ArrivalTime { get; set; }
    public MeansOfTransport? MeansOfTransport { get; set; }
    public ProfessionalInfos? ProfessionalInfos { get; set; }
    //skills
    public Skills? Skills { get; set; }
    //traits
    public Traits? Traits { get; set; }
}