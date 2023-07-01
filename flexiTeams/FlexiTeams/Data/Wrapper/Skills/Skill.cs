
namespace FlexiTeams.Data.Wrapper;

public class Skill
{
    public string Language { get; }
    public string Get { get; }

    public Skill(string language, string skill)
    {
        Language = language;
        Get = skill;
    }
}