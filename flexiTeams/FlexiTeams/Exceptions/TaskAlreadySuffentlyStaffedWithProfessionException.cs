using FlexiTeams.DataClasses.Wrapper;
using Task = FlexiTeams.DataClasses.Task.Task;
namespace FlexiTeams.Exceptions
{
    public class TaskAlreadySuffentlyStaffedWithProfessionException : Exception
    {
        public TaskAlreadySuffentlyStaffedWithProfessionException() { }
        
        public TaskAlreadySuffentlyStaffedWithProfessionException(Task task, Profession profession) : base
            (
                "Task '" +
                task.Type + 
                "' is already sufficently staffed with the profession '" +
                profession + 
                "'"
            ){}

    }
}
