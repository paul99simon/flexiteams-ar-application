namespace FlexiTeams.Data.Wrapper;

public class AdditionalJob
{
    public string Language { get; }
    public string Get { get; }
    public int? YearlyRequiredDays { get; }

    public AdditionalJob(string language, string additionalJob, int yearlyRequiredDays)
    {
        Language = language;
        Get = additionalJob;
        YearlyRequiredDays = yearlyRequiredDays;
    }
    
    public AdditionalJob(string language, string additionalJob)
    {
        Language = language;
        Get = additionalJob;
        YearlyRequiredDays = null;
    }
}