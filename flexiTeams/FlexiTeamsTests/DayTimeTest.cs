using System;
using flexiTeams.Util;
using NUnit.Framework;

namespace FlexiTeamsTests;

[TestFixture]
public class DayTimeTest
{
    [Test]
    public void ToStringTest()
    {
        //Arrange
        var ti1 = new DayTime("12:00");
        var ti2 = new DayTime("12:00:00");
        var ti3 = new DayTime("12:00:30");
        var ti4 = new DayTime(12, 0);
        var ti5 = new DayTime(12, 0, 0);
        
        //Assert
        Assert.AreEqual("12:00", ti1.ToString());        
        Assert.AreEqual("12:00", ti2.ToString());
        Assert.AreEqual("12:00:30", ti3.ToString());
        Assert.AreEqual("12:00", ti4.ToString());        
        Assert.AreEqual("12:00", ti5.ToString());
    }
    
    [Test]
    public void IllegalRegexTest()
    {
        //Arrange
        string param1 = "a";
        string param2 = "25:00";
        string param3 = "00:60";
        string param4 = "00:00:60";
        
        
        //Act
        ArgumentException e1 = Assert.Throws<ArgumentException>(() => new DayTime(param1));
        ArgumentException e2 = Assert.Throws<ArgumentException>(() => new DayTime(param2));
        ArgumentException e3 = Assert.Throws<ArgumentException>(() => new DayTime(param3));
        ArgumentException e4 = Assert.Throws<ArgumentException>(() => new DayTime(param4));

        //Assert
        Assert.AreEqual ("param format must either be hh:mm or hh:mm:ss", e1.Message);
        Assert.AreEqual("hours must be between 0 and 23", e2.Message);
        Assert.AreEqual("minutes must be between 0 and 59", e3.Message);
        Assert.AreEqual("seconds must be between 0 and 59", e4.Message);
    }

    [Test]
    public void NullParamTest()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            DayTime ti = new DayTime(null);
        });
    }

    [Test]
    public void EqualsTest()
    {
        //Arrange
        DayTime ti1 = new DayTime("12:15:30");
        DayTime ti2 = new DayTime("12:15:30");
        DayTime ti3 = new DayTime("00:00");
        DayTime ti4 = new DayTime("00:00:00");
        
        //Assert
        Assert.AreEqual(true, ti1.Equals(ti2));
        Assert.AreEqual(false, ti1.Equals(ti3));
        Assert.AreEqual(true, ti3.Equals(ti4));
    }

    [Test]
    public void LessThenGreaterThenTest()
    {
        //Arrange
        string param1 = "12:00";
        string param2 = "13:00";
        string param3 = "12:01";
        string param4 = "12:00:01";

        //Act
        DayTime ti1 = new DayTime(param1);
        DayTime ti2 = new DayTime(param2);
        DayTime ti3 = new DayTime(param3);
        DayTime ti4 = new DayTime(param4);
        DayTime ti5 = null;
        
        //Assert
        Assert.AreEqual(false, ti1 < ti1 );
        Assert.AreEqual(false, ti1 > ti1 );
        Assert.AreEqual(true, ti1 < ti2 );
        Assert.AreEqual(false, ti1 > ti2);
        Assert.AreEqual(true, ti1 < ti3 );
        Assert.AreEqual(false, ti1 > ti3);
        Assert.AreEqual(true, ti1 < ti4 );
        Assert.AreEqual(false, ti1 > ti4);
        Assert.Throws<NullReferenceException>(() =>
        {
            var b = ti1 < ti5;
        });
        Assert.Throws<NullReferenceException>(() =>
        {
            var b = ti1 > ti5;
        });

    }

    [Test]
    public void LessThenEqualsGreaterThenEqualsTest()
    {
        //Arrange
        string param1 = "12:00";
        string param2 = "13:00";
        string param3 = "12:01";
        string param4 = "12:00:01";

        //Act
        DayTime ti1 = new DayTime(param1);
        DayTime ti2 = new DayTime(param2);
        DayTime ti3 = new DayTime(param3);
        DayTime ti4 = new DayTime(param4);
        DayTime ti5 = null;
        
        //Assert
        Assert.AreEqual(true, ti1 <= ti1 );
        Assert.AreEqual(true, ti1 >= ti1 );
        Assert.AreEqual(true, ti1 <= ti2 );
        Assert.AreEqual(false, ti1 >= ti2);
        Assert.AreEqual(true, ti1 <= ti3 );
        Assert.AreEqual(false, ti1 >= ti3);
        Assert.AreEqual(true, ti1 <= ti4 );
        Assert.AreEqual(false, ti1 >= ti4);
        Assert.Throws<NullReferenceException>(() =>
        {
            var b = ti1 <= ti5;
        });
        Assert.Throws<NullReferenceException>(() =>
        {
            var b = ti1 >= ti5;
        });
    }
}