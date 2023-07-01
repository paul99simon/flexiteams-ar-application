using System;
using System.Xml;
using FlexiTeams.Data;
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
        ResourceBuilder builder = new ResourceBuilder();
        ResourcePool rp = new ResourcePool(builder, reader);

        Assert.AreEqual(3, rp.Size());
    }
}