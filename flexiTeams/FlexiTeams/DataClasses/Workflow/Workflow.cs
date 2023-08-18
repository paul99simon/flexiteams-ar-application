using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.DataClasses.Workflow;

public class Workflow : ILanguageObject
{
    public WorkflowId Id { get; set; }
    
    public WorkflowType? Type => _types.ContainsKey(_langCode) ? _types[_langCode] : null;
    private readonly Dictionary<string, WorkflowType> _types = new();
    public void Add(string langCode, WorkflowType type)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        _types[langCode] = type;
    }
    public void AddRange(Dictionary<string, WorkflowType> types)
    {
        foreach (var pair in types)
        {
            if(! _types.ContainsKey(pair.Key)) _types.Add(pair.Key, pair.Value);
            _types[pair.Key] = pair.Value;
        }
    }
    
    public Duration? Duration { get; set; }

    public Venue? Venue => _venues.ContainsKey(_langCode) ? _venues[_langCode] : null;
    private readonly Dictionary<string, Venue> _venues = new();
    public void Add(string langCode, Venue venue)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        _venues[langCode] = venue;
    }
    public void AddRange(Dictionary<string, Venue> venues)
    {
        foreach (var pair in venues)
        {
            if(! _venues.ContainsKey(pair.Key)) _venues.Add(pair.Key, pair.Value);
            _venues[pair.Key] = pair.Value;
        }
    }
    
    public Procedures? Procedures { get; set; }

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