namespace FlexiTeams.DataClasses.Wrapper;

public class Profession
{
    public string Language { get; }
    public string Get { get; }
    
    public Profession(string language, string profession)
    {
        Language = language;
        Get = profession;
    }

    public bool EqualsTo(Profession profession)
    {
        return profession.Get.Equals(Get) & profession.Language.Equals(Language);
    }
}