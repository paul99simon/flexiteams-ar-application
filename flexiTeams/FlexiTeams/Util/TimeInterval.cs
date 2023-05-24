using System.ComponentModel;
using System.Text.RegularExpressions;

namespace flexiTeams.Util;

public class TimeInterval
{
    private readonly DayTime _begin;
    private readonly DayTime _end;


    public TimeInterval(DayTime begin, DayTime end)
    {
        _begin = begin;
        _end = end;
    }

    public TimeInterval(string param)
    {
        throw new NotImplementedException();
    }
    
    public override string ToString()
    {
        return "[" + _begin + ", " + _end + "]";
    }

    public bool Contains(TimeInterval ti)
    {
        throw new NotImplementedException();
    }
}