using FlexiTeams.DataClasses.Data.Wrapper;


namespace FlexiTeams.Util.EqualityComperator
{
    public class DataIdEqualityComparer : IEqualityComparer<DataId>
    {
        public bool Equals(DataId x, DataId y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.ToString().Equals(y.ToString());
        }

        public int GetHashCode(DataId obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
