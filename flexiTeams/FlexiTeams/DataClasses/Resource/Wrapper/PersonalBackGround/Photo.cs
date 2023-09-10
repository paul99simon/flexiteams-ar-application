namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class Photo
{
    private readonly string Path;

    public Photo(string path)
    {
        Path = path;
    }

    public override string ToString()
    {
        return Path;
    }
}