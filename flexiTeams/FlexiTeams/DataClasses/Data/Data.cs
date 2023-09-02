using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.DataClasses.Data;

public class Data : ILanguageObject
{
    public DataId Id { get; set; }
    
    public DataName Name => _names[_langCode];
    private readonly Dictionary<string, DataName> _names = new ();
    public void Add(string langCode, DataName name)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if(_names.ContainsKey(langCode)) return;
        _names.Add(langCode, name);
    }
    public void AddRange(Dictionary<string, DataName> names)
    {
        foreach (var pair in names)
        {
            if(! _names.ContainsKey(pair.Key)) _names.Add(pair.Key, pair.Value);
            _names[pair.Key] = pair.Value;
        }
    }
    
    private string _langCode = "";
    public void SetLanguage(string langCode)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        _langCode = langCode;
    }
    public string GetLanguage()
    {
        return _langCode;
    }
}