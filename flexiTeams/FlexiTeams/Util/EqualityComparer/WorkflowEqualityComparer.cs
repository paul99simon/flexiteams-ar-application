using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiTeams.DataClasses.Workflow;

namespace FlexiTeams.Util.EqualityComperator
{
    public class WorkflowEqualityComparer : EqualityComparer<Workflow>
    {
        public override bool Equals(Workflow x, Workflow y)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode(Workflow obj)
        {
            throw new NotImplementedException();
        }
    }
}
