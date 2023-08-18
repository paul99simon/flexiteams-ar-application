namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class AdditionalJob
{
    public string Get { get; }
    public int? YearlyRequiredDays { get; }

    public AdditionalJob( string additionalJob, int yearlyRequiredDays)
    {
        Get = additionalJob;
        YearlyRequiredDays = yearlyRequiredDays;
    }
    
    public AdditionalJob(string additionalJob)
    {
        Get = additionalJob;
        YearlyRequiredDays = null;
    }
}