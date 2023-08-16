using System.Collections;

namespace FlexiTeams.DataClasses.Resource.Wrapper;

public class ProfessionalInfos : IEnumerable<ProfessionalInfo>
{
    public List<ProfessionalInfo> List { get; } = new();
    public ProfessionalInfo this[int index] => List[index];

    public void Add(ProfessionalInfo professionalInfo)
    {
        List.Add(professionalInfo);
    }
    
    public IEnumerator<ProfessionalInfo> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}