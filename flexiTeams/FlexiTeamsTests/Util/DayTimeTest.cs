using System;
using FlexiTeams.Util;
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

    [Test]
    public void plusTest()
    {
        //Hours
        //Arrange
        const string param1 = "06:00";
        const string param2 = "18:00";
        var dt1 = new DayTime(param1);
        var dt2 = new DayTime(param2);
        
        const string ex1 = "00:00";
        const string ex2 = "12:00";
        var dte1 = new DayTime(ex1);
        var dte2 = new DayTime(ex2);
        
        //Act
        var result1 = dt1 + dt1;
        var result2 = dt1 + dt2;
        var result3 = dt2 + dt2;
        
        //Assert
        Assert.AreEqual(true, result1.Equals(dte2));
        Assert.AreEqual(true, result2.Equals(dte1));
        Assert.AreEqual(true, result3.Equals(dte2));

        //Minutes
        //Arrange
        const string param3 = "23:15";
        const string param4 = "00:15";
        const string param5 = "00:45";
        var dt3 = new DayTime(param3);
        var dt4 = new DayTime(param4);
        var dt5 = new DayTime(param5);
        
        const string ex3 = "23:30";
        const string ex4 = "00:00";
        var dte3 = new DayTime(ex3);
        var dte4 = new DayTime(ex4);
        
        //Act
        var result4 = dt3 + dt4;
        var result5 = dt3 + dt5;
        
        //Assert
        Assert.AreEqual(true, result4.Equals(dte3));
        Assert.AreEqual(true, result5.Equals(dte4));
        
        //Seconds
        //Arrange
        const string param6 = "23:59:15";
        const string param7 = "00:00:15";
        const string param8 = "00:00:45";
        var dt6 = new DayTime(param6);
        var dt7 = new DayTime(param7);
        var dt8 = new DayTime(param8);
        
        const string ex5 = "23:59:30";
        const string ex6 = "00:00:00";
        var dte5 = new DayTime(ex5);
        var dte6 = new DayTime(ex6);
        
        //Act
        var result6 = dt6 + dt7;
        var result7 = dt6 + dt8;
        
        //Assert
        Assert.AreEqual(true, result6.Equals(dte5));
        Assert.AreEqual(true, result7.Equals(dte6));
    }

    [Test]
    public void minusTest()
    {
        //Hours
        //Arrange
        const string param1 = "06:00";
        const string param2 = "12:00";
        var dt1 = new DayTime(param1);
        var dt2 = new DayTime(param2);

        const string ex1 = "18:00";
        const string ex2 = "06:00";
        var dte1 = new DayTime(ex1);
        var dte2 = new DayTime(ex2);
        
        //Act
        var result1 = dt1 - dt2;
        var result2 = dt2 - dt1;
        
        //Assert
        Assert.AreEqual(true, result1.Equals(dte1));
        Assert.AreEqual(true, result2.Equals(dte2));

        //Minutes
        //Arrange
        const string param3 = "00:30";
        const string param4 = "00:15";
        const string param5 = "00:45";
        var dt3 = new DayTime(param3);
        var dt4 = new DayTime(param4);
        var dt5 = new DayTime(param5);
        
        const string ex3 = "00:15";
        const string ex4 = "23:45";
        var dte3 = new DayTime(ex3);
        var dte4 = new DayTime(ex4);
        
        //Act
        var result3 = dt3 - dt4;
        var result4 = dt3 - dt5;
        
        //Assert
        Assert.AreEqual(true, result3.Equals(dte3));
        Assert.AreEqual(true, result4.Equals(dte4));

        //Seconds
        //Arrange
        const string param6 = "00:00:30";
        const string param7 = "00:00:15";
        const string param8 = "00:00:45";
        var dt6 = new DayTime(param6);
        var dt7 = new DayTime(param7);
        var dt8 = new DayTime(param8);
        
        const string ex5 = "00:00:15";
        const string ex6 = "23:59:45";
        var dte5 = new DayTime(ex5);
        var dte6 = new DayTime(ex6);
        
        //Act
        var result5 = dt6 - dt7;
        var result6 = dt6 - dt8;
        
        //Assert
        Assert.AreEqual(true, result5.Equals(dte5));
        Assert.AreEqual(true, result6.Equals(dte6));
    }
}