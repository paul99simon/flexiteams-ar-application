using FlexiTeams.IO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiTeamsTests.IO
{
    [TestFixture]
    public class ValidationTest
    {
        const string path = "C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/importTest.xml";

        [Test]
        public void ValidateTest()
        {
            Validation.Validate(path);
        }
    }
}