using FlexiTeams.DataClasses.Wrapper;


namespace FlexiTeams.Util.EqualityComperator
{
    public class IdEqualityComperator : IEqualityComparer<Id>
    {
        public bool Equals(Id x, Id y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.ToString().Equals(y.ToString());
        }

        public int GetHashCode(Id obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
