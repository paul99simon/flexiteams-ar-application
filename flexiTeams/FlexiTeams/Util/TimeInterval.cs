using System.Text.RegularExpressions;

namespace flexiTeams.Util;

public class TimeInterval
{
    
    private readonly DayTime _begin;
    private readonly DayTime _end;
    
    public TimeInterval(DayTime begin, DayTime end)
    {
        if (begin == null || end == null) throw new ArgumentNullException();
        CheckIfEqual(begin, end);
        _begin = begin;
        _end = end;
    }

    public TimeInterval(string param)
    { 
        const string pattern1 = "^\\[[0-9]{2}:[0-9]{2}, [0-9]{2}:[0-9]{2}\\]$";
        const string pattern2 = "^\\[[0-9]{2}:[0-9]{2}, [0-9]{2}:[0-9]{2}:[0-9]{2}\\]$";
        const string pattern3 = "^\\[[0-9]{2}:[0-9]{2}:[0-9]{2}, [0-9]{2}:[0-9]{2}\\]$";
        const string pattern4 = "^\\[[0-9]{2}:[0-9]{2}:[0-9]{2}, [0-9]{2}:[0-9]{2}:[0-9]{2}\\]$";

        string[] splits;

        if (Regex.IsMatch(param, pattern1) | Regex.IsMatch(param, pattern2) | Regex.IsMatch(param, pattern3) | Regex.IsMatch(param, pattern4))
        {
            param = param.Substring(1, param.Length - 2);
            splits = param.Split(',');
            for (int i = 0; i < splits.Length; i++)
            {
                splits[i] = splits[i].Trim();
            }
        }
        else throw new ArgumentException("param format must either be [hh:mm, hh:mm] or [hh:mm:ss, hh:mm:ss] or [hh:mm, hh:mm:ss] or [hh:mm:ss, hh:mm]");
        
        DayTime begin = new DayTime(splits[0]);
        DayTime end = new DayTime(splits[1]);
        
        CheckIfEqual(begin, end);

        _begin = begin;
        _end = end;
    }

    private static void CheckIfEqual(DayTime t1, DayTime t2)
    {
        if (t1.Equals(t2)) throw new ArgumentException("begin time must differ from end time");
    }
    
    public override string ToString()
    {
        return "[" + _begin + ", " + _end + "]";
    }

    public bool Contains(TimeInterval other)
    {
        if (_begin < _end)
        {
            if(other._begin < other ._end) return _begin <= other._begin & other._end <= _end;
            return false;
        }
        if (other._begin < other._end) return other._end <= _end;
        return _begin <= other._begin & other._end <= _end;
    }

    public DayTime GetLength()
    {
        throw new NotImplementedException();
    }
}