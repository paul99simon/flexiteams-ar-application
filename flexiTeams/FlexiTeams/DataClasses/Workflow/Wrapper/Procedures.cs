using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.DataClasses.Workflow.Wrapper;

public class Procedures
{
    public int Get { get; }

    public Procedures(int count)
    {
        Get = count;
    }

    public static Procedures operator +(Procedures a, Procedures b)
    {
        if (a is null && b is null) { return new Procedures(0); }
        if (a is null) return b;
        if (b is null) return a;

        return new Procedures(a.Get + b.Get);
    }
    public static Procedures operator -(Procedures a, Procedures b)
    {
        if (a is null && b is null) { return new Procedures(0); }
        if (a is null) return new Procedures(-1 * b.Get);
        if (b is null) return a;

        return new Procedures(a.Get - b.Get);
    }
    public static Procedures operator ++(Procedures a)
    {
        if (a is null) return null;
        return new Procedures(a.Get + 1);
    }
    public static Procedures operator --(Procedures a)
    {
        if (a is null) return null;
        return new Procedures(a.Get - 1);
    }
}