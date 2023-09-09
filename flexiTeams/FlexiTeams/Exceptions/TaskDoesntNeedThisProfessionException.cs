using FlexiTeams.DataClasses.Wrapper;
using Task = FlexiTeams.DataClasses.Task.Task;

namespace FlexiTeams.Exceptions
{
    public class TaskDoesntNeedThisProfessionException : Exception
    {
        public TaskDoesntNeedThisProfessionException() { }

        public TaskDoesntNeedThisProfessionException(Task task, Profession profession) : base(
            "The Task '" +
            task.Type +
            "' doesnt need the Profession'" +
            profession +
            "' for completion"
            ) { }
    }
}
