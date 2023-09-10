using FlexiTeams.DataClasses.Data;

namespace FlexiTeams.Util.EqualityComperator
{
    public class DataEqualityComparer : EqualityComparer<Data>
    {

        public override bool Equals(Data x, Data y)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode(Data obj)
        {
            throw new NotImplementedException();
        }
    }
}
