namespace FlexiTeams.Data.Wrapper;

public class Photo
{
    private readonly string _path;

    public Photo(string path)
    {
        _path = path;
    }

    public string Get()
    {
        return _path;
    }
}