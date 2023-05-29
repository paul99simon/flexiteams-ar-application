using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class AdditionalJobs : IEnumerable<AdditionalJob>
{
    private readonly List<AdditionalJob> _additionalJobs;

    public AdditionalJobs(List<AdditionalJob> additionalJobs)
    {
        _additionalJobs = additionalJobs;
    }
    
    public IEnumerator<AdditionalJob> GetEnumerator()
    {
        return _additionalJobs.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}