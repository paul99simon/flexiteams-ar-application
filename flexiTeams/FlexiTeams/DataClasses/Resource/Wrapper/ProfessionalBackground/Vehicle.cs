namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Vehicle
{
    private readonly string Get;

    public Vehicle(string vehicle)
    {
        Get = vehicle;
    }

    public override string ToString()
    {
        return Get;
    }
}