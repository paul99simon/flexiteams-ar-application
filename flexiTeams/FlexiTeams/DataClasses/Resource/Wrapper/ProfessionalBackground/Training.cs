namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Training
{
    private readonly string Get;

    public Training(string training)
    {
        Get = training;
    }

    public override string ToString()
    {
        return Get;
    }
}