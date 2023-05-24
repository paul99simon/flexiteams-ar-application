using System.Text.RegularExpressions;

namespace flexiTeams.Util;

public class DayTime
{
    
    private readonly int _hours;
    private readonly int _minutes;
    private readonly int _seconds;
    
    private static readonly string _pattern_HH_MM = "^([01][0-9]|2[0-3]):([0-5][0-9])$";
    private static readonly string _pattern_HH_MM_SS = "^([01][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$";

    public DayTime(int hours, int minutes)
    {
        if (!(hours >= 0 & hours < 24)) throw new ArgumentException("hours must be between 0 and 23");
        if (!(minutes >= 0 & minutes < 60)) throw new ArgumentException("minutes must be between 0 and 59");
        _hours = hours;
        _minutes = minutes;
        _seconds = 0;
    }

    public DayTime(int hours, int minutes, int seconds)
    {
        if (!(hours >= 0 & hours < 24)) throw new ArgumentException("hours must be between 0 and 23");
        if (!(minutes >= 0 & minutes < 60)) throw new ArgumentException("minutes must be between 0 and 59");
        if (!(seconds >= 0 & minutes < 60)) throw new ArgumentException("seconds must be between 0 and 59");
        _hours = hours;
        _minutes = minutes;
        _seconds = seconds;
    }

    public DayTime(string param)
    {
        if(!(Regex.IsMatch(param, _pattern_HH_MM) | Regex.IsMatch(param, _pattern_HH_MM_SS))) throw
            new ArgumentException("param format must either be hh:mm or hh:mm:ss");
        
        string[] splits = param.Split(':');
        _hours = int.Parse(splits[0]);
        _minutes = int.Parse(splits[1]);

        _seconds = Regex.IsMatch(param, _pattern_HH_MM) ? 0 : int.Parse(splits[2]);
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
        return ((_hours == other._hours) & (_minutes == other._minutes));
    }
    
    public static bool operator <(DayTime t1, DayTime t2)
    {
        throw new NotImplementedException();
    }

    public static bool operator <=(DayTime t1, DayTime t2)
    {
        throw new NotImplementedException();
    }

    public static bool operator >(DayTime t1, DayTime t2)
    {
        throw new NotImplementedException();
    }
    
    public static bool operator >=(DayTime t1, DayTime t2)
    {
        throw new NotImplementedException();
    }
}