namespace FlexiTeams.Data.Wrapper;

public class YearlyEducation
{
    private readonly int _yearlyEducation;

    public YearlyEducation(int yearlyEducation)
    {
        _yearlyEducation = yearlyEducation;
    }

    public int Get()
    {
        return _yearlyEducation;
    }
}