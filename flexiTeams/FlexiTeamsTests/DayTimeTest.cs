using System;
using flexiTeams.Util;
using NUnit.Framework;

namespace FlexiTeamsTests;

[TestFixture]
public class DayTimeTest
{
    [Test]
    public void ToStringTest1()
    {
        //Arrange
        var ti = new DayTime("12:00");
        
        //Assert
        Assert.AreEqual("12:00", ti.ToString());
    }

    [Test]
    public void ToStringTest2()
    {
        //Arrange
        var ti = new DayTime("12:00:30");
        
        //Assert
        Assert.AreEqual("12:00:30", ti.ToString());
    }
    
    [Test]
    public void ToStringTest3()
    {
        //Arrange
        var ti = new DayTime("12:00:30");
        
        //Assert
        Assert.AreEqual("12:00:30", ti.ToString());
    }

    [Test]
    public void IllegalRegexTest()
    {
        //Assert
        ArgumentException ex = Assert.Throws<ArgumentException>(() =>
        {
            DayTime ti = new DayTime("a");
        });

        Assert.AreEqual ("param format must either be hh:mm or hh:mm:ss", ex.Message);
    }

    [Test]
    public void NullParamTest()
    {
        ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
        {
            DayTime ti = new DayTime(null);
        });
    }
}