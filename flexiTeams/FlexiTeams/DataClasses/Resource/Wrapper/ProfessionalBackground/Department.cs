namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Department
{
    private string Get { get; }

    public Department(string department)
    {
        Get = department;
    }

    public override string ToString()
    {
        return Get;
    }
}