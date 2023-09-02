using System;
using System.Xml;
using FlexiTeams;
using FlexiTeams.ConstructionClasses;
using FlexiTeams.ConstructionClasses.Builder;
using NUnit.Framework;

namespace FlexiTeamsTests.Inventory;

[TestFixture]
public class ResourcePoolTest
{
    [Test]
    public void GetResoucePool()
    {
        String path = "../../../../resourcePools/resource_pool_draft.xml";

        BasicResourceBuilder builder = new BasicResourceBuilder();
        ResourcePool rp = new ResourcePool(builder, path);

        Assert.AreEqual(3, rp.Count);
    }
}