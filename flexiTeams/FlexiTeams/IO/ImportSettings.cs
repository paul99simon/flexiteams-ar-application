using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Builder.Interface;
using FlexiTeams.ConstructionClasses.Director.Basic;
using FlexiTeams.ConstructionClasses.Director.Interface;

namespace FlexiTeams.IO
{
    public class ImportSettings
    {
        //Builder
        public IResourceBuilder ResourceBuilder = new BasicResourceBuilder();
        public IDataBuilder DataBuilder = new BasicDataBuilder();
        public IWorkflowBuilder WorkflowBuilder = new BasicWorkflowBuilder();
        public ITaskBuilder TaskBuilder = new BasicTaskBuilder();

        //Director
        public IResourceDirector ResourceDirector = new BasicResourceDirector();
        public IDataDirector DataDirector = new BasicDataDirector();
        public IWorkflowDirector WorkflowDirector = new BasicWorkflowDirector();
        public ITaskDirector TaskDirector = new BasicTaskDirector();
    }
}
