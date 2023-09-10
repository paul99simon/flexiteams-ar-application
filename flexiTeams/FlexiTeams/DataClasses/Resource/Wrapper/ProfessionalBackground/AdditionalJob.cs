namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class AdditionalJob
{
    private readonly string Get;
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

    public override string ToString()
    {
        return Get;
    }
}