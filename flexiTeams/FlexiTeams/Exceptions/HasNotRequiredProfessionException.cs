using FlexiTeams.DataClasses.Resource;
using FlexiTeams.DataClasses.Wrapper;
using Task = FlexiTeams.DataClasses.Task.Task;

namespace FlexiTeams.Exceptions
{
    public class HasNotRequiredProfessionException : Exception
    {

        public HasNotRequiredProfessionException() { }

        public HasNotRequiredProfessionException(Resource resource, Task task, Profession profession) : base("\"" + resource.Id.Get + "\" cannot execute Task \"" + task.Id.Get + "\" as \"" + profession.Get + "\"") {
        
        }
    }
}
