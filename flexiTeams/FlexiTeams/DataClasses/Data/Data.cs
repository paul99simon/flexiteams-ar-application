using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.Util;

namespace FlexiTeams.DataClasses.Data;

public class Data : ILanguageObject
{
    private string _langCode = "";

    public string Id
    {
        get => _dataId.Get;
    }

    private DataId _dataId = null;
    
    public string Name => _names[_langCode];
    private readonly Dictionary<string, string> _names = new ();
    
    public void SetLanguage(string langCode)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        _langCode = langCode;
    }
    
    public void AddName(string langCode, string name)
    {
        if(! ISO_639_1.IsValidCode(langCode)) return;
        if(_names.ContainsKey(langCode)) return;
        _names.Add(langCode, name);
    }
}