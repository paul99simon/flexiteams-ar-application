using System.Diagnostics;

namespace FlexiTeams.DataClasses.Wrapper;

public class Duration
{
    public int Get { get; }

    public Duration(int minutes)
    {
        Get = minutes;
    }

    public static Duration operator +(Duration a, Duration b)
    {
        if (a is null && b is null) { return new Duration(0); }
        if (a is null) return b;
        if (b is null) return a;

        return new Duration(a.Get + b.Get);
    }
    public static Duration operator -(Duration a, Duration b)
    {
        if (a is null && b is null) { return new Duration(0); }
        if (a is null) return new Duration(-1 * b.Get);
        if (b is null) return a;

        return new Duration(a.Get - b.Get);
    }
}