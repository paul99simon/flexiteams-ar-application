using System;
using System.Collections.Generic;
using FlexiTeams.DataClasses.Resource;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.Util;
using NUnit.Framework;

namespace FlexiTeamsTests;

[TestFixture]
public class WorkAgreementTest
{
    [Test]
    public void IsAgreedTimeTest()
    {
        //Arrange
        const string param = "[06:00, 14:00]";

        var ti = new TimeInterval(param);
        
        var workagreement = new List<TimeInterval>[]
        {
            new List<TimeInterval>(),
            new List<TimeInterval>(),
            new List<TimeInterval>(),
            new List<TimeInterval>(),
            new List<TimeInterval>(),
            new List<TimeInterval>(),
            new List<TimeInterval>()
        };
        
        workagreement[0].Add(ti);
        workagreement[1].Add(ti);
        workagreement[2].Add(ti);
        workagreement[3].Add(ti);
        workagreement[4].Add(ti);

        var resource = new Resource();
        resource.AddRange(workagreement);
        
        const int MONDAY = 0;
        const int SUNDAY = 6;
        
        const string param1 = "[08:00, 12:00]";
        const string param2 = "[08:00, 16:00]";
        const string param3 = "[04:00, 12:00]";
        const string param4 = "[04:00, 16:00]";

        var ti1 = new TimeInterval(param1);
        var ti2 = new TimeInterval(param2);
        var ti3 = new TimeInterval(param3);
        var ti4 = new TimeInterval(param4);

        
        //Act
        bool result1 = resource.IsAgreedTime(MONDAY, ti1);
        bool result2 = resource.IsAgreedTime(MONDAY, ti2);
        bool result3 = resource.IsAgreedTime(MONDAY, ti3);
        bool result4 = resource.IsAgreedTime(MONDAY, ti4);

        bool result5 = resource.IsAgreedTime(SUNDAY, ti1);
        
        //Assert
        Assert.AreEqual(true, result1);
        Assert.AreEqual(false, result2);
        Assert.AreEqual(false, result3);
        Assert.AreEqual(false, result4);
        Assert.AreEqual(false, result5);
    }
}