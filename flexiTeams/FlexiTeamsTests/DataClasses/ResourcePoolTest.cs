using System;
using System.Xml;
using FlexiTeams;
using FlexiTeams.ConstructionClasses;
using FlexiTeams.ConstructionClasses.Builder;
using NUnit.Framework;

namespace FlexiTeamsTests;

[TestFixture]
public class ResourcePoolTest
{
    [Test]
    public void getResoucePool()
    {
        String path = "../../../../resourcePools/resource_pool_draft.xml";

        XmlReader reader = XmlReader.Create(path);
        BasicResourceBuilder builder = new BasicResourceBuilder();
        ResourcePool rp = new ResourcePool(builder, reader);

        Assert.AreEqual(3, rp.Size());
    }
}