namespace FlexiTeams.Data.Wrapper;

public class Age
{
    private readonly int _age;

    public Age(int age)
    {
        _age = age;
    }

    public int Get()
    {
        return _age;
    }
}