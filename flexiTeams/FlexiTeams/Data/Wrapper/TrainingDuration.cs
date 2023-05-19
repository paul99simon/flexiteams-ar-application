namespace FlexiTeams.Data.Wrapper;

public class TrainingDuration
{
    private readonly int _trainingDuration;

    public TrainingDuration(int trainingDuration)
    {
        _trainingDuration = trainingDuration;
    }

    public int Get()
    {
        return _trainingDuration;
    }
}