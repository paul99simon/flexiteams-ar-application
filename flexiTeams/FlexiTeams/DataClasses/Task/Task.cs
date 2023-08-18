using FlexiTeams.DataClasses.Task.Wrappper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.DataClasses.Task;

public class Task : ILanguageObject
{
    public TaskId Id { get; set; }

    public TaskType? Type => _types.ContainsKey(_langcode) ? _types[_langcode] : null;
    private readonly Dictionary<string, TaskType> _types = new();
    public void Add(string langCode, TaskType type)
    {
        if (!ISO_639_1.IsValidCode(langCode)) return;
        if(! _types.ContainsKey(langCode)) _types.Add(langCode, type);
    }
    public void AddRange(Dictionary<string, TaskType> types)
    {
        foreach (var pair in types)
        {
            if(! _types.ContainsKey(pair.Key)) _types.Add(pair.Key, pair.Value);
            _types[pair.Key] = pair.Value;
        }
    }

    public Venue? Venue => _venues.ContainsKey(_langcode) ? _venues[_langcode] : null;
    private readonly Dictionary<string, Venue> _venues = new();
    public void Add(string langCode, Venue venue)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if(! _venues.ContainsKey(langCode)) _venues.Add(langCode, venue);
    }
    public void AddRange(Dictionary<string, Venue> venues)
    {
        foreach (var pair in venues)
        {
            if(! _venues.ContainsKey(pair.Key)) _venues.Add(pair.Key, pair.Value);
            _venues[pair.Key] = pair.Value;
        }
    }
    
    public Priority Priority { get; set; }
    public Duration Duration { get; set; }

    public List<Profession>? RequiredProfessions => _requiredProfessions.ContainsKey(_langcode) ? _requiredProfessions[_langcode] : null;
    private readonly Dictionary<string,List<Profession>> _requiredProfessions = new();
    public void Add(string langCode, Profession profession)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if (!_requiredProfessions.ContainsKey(langCode))
        {
            _requiredProfessions.Add(langCode, new List<Profession>());
        }
        _requiredProfessions[langCode].Add(profession);
    }
    public void AddRange(Dictionary<string, List<Profession>> requiredProfessions)
    {
        foreach (var pair in requiredProfessions)
        {
            if(! _requiredProfessions.ContainsKey(pair.Key)) _requiredProfessions.Add(pair.Key, new List<Profession>());
            _requiredProfessions[pair.Key].AddRange(pair.Value);
        }
    }
    
    private string _langcode = "";
    public void SetLanguage(string langCode)
    {
        if (!ISO_639_1.IsValidCode(langCode)) return; 
        _langcode = langCode;
    }
    public string GetLanguage()
    {
        return _langcode;
    }
}