using flexiTeams.Util;

namespace FlexiTeams.Data.Wrapper;

public class WorkAgreement
{
    private readonly List<TimeInterval>[] _schedule = new List<TimeInterval>[7];

    public WorkAgreement(List<TimeInterval>[] schedule)
    {
        if (schedule.Length != 7) throw new ArgumentException();
        for (int i = 0; i <= 6; i++)
        {
            _schedule[i] = schedule[i];
        }
    }
    
    public bool IsAgreedTime(int weekDay, TimeInterval ti)
    {
       foreach (TimeInterval interval in _schedule[weekDay])
       {
            if (interval.Contains(ti)) return true;
       }
       
       return false;
    }
}