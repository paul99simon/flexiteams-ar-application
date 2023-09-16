using FlexiTeams.DataClasses.Workflow;
using FlexiTeams.DataClasses.Workflow.Wrapper;
using FlexiTeams.Util.EqualityComperator;
using System.Collections;

namespace FlexiTeams.Inventory
{
    public class WorkFlowPool : IEnumerable<Workflow>
    {
        public List<Workflow> List
        {
            get
            {
                var temp = new List<Workflow>();

                foreach (var pair in _pool)
                {
                    temp.Add(pair.Value);
                }

                return temp;
            }
        }
        public int Count => List.Count;
        private readonly Dictionary<WorkflowId, Workflow> _pool = new(new WorkflowIdEqualityComparer());
        public Workflow this[WorkflowId id] => _pool[id];

        public void Add(Workflow workflow)
        {
            if (_pool.ContainsKey(workflow.Id)) return;
            _pool.Add(workflow.Id, workflow);
        }

        public IEnumerator<Workflow> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
