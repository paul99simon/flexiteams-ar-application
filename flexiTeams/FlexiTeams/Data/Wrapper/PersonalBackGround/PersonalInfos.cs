using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class PersonalInfos : IEnumerable<PersonalInfo>
{
    public List<PersonalInfo> List { get; } = new();
    public PersonalInfo this[int index] => List[index];

    public void Add(PersonalInfo personalInfo)
    {
        List.Add(personalInfo);
    }

    public IEnumerator<PersonalInfo> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}