using System;
using FlexiTeams.Data;
using NUnit.Framework;

namespace FlexiTeamsTests;

[TestFixture]
public class ResourcePoolTest
{
    [Test]
    public void getResoucePool()
    {
        Uri uri = new Uri("../../resourcePools/resource_pool_draft.xml");
        ResourcePool rp = new ResourcePool(uri);
    }
}