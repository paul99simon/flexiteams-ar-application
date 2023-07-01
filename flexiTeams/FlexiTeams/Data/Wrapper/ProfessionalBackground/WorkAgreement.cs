using System.Collections;
using flexiTeams.Util;

namespace FlexiTeams.Data.Wrapper;

public class WorkAgreement : IEnumerable<List<TimeInterval>>
{
    public List<TimeInterval>[] List { get; } = new List<TimeInterval>[7];
    public List<TimeInterval> this[int index] => List[index];

    public WorkAgreement()
    {
        for (int i = 0; i < List.Length; i++)
        {
            List[i] = new List<TimeInterval>();
        }
    }

    public void Add(int index, TimeInterval timeInterval)
    {
        List[index].Add(timeInterval);
    }
    
    public IEnumerator<List<TimeInterval>> GetEnumerator()
    {
        return List.ToList().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public bool IsAgreedTime(int weekDay, TimeInterval ti)
    {
        foreach (TimeInterval interval in List[weekDay])
        {
            if (interval.Contains(ti)) return true;
        }
       
        return false;
    }

    public override string ToString()
    {
        var result = "";

        result += "monday: " + String.Join(", ", List[0]) + "\n";
        result += "tuesday: " + String.Join(", ", List[1]) + "\n";
        result += "wednesday: " + String.Join(", ", List[2]) + "\n";
        result += "thursday: " + String.Join(", ", List[3]) + "\n";
        result += "friday: " + String.Join(", ", List[4]) + "\n";
        result += "saturday: " + String.Join(", ", List[5]) + "\n";
        result += "sunday: " + String.Join(", ", List[6]);
        
        return result;
    }
}