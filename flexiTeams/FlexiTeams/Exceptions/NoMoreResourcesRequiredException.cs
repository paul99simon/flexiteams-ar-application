using FlexiTeams.DataClasses.Wrapper;
using Task = FlexiTeams.DataClasses.Task.Task;

namespace FlexiTeams.Exceptions
{
    public class NoMoreResourcesRequiredException : Exception
    {

        public NoMoreResourcesRequiredException() { }

        public NoMoreResourcesRequiredException(Task task, Profession profession) : base(    "task \"" +
                                                                                                                task.Id.Get +
                                                                                                                "\" is already sufficently staffed with Resources that have the Profession \"" + 
                                                                                                                profession.Get + 
                                                                                                                "\"") { }
    }
}