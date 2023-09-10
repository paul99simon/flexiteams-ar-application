using FlexiTeams.DataClasses.Wrapper;
using Task = FlexiTeams.DataClasses.Task.Task;

namespace FlexiTeams.Exceptions
{
    [Serializable]
    public class NoMoreResourcesRequiredException : Exception
    {
        public NoMoreResourcesRequiredException() { }

        public NoMoreResourcesRequiredException(Task task, Profession profession) : base
            (
            "task \"" +                                                                                                 
            task.Id +
            "\" is already sufficently staffed with Resources that have the Profession \"" + 
            profession + 
            "\""
            ) { }
    }
}