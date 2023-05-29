namespace FlexiTeams.Data.Wrapper;

public class YearlyTimeOf
{
    private readonly int _yearlyTimeOf;

    public YearlyTimeOf(int yearlyTimeOf)
    {
        _yearlyTimeOf = yearlyTimeOf;
    }

    public int Get()
    {
        return _yearlyTimeOf;
    }
}