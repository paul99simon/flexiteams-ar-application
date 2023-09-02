using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiTeams.DataClasses.Data
{
    public class DataEqualityComparer : EqualityComparer<Data>
    {

        public override bool Equals(Data x, Data y)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode(Data obj)
        {
            throw new NotImplementedException();
        }
    }
}
