using FlexiTeams.DataClasses.Wrapper;

namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class CommuteTime
{
    public int Minutes { get; }

    public CommuteTime(int minutes)
    {
        Minutes = minutes;
    }
}