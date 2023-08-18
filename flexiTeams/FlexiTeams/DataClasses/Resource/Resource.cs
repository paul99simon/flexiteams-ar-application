using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.DataClasses.Resource;

public class Resource : ILanguageObject
{
    //personal Info
    public ResourceId Id { get; set; }
    
    public List<Photo> Photos { get; set; } = new();
    
    public Age? Age { get; set; }
    
    public Prefix? Prefix { get; set; }
    
    public List<FirstName> FirstNames { get; set; } = new();
    
    public List<LastName> LastNames { get; set; } = new();
    
    public MaritalState? MaritalState => _maritalStates.ContainsKey(_langCode) ? _maritalStates[_langCode] : null;
    private readonly Dictionary<string, MaritalState> _maritalStates = new();
    public void Add(string langCode, MaritalState maritalState)
    {
        if (!ISO_639_1.IsValidCode(langCode)) return;
        if(! _maritalStates.ContainsKey(langCode)) _maritalStates.Add(langCode, maritalState);
    }
    public void AddRange(Dictionary<string, MaritalState> maritalStates)
    {
        if(maritalStates == null) return;

        foreach (var pair in maritalStates)
        {
            _maritalStates[pair.Key] = pair.Value;
        }
    }

    public List<Child> Children { get; set; } = new();

