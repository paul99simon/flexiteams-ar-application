using System.ComponentModel;
using System.Text.RegularExpressions;

namespace flexiTeams.Util;

public class TimeInterval
{
    private readonly string _begin;
    private readonly string _end;

    private static readonly string _timePattern = "^([01][0-9]|2[0-3]):([0-5][0-9])$";
    private static readonly string _timePatternSplit = "([01][0-9]|2[0-3]):([0-5][0-9])";
    private static readonly string _intervalPattern = "^\\[([01][0-9]|2[0-3]):([0-5][0-9]), ([01][0-9]|2[0-3]):([0-5][0-9])\\]$";

    public TimeInterval(string begin, string end)
    {
        if (!Regex.IsMatch(begin, _timePattern)) throw new ArgumentException("\"" + begin + "\" doesnt match \"HH:MM\" format");
        if (!Regex.IsMatch(end, _timePattern)) throw new ArgumentException("\"" + end + "\" doesnt match \"HH:MM\" format");
        
        _begin = begin;
        _end = end;
    }

    public TimeInterval(string interval)
    {
        if (!Regex.IsMatch(interval, _intervalPattern)) throw new InvalidEnumArgumentException("\"" + interval + "\" doesnt match [HH:MM, HH:MM] format");
        var matches = Regex.Matches(interval, _timePatternSplit);

        _begin = matches[0].ToString();
        _end = matches[1].ToString();
    }
    
    public override string ToString()
    {
        return "[" + _begin + ", " + _end + "]";
    }
}