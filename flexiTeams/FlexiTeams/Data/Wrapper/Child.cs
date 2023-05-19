using System.Xml;

namespace flexiTeams.Data;

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