namespace FlexiTeams.Data.Wrapper;

public class Overtime
{
    private readonly int _overtime;

    public Overtime(int overtime)
    {
        _overtime = overtime;
    }

    public int Get()
    {
        return _overtime;
    }
}