using FlexiTeams.DataClasses.Task.Wrapper;

namespace FlexiTeams.Util.EqualityComperator
{
    public class TaskIdEqualityComparer : IEqualityComparer<TaskId>
    {
        public bool Equals(TaskId x, TaskId y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.ToString().Equals(y.ToString());
        }

        public int GetHashCode(TaskId obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
