using System.Text.RegularExpressions;

namespace FlexiTeams.Util;

public class DayTime
{
    
    private readonly int _hours;
    private readonly int _minutes;
    private readonly int _seconds;
    
    private static readonly string _pattern_HH_MM = "^[0-9]{2}:[0-9]{2}$";
    private static readonly string _pattern_HH_MM_SS = "^[0-9]{2}:[0-9]{2}:[0-9]{2}$";

    public DayTime(int hours, int minutes) : this(hours, minutes, 0)
    {
    }

    public DayTime(int hours, int minutes, int seconds)
    {
        CheckIfTime(hours, minutes, seconds);
        _hours = hours;
        _minutes = minutes;
        _seconds = seconds;
    }

    public DayTime(string param)
    {
        if(!(Regex.IsMatch(param, _pattern_HH_MM) | Regex.IsMatch(param, _pattern_HH_MM_SS))) throw
            new ArgumentException("param format must either be hh:mm or hh:mm:ss");
        
        string[] splits = param.Split(':');
        int hours = int.Parse(splits[0]);
        int minutes = int.Parse(splits[1]);
        int seconds = Regex.IsMatch(param, _pattern_HH_MM) ? 0 : int.Parse(splits[2]);
        
        CheckIfTime(hours, minutes, seconds);
        _hours = hours;
        _minutes = minutes;
        _seconds = seconds;
    }

    private static void CheckIfTime(int hours, int minutes, int seconds)
    {
        if (!(hours >= 0 & hours < 24)) throw new ArgumentException("hours must be between 0 and 23");
        if (!(minutes >= 0 & minutes < 60)) throw new ArgumentException("minutes must be between 0 and 59");
        if (!(seconds >= 0 & seconds < 60)) throw new ArgumentException("seconds must be between 0 and 59");
    }

    public override string ToString()
    {
        string result = "";

        if (_hours < 10) result = result + "0" + _hours;
        else result += _hours;

        result += ":";

        if (_minutes < 10) result = result + "0" + _minutes;
        else result += _minutes;

        if (_seconds == 0) return result;

        result += ":";

        if (_seconds < 10) result = result + "0" + _seconds;
        else result += _seconds;

        return result;
    }

    public bool Equals(DayTime other)
    {
        return (_hours == other._hours) & (_minutes == other._minutes) & (_seconds == other._seconds);
    }
    
    public static bool operator <(DayTime t1, DayTime t2)
    {
        if (t1._hours < t2._hours) return true;
        if (t1._hours > t2._hours) return false;
        if (t1._minutes < t2._minutes) return true;
        if (t1._minutes > t2._minutes) return false;
        if (t1._seconds < t2._seconds) return true;        
        if (t1._seconds > t2._seconds) return false;
        return false;
    }

    public static bool operator <=(DayTime t1, DayTime t2)
    {
        return t1 < t2 | t1.Equals(t2);
    }

    public static bool operator >(DayTime t1, DayTime t2)
    {
        if (t1._hours > t2._hours) return true;
        if (t1._hours < t2._hours) return false;
        if (t1._minutes > t2._minutes) return true;
        if (t1._minutes < t2._minutes) return false;
        if (t1._seconds > t2._seconds) return true;        
        if (t1._seconds < t2._seconds) return false;
        return false;
    }
    
    public static bool operator >=(DayTime t1, DayTime t2)
    {
        return t1 > t2 | t1.Equals(t2);
    }

    public static DayTime operator +(DayTime t1, DayTime t2)
    {

        int hours = 0;
        int minutes = 0;
        int seconds;
        
        seconds = t1._seconds + t2._seconds;

        if (seconds / 60 != 0)
        {
            minutes++;
            seconds %= 60;
        }

        minutes += (t1._minutes + t2._minutes);
        
        if (minutes / 60 != 0)
        {
            hours++;
            minutes %= 60;
        }

        hours += (t1._hours + t2._hours);
        hours %= 24;

        return new DayTime(hours, minutes, seconds);
    }
    
    public static DayTime operator -(DayTime t1, DayTime t2)
    {
        int hours = 0;
        int minutes = 0;
        int seconds = 0;

        seconds = t1._seconds - t2._seconds;
        
        if (seconds < 0)
        {
            minutes++;
            seconds = 60 + seconds;
        }

        minutes = t1._minutes - t2._minutes - minutes;

        if (minutes < 0)
        {
            hours++;
            minutes = 60 + minutes;
        }

        
        hours = t1._hours - t2._hours - hours;
        if (hours < 0)
        {
            hours = 24 + hours;
        }
        
        return new DayTime(hours, minutes, seconds);
    }
}