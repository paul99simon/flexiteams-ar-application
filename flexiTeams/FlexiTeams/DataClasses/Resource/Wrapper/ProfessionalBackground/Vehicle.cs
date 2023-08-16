namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Vehicle
{
    public string Language { get; }
    public string Get { get; }

    public Vehicle(string language, string vehicle)
    {
        Language = language;
        Get = vehicle;
    }
}