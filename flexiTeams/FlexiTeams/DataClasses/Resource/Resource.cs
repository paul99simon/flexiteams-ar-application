using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.DataClasses.Resource;

public class Resource
{
    //personal Info
    public ResourceId Id { get; set; }
    
    public List<Photo> Photos { get; } = new();
    
    public Age? Age { get; set; }
    
    public Prefix? Prefix { get; set; }
    
    public List<FirstName> FirstNames { get; } = new();
    
    public List<LastName> LastNames { get; } = new();
    
    public MaritalState? MaritalState { get; set; }

    public List<Child> Children { get; } = new();

    public List<Stressor> Stressors { get; } = new();

    
    public List<PersonalInfo> PersonalInfos { get; } = new();

    //professional Info
    public List<Profession> Professions { get; } = new();
   
    public List<Department> Departments { get; } = new();

    public WorkExperience? WorkExperience { get; set; }
    
    public TrainingDuration? TrainingDuration { get; set; }
    
    public WeeklyHours? WeeklyHours { get; set; }

    public Overtime? Overtime { get; set; }

    public YearlyTimeOf? YearlyTimeOf { get; set; }
    
    public YearlyEducation? YearlyEducation { get; set; }

    public List<Training> Trainings { get; } = new ();
    

    public List<Qualification> Qualifications { get; } = new();

    public List<TimeInterval>[] WorkAgreement { get; } = new List<TimeInterval>[]
    {
        new List<TimeInterval>(),
        new List<TimeInterval>(),
        new List<TimeInterval>(),
        new List<TimeInterval>(),
        new List<TimeInterval>(),
        new List<TimeInterval>(),
        new List<TimeInterval>()
    };
    public void Add(int index, TimeInterval timeInterval)
    {
        WorkAgreement[index].Add(timeInterval);
    }
    public void AddRange(List<TimeInterval>[] workAgreement)
    {
        if(workAgreement == null) return;
        for (int i = 0; i < 7; i++)
        {
            WorkAgreement[i].AddRange(workAgreement[i]);

            foreach (var ti1 in WorkAgreement[i])
            {
                foreach (var ti2 in WorkAgreement[i])
                {
                    if (ti1 != ti2 & ti1.Equals(ti2)) WorkAgreement[i].Remove(ti2);
                }
            }
        }
        
    }
    public bool IsAgreedTime(int weekDay, TimeInterval ti)
    {
        foreach (TimeInterval interval in WorkAgreement[weekDay])
        {
            if (interval.Contains(ti)) return true;
        }
       
        return false;
    }

    public List<Studies> Studies { get; } = new();

    public List<AdditionalJob> AdditionalJobs { get; } = new();

    public ArrivalTime? ArrivalTime { get; set; }

    public List<Vehicle> MeansOfTransport { get; } = new();

    public List<ProfessionalInfo> ProfessionalInfos { get; } = new();

    public List<Skill> Skills { get; } = new();

    public List<Trait> Traits { get; } = new();

}