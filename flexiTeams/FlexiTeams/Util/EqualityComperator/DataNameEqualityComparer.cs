using FlexiTeams.DataClasses.Data.Wrapper;

namespace FlexiTeams.Util.EqualityComperator
{
    public class DataNameEqualityComparer : EqualityComparer<DataName>
    {

        public override bool Equals(DataName x, DataName y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.ToString().Equals(y.ToString());
        }

        public override int GetHashCode(DataName obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
