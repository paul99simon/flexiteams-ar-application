using flexiTeams.Data.Wrapper;

namespace flexiTeams.Data;

public class Resource
{
    //personal Info
    public Uri Image;
    public int Age;
    public string PreFix;
    public List<string> FirstName;
    public List<string> LastName;
    public string MaritalStatus;
    public List<Child> Children;
    public List<string> Stressors;
    public string PersonalInfo;

    //professional Info
    public List<string> Professions;
    public List<string> Departments;
    public int WorkExperience;
    public int TrainingDuration;
    public float WeeklyHours;
    public float Overtime;
    public int YearlyTimeOf;
    public int YearlyEducation;
    public List<string> Training;
    public List<string> Qualifications;
    public List<WorkAgreement> WorkAgreements;

    //skills
    //traits
}