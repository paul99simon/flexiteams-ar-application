using System;
using System.ComponentModel;
using flexiTeams.Util;
using NUnit.Framework;

namespace FlexiTeamsTests;

[TestFixture]
public class TimeIntervalTest
{
    [Test]
    public void ToStringTest1()
    {
        var ti = new TimeInterval("12:00", "14:30");
        
        Assert.AreEqual("[12:00, 14:30]", ti.ToString());
    }

    [Test]
    public void ToStringTest2()
    {
        
        var ti = new TimeInterval("[12:00, 14:00]");
        
        Assert.AreEqual("[12:00, 14:00]", ti.ToString());

    }

    [Test]
    public void IllegalRegexTest1()
    {
        //Assert
        ArgumentException ex = Assert.Throws<ArgumentException>(() =>
        {
            TimeInterval ti = new TimeInterval("a", "12:00");
        });
        
        Assert.AreEqual(ex.Message, "\"a\" doesnt match \"HH:MM\" format");
    }
    
    [Test]
    public void IllegalRegexTest2()
    {
        //Assert
        ArgumentException ex = Assert.Throws<ArgumentException>(() =>
        {
            TimeInterval ti = new TimeInterval("12:00", "a");
        });
        
        Assert.AreEqual(ex.Message, "\"a\" doesnt match \"HH:MM\" format");
    }
    
    [Test]
    public void IllegalRegexTest3()
    {
        //Assert
        ArgumentException ex = Assert.Throws<ArgumentException>(() =>
        {
            TimeInterval ti = new TimeInterval("a12:00", "14:00");
        });
        
        Assert.AreEqual(ex.Message, "\"a12:00\" doesnt match \"HH:MM\" format");
    }

    public void NullParamTest1()
    {
        
    }
}