using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class AdditionalJobs : IEnumerable<AdditionalJob>
{
   public List<AdditionalJob> List { get; } = new();
   public AdditionalJob this[int index] => List[index];

   public void Add(AdditionalJob additionalJob)
   {
       List.Add(additionalJob);
   }
    
    public IEnumerator<AdditionalJob> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}