using FlexiTeams.DataClasses.Wrapper;
using FlexiTeams.Util.EqualityComperator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiTeamsTests.Util.EqualityComperator
{
    [TestFixture]
    public class EqualityComperatorTests
    {
        [Test]
        public void ProfessionEqualityComperatorTest()
        {
            Profession profession1 = new("nurse");
            Profession profession2 = new("nurse");
            Profession profession3 = new("doctor");

            List<Profession> list = new()
            {
                profession1
            };

            ProfessionEqualityComperator comperator = new();

            Assert.IsFalse(list.Contains(profession3, comperator));
            Assert.IsTrue(list.Contains(profession2, comperator));

            Dictionary<Profession, int> dict = new(comperator);

            dict.Add(profession1, 1);

            Assert.AreEqual(dict[profession1], 1);
            Assert.AreEqual(dict[profession2], 1);
            Assert.Throws<KeyNotFoundException>(() => _ = dict[profession3]);

        }
    }
}
