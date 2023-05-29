
namespace FlexiTeams.Data.Wrapper;

public class Skill
{
    private readonly string _lang;
    private readonly string _skill;

    public Skill(string lang, string skill)
    {
        _lang = lang;
        _skill = skill;
    }

    public string Get()
    {
        return _skill;
    }

    public string Language()
    {
        return _lang;
    }
}