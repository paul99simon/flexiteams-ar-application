using FlexiTeams.DataClasses.Data.Wrapper;

namespace FlexiTeams.DataClasses.Data;

public class Data
{
    public DataId Id { get; set; } = new("");

    public DataName Name { get; set; } = new("");
}