using FlexiTeams.DataClasses.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexiTeams.Util.EqualityComperator
{
    public class ProfessionEqualityComperator : IEqualityComparer<Profession>
    {
        public bool Equals(Profession x, Profession y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return y.ToString().Equals(x.ToString());
        }

        public int GetHashCode(Profession profession)
        {
            return profession.ToString().GetHashCode();
        }
    }
}
