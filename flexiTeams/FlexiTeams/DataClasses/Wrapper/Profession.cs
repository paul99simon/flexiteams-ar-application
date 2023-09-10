namespace FlexiTeams.DataClasses.Wrapper;

public class Profession
{
    private readonly string Get;
    
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