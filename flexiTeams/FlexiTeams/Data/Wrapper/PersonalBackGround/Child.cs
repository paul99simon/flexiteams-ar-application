namespace FlexiTeams.Data.Wrapper;

public class Child
{
    private readonly int _age;

    public Child(int age)
    {
        _age = age;
    }

    public int Get()
    {
        return _age;
    }
}