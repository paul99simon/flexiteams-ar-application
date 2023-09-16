using FlexiTeams.DataClasses.Resource;
using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util.EqualityComperator;
using System.Collections;
using Task = FlexiTeams.DataClasses.Task.Task;
using FlexiTeams.DataClasses.Task.Wrappper;

namespace FlexiTeams.Inventory
{
    public class TaskPool : IEnumerable<Task>
    {
        public List<Task> List
        {
            get
            {
                var temp = new List<Task>();

                foreach (var pair in _pool)
                {
                    temp.Add(pair.Value);
                }

                return temp;
            }
        }
        public int Count => List.Count;
        private readonly Dictionary<TaskId, Task> _pool = new(new TaskIdEqualityComparer());
        public Task this[TaskId id] => _pool[id];

        public void Add(Task task)
        {
            if (_pool.ContainsKey(task.Id)) return;
            _pool.Add(task.Id, task);
        }

        public IEnumerator<Task> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
