namespace FlexiTeams.Data.Wrapper;

public class AdditionalJob
{
    private readonly string _lang;
    private readonly string _additionalJob;
    private readonly int? _yearlyRequiredDays;

    public AdditionalJob(string lang, string additionalJob, int yearlyRequiredDays)
    {
        _lang = lang;
        _additionalJob = additionalJob;
        _yearlyRequiredDays = yearlyRequiredDays;
    }
    
    public AdditionalJob(string lang, string additionalJob)
    {
        _lang = lang;
        _additionalJob = additionalJob;
        _yearlyRequiredDays = null;
    }

    public string Get()
    {
        return _additionalJob;
    }


    public string Language()
    {
        return _lang;
    }

    public int? YearlyRequiredDays()
    {
        return _yearlyRequiredDays;
    }
}