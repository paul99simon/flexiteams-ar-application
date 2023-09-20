using FlexiTeams.DataClasses.Resource.Wrapper;

namespace FlexiTeams.Util.EqualityComperator
{
    public class ResourceIdEqualityComparer : IEqualityComparer<ResourceId>
    {
        public bool Equals(ResourceId x, ResourceId y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.ToString().Equals(y.ToString());
        }

        public int GetHashCode(ResourceId obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
