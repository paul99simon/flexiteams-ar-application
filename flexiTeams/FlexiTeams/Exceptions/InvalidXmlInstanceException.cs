using System;
using System.Collections.Generic;
using System.Text;

namespace FlexiTeams.Exceptions
{
    [Serializable]
    public class InvalidXmlInstanceException : Exception
    {
        public InvalidXmlInstanceException(string message) : base (message) { }
    }
}
