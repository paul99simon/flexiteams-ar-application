namespace FlexiTeams.Data.Wrapper;

public class WorkExperience
{
    private readonly int _workExperience;

    public WorkExperience(int workExperience)
    {
        _workExperience = workExperience;
    }

    public int Get()
    {
        return _workExperience;
    }
}