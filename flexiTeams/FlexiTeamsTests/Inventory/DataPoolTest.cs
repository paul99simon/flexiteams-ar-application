using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiTeams.Inventory;

namespace FlexiTeamsTests.Inventory
{
    [TestFixture]
    public class DataPoolTest
    {
        [Test]
        public void GetDataPool()
        {
            String path = "../../../../dataPools/20DataPool.xml";

            BasicDataBuilder builder = new BasicDataBuilder();
            DataPool dp = new DataPool(builder, path);

            Assert.AreEqual(480, dp.Count);
        }

    }
}
