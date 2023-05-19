using System.Xml;
using flexiTeams.Data.Wrapper;
using FlexiTeams.Data.Wrapper;

namespace flexiTeams.Data;

public class Resource
{
    public Resource(XmlReader reader)
    {
        while(reader.Read()) Console.Write(reader.ReadInnerXml());
    }
    
    //personal Info
    private Photo _photo;
    private Age _age;
    private Prefix _prefix;
    private List<FirstName> _firstNames;
    private List<LastName> _lastNames;
    private List<MaritalStatus> _maritalStatus;
    private List<Child> _children;
    private List<Stressor> _stressors;
    private List<PersonalInfo> _personalInfos;

    //professional Info
    private List<Profession> _professions;
    private List<Department> _departments;
    private WorkExperience _workExperience;
    private TrainingDuration _trainingDuration;
    private WeeklyHours _weeklyHours;
    private Overtime _overtime;
    private YearlyEducation _yearlyEducation;
    private List<Training> _trainings;
    private List<Qualification> _qualifications;
    private List<WorkAgreement> _workAgreements;

    //skills
    private List<Skill> _skills;
    //traits
    private List<Trait> _traits;
}