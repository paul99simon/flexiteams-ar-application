namespace FlexiTeams.Data.Wrapper;

public class Training
{
    public string Language { get; }
    public string Get { get; }

    public Training(string language, string training)
    {
        Language = language;
        Get = training;
    }
}