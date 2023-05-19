using System.Dynamic;

namespace FlexiTeams.Data.Wrapper;

public class ArrivalTime
{
    private readonly int _arrivalTime;

    public ArrivalTime(int arrivalTime)
    {
        _arrivalTime = arrivalTime;
    }

    public int Get()
    {
        return _arrivalTime;
    }
}