    public List<Stressor>? Stressors => _stressors.ContainsKey(_langCode) ? _stressors[_langCode] : null;
    private readonly Dictionary<string, List<Stressor>> _stressors = new();
    public void Add(string langCode, Stressor stressor)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if(! _stressors.ContainsKey(langCode)) _stressors.Add(langCode, new List<Stressor>());
        _stressors[langCode].Add(stressor);
    }
    public void AddRange(Dictionary<string, List<Stressor>> stressors)
    {
        if(stressors == null) return;
        foreach (var pair in stressors)
        {
            if (!_stressors.ContainsKey(pair.Key)) _stressors.Add(pair.Key, pair.Value);
            else _stressors[pair.Key].AddRange(pair.Value);
        }
    }
    
    public List<PersonalInfo>? PersonalInfos => _personalInfos.ContainsKey(_langCode) ? _personalInfos[_langCode] : null;
    private readonly Dictionary<string, List<PersonalInfo>> _personalInfos = new();
    public void Add(string langCode, PersonalInfo personalInfo)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if(! _personalInfos.ContainsKey(langCode)) _personalInfos.Add(langCode, new List<PersonalInfo>());
        _personalInfos[langCode].Add(personalInfo);
    }
    public void AddRange(Dictionary<string, List<PersonalInfo>> personalInfos)
    {
        if(personalInfos == null) return;
        foreach (var pair in personalInfos)
        {
            if (! _personalInfos.ContainsKey(pair.Key)) _personalInfos.Add(pair.Key, pair.Value);
            else _personalInfos[pair.Key].AddRange(pair.Value);
        }
    }
    
    //professional Info
    public List<Profession>? Professions => _professions.ContainsKey(_langCode) ? _professions[_langCode] : null;
    private readonly Dictionary<string, List<Profession>> _professions = new();
    public void Add(string langCode, Profession profession)
    {
        if (!ISO_639_1.IsValidCode(langCode)) return;
        if (!_professions.ContainsKey(langCode)) _professions.Add(langCode, new List<Profession>());
        _professions[langCode].Add(profession);
    }
    public void AddRange(Dictionary<string, List<Profession>> professions)
    {
        if(professions == null) return;
        foreach (var pair in professions)
        {
            if (!_professions.ContainsKey(pair.Key)) _professions.Add(pair.Key, pair.Value);
            else _professions[pair.Key].AddRange(pair.Value);
        }
    }

    public List<Department>? Departments => _departments.ContainsKey(_langCode) ? _departments[_langCode] : null;
    private readonly Dictionary<string, List<Department>> _departments = new();
    public void Add(string langCode, Department department)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if(! _departments.ContainsKey(langCode)) _departments.Add(langCode, new List<Department>());
        _departments[langCode].Add(department);
    }
    public void AddRange(Dictionary<string, List<Department>> departments)
    {
        if(departments == null) return;
        foreach (var pair in departments)
        {
            if (!_departments.ContainsKey(pair.Key)) _departments.Add(pair.Key, pair.Value);
            else _departments[pair.Key].AddRange(pair.Value);
        }
    }
    
    public WorkExperience? WorkExperience { get; set; }
    
    public TrainingDuration? TrainingDuration { get; set; }
    
    public WeeklyHours? WeeklyHours { get; set; }

    public Overtime? Overtime { get; set; }

    public YearlyTimeOf? YearlyTimeOf { get; set; }
    
    public YearlyEducation? YearlyEducation { get; set; }

    public List<Training>? Trainings => _trainings.ContainsKey(_langCode) ? _trainings[_langCode] : null;
    private readonly Dictionary<string, List<Training>> _trainings = new();
    public void Add(string langCode, Training training)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if(! _trainings.ContainsKey(langCode)) _trainings.Add(langCode, new List<Training>());
        _trainings[langCode].Add(training);
    }
    public void AddRange(Dictionary<string, List<Training>> trainings)
    {
        if(trainings == null) return;
        foreach (var pair in trainings)
        {
            if (!_trainings.ContainsKey(pair.Key)) _trainings.Add(pair.Key, pair.Value);
            else _trainings[pair.Key].AddRange(pair.Value);
        }
    }

    public List<Qualification>? Qualification => _qualifications.ContainsKey(_langCode) ? _qualifications[_langCode] : null;
    private readonly Dictionary<string, List<Qualification>> _qualifications = new();
    public void Add(string langCode, Qualification qualification)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if(! _qualifications.ContainsKey(langCode)) _qualifications.Add(langCode, new List<Qualification>());
        _qualifications[langCode].Add(qualification);
    }
    public void AddRange(Dictionary<string, List<Qualification>> qualifications)
    {
        if(qualifications == null) return;
        foreach (var pair in qualifications)
        {
            if (!_qualifications.ContainsKey(pair.Key)) _qualifications.Add(pair.Key, pair.Value);
            else _qualifications[pair.Key].AddRange(pair.Value);
        }
    }
    
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

    public List<Studies>? Studies => _studies.ContainsKey(_langCode) ? _studies[_langCode] : null;
    private readonly Dictionary<string, List<Studies>> _studies = new();
    public void Add(string langCode, Studies studies)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if(! _studies.ContainsKey(langCode)) _studies.Add(langCode, new List<Studies>());
        _studies[langCode].Add(studies);
    }
    public void AddRange(Dictionary<string, List<Studies>> studies)
    {
        if(studies == null) return;
        foreach (var pair in studies)
        {
            if (!_studies.ContainsKey(pair.Key)) _studies.Add(pair.Key, pair.Value);
            else _studies[pair.Key].AddRange(pair.Value);
        }
    }
    
    public List<AdditionalJob>? AdditionalJobs => _additionalJobs.ContainsKey(_langCode) ? _additionalJobs[_langCode] : null;
    private readonly Dictionary<string, List<AdditionalJob>> _additionalJobs = new();
    public void Add(string langCode, AdditionalJob additionalJob)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if(! _additionalJobs.ContainsKey(langCode)) _additionalJobs.Add(langCode, new List<AdditionalJob>());
        _additionalJobs[langCode].Add(additionalJob);
    }
    public void AddRange(Dictionary<string, List<AdditionalJob>> additionalJobs)
    {
        if (additionalJobs == null) return;
        foreach (var pair in additionalJobs)
        {
            if (!_additionalJobs.ContainsKey(pair.Key)) _additionalJobs.Add(pair.Key, pair.Value);
            else _additionalJobs[pair.Key].AddRange(pair.Value);
        }
    }
    
    public ArrivalTime? ArrivalTime { get; set; }

    public List<Vehicle>? MeansOfTransport => _meansOfTransport.ContainsKey(_langCode) ? _meansOfTransport[_langCode] : null;
    private readonly Dictionary<string, List<Vehicle>> _meansOfTransport = new();
    public void Add(string langCode, Vehicle vehicle)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if(! _meansOfTransport.ContainsKey(langCode)) _meansOfTransport.Add(langCode, new List<Vehicle>());
        _meansOfTransport[langCode].Add(vehicle);
    }
    public void AddRange(Dictionary<string, List<Vehicle>> meansOfTransport)
    {
        if(meansOfTransport == null) return;
        foreach (var pair in meansOfTransport)
        {
            if (!_meansOfTransport.ContainsKey(pair.Key)) _meansOfTransport.Add(pair.Key, pair.Value);
            else _meansOfTransport[pair.Key].AddRange(pair.Value);
        }
    }
    
    public List<ProfessionalInfo>? ProfessionalInfos => _professionalInfos.ContainsKey(_langCode) ? _professionalInfos[_langCode] : null;
    private readonly Dictionary<string, List<ProfessionalInfo>> _professionalInfos = new();
    public void Add(string langCode, ProfessionalInfo professionalInfo)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if(! _professionalInfos.ContainsKey(langCode)) _professionalInfos.Add(langCode, new List<ProfessionalInfo>());
        _professionalInfos[langCode].Add(professionalInfo);
    }
    public void AddRange(Dictionary<string, List<ProfessionalInfo>> professionalInfos)
    {
        if(professionalInfos == null) return;
        foreach (var pair in professionalInfos)
        {
            if (!_professionalInfos.ContainsKey(pair.Key)) _professionalInfos.Add(pair.Key, pair.Value);
            else _professionalInfos[pair.Key].AddRange(pair.Value);
        }
    }
    
    public List<Skill>? Skills => _skills.ContainsKey(_langCode) ? _skills[_langCode] : null;
    private readonly Dictionary<string, List<Skill>> _skills = new();
    public void Add(string langCode, Skill skill)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if(! _skills.ContainsKey(langCode)) _skills.Add(langCode, new List<Skill>());
        _skills[langCode].Add(skill);
    }
    public void AddRange(Dictionary<string, List<Skill>> skills)
    {
        if(skills == null) return;
        foreach (var pair in skills)
        {
            if (!_skills.ContainsKey(pair.Key)) _skills.Add(pair.Key, pair.Value);
            else _skills[pair.Key].AddRange(pair.Value);
        }
    }

    public List<Trait>? Traits => _traits.ContainsKey(_langCode) ? _traits[_langCode] : null;
    private readonly Dictionary<string, List<Trait>> _traits = new();
    public void Add(string langCode, Trait trait)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if(! _traits.ContainsKey(langCode)) _traits.Add(langCode, new List<Trait>());
        _traits[langCode].Add(trait);
    }
    public void AddRange(Dictionary<string, List<Trait>> traits)
    {
        if(traits == null) return;
        foreach (var pair in traits)
        {
            if (!_traits.ContainsKey(pair.Key)) _traits.Add(pair.Key, pair.Value);
            else _traits[pair.Key].AddRange(pair.Value);
        }
    }
    
    private string _langCode = "";
    public void SetLanguage(string langCode)
    {
        _langCode = langCode;
    }
    public string GetLanguage()
    {
        return _langCode;
    }
}