namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Trait
{
    public string Name { get; }
    public int Value { get; }

    public Trait(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public override string ToString()
    {
        return "[" + Name + ", " + Value +"]";
    }

}