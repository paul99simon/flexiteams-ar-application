using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class PersonalInfos : IEnumerable<PersonalInfo>
{
    private readonly List<PersonalInfo> _personalInfos;

    public PersonalInfos(List<PersonalInfo> personalInfos)
    {
        _personalInfos = personalInfos;
    }

    public IEnumerator<PersonalInfo> GetEnumerator()
    {
        return _personalInfos.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}