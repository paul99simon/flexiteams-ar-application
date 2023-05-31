namespace FlexiTeams.Data.Wrapper;

public class Profession
{
    public string Language { get; }
    public string Get { get; }
    
    public Profession(string language, string profession)
    {
        Language = language;
        Get = profession;
    }
}