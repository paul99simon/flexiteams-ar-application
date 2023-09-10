
namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Skill
{
    private readonly string Get;

    public Skill(string skill)
    {
        Get = skill;
    }

    public override string ToString()
    {
        return Get;
    }
}