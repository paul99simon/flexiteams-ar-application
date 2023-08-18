using System.Text.RegularExpressions;

namespace FlexiTeams.Util;

public class TimeInterval
{
    
    private readonly DayTime _begin;
    private readonly DayTime _end;
    
    public TimeInterval(DayTime begin, DayTime end)
    {
        if (begin == null || end == null) throw new ArgumentNullException();
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
        
        _begin = begin;
        _end = end;
    }
    
    public override string ToString()
    {
        return "[" + _begin + ", " + _end + "]";
    }

    public bool Contains(TimeInterval other)
    {
        if (_begin <= _end)
        {
            if(other._begin <= other._end) return _begin <= other._begin & other._end <= _end;
            return false;
        }
        
        var begin = new DayTime(0, 0);
        var end = new DayTime(23, 59, 59);
            
        var temp1 = new TimeInterval(_begin, end);
        var temp2 = new TimeInterval(begin, _end);
            
        if (other._begin <= other._end) return temp1.Contains(other) | temp2.Contains(other);
            
        var temp3 = new TimeInterval(other._begin, end);
        var temp4 = new TimeInterval(begin, other._end);
        return temp1.Contains(temp3) & temp2.Contains(temp4);
    }

    public bool Intersects(TimeInterval other)
    {
        if (other._begin < _begin & other._end > _end) return other.Intersects(this);
        
        var begin = new TimeInterval(other._begin, other._begin);
        var end = new TimeInterval(other._end, other._end);

        return (Contains(begin) | Contains(end));

    }

    public DayTime GetLength()
    { 
        return _end - _begin;
    }

    public bool Equals(TimeInterval other)
    {
        return other._begin.Equals(_begin) & other._end.Equals(_end);
    }
}