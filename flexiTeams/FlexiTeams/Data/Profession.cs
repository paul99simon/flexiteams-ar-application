namespace flexiTeams.Data.Wrapper;

public class Profession
{
    private string _language { get; }
    private string _name { get; }

    public Profession(string language, string name)
    {
        _language = language;
        _name = name;
    }
}