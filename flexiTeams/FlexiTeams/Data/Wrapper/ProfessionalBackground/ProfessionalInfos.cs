using System.Collections;

namespace FlexiTeams.Data.Wrapper;

public class ProfessionalInfos : IEnumerable<ProfessionalInfo>
{
    private readonly List<ProfessionalInfo> _professionalInfos;

    public ProfessionalInfos(List<ProfessionalInfo> professionalInfos)
    {
        _professionalInfos = professionalInfos;
    }

    public IEnumerator<ProfessionalInfo> GetEnumerator()
    {
        return _professionalInfos.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}