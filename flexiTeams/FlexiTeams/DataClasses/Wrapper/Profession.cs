namespace FlexiTeams.DataClasses.Wrapper;

public class Profession
{
    public string Get { get; }
    
    public Profession(string profession)
    {
        Get = profession;
    }

    public bool EqualsTo(Profession profession)
    {
        return profession.Get.Equals(Get);
    }

    public override string ToString()
    {
        return Get;
    }
}