using FlexiTeams.DataClasses.Workflow.Wrapper;

namespace FlexiTeams.Util.EqualityComperator
{
    internal class WorkflowIdEqualityComparer : IEqualityComparer<WorkflowId>
    {
        public bool Equals(WorkflowId x, WorkflowId y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.ToString().Equals(y.ToString());
        }

        public int GetHashCode(WorkflowId obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
