namespace FlexiTeams.Data.Wrapper;

public class WeeklyHours
{
    private readonly int _weeklyHours;

    public WeeklyHours(int weeklyHours)
    {
        _weeklyHours = weeklyHours;
    }

    public int Get()
    {
        return _weeklyHours;
    }
}