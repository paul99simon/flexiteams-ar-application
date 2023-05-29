using System;
using flexiTeams.Util;
using NUnit.Framework;

namespace FlexiTeamsTests;

[TestFixture]
public class TimeIntervalTest
{
    
    [Test]
    public void IllegalRegexTest()
    {
        //Arrange
        const string param1 = "a";
        const string param2 = "[a12:00, 13:00]";
        const string param3 = "[112:00, 13:00]";
        //const string param4 = "[12:00, 12:00]";
        const string param5 = "[24:00, 13:00]";
        const string param6 = "[12:60, 13:00]";
        const string param7 = "[12:00:60, 13:00]";
        
        //Act
        var e1 = Assert.Throws<ArgumentException>(() => new TimeInterval(param1));
        var e2 = Assert.Throws<ArgumentException>(() => new TimeInterval(param2));
        var e3 = Assert.Throws<ArgumentException>(() => new TimeInterval(param3));
        //var e4 = Assert.Throws<ArgumentException>(() => new TimeInterval(param4));
        var e5 = Assert.Throws<ArgumentException>(() => new TimeInterval(param5));
        var e6 = Assert.Throws<ArgumentException>(() => new TimeInterval(param6));
        var e7 = Assert.Throws<ArgumentException>(() => new TimeInterval(param7));
        
        //Assert
        Assert.AreEqual("param format must either be [hh:mm, hh:mm] or [hh:mm:ss, hh:mm:ss] or [hh:mm, hh:mm:ss] or [hh:mm:ss, hh:mm]", e1.Message);
        Assert.AreEqual("param format must either be [hh:mm, hh:mm] or [hh:mm:ss, hh:mm:ss] or [hh:mm, hh:mm:ss] or [hh:mm:ss, hh:mm]", e2.Message);
        Assert.AreEqual("param format must either be [hh:mm, hh:mm] or [hh:mm:ss, hh:mm:ss] or [hh:mm, hh:mm:ss] or [hh:mm:ss, hh:mm]", e3.Message);
        //Assert.AreEqual("begin time must differ from end time", e4.Message);
        Assert.AreEqual("hours must be between 0 and 23", e5.Message);
        Assert.AreEqual("minutes must be between 0 and 59", e6.Message);
        Assert.AreEqual("seconds must be between 0 and 59", e7.Message);
    }

    [Test]
    public void NullParamTest()
    {
        //Assert
        Assert.Throws<ArgumentNullException>(() => new TimeInterval(null));
        Assert.Throws<ArgumentNullException>(() => new TimeInterval(null, null));
    }
    
    [Test]
    public void ToStringTest()
    {
        //Arrange
        const string param1 = "[12:00, 13:00]";
        const string param2 = "[12:00:00, 13:00:00]";
        const string param3 = "[12:00:15, 13:00]";
        const string param4 = "[12:00, 13:00:15]";        
        const string param5 = "[12:00:15, 13:00:15]";

        //Act
        TimeInterval ti1 = new TimeInterval(param1);
        TimeInterval ti2 = new TimeInterval(param2);
        TimeInterval ti3 = new TimeInterval(param3);
        TimeInterval ti4 = new TimeInterval(param4);
        TimeInterval ti5 = new TimeInterval(param5);
        
        //Assert
        Assert.AreEqual("[12:00, 13:00]", ti1.ToString());
        Assert.AreEqual("[12:00, 13:00]", ti2.ToString());
        Assert.AreEqual("[12:00:15, 13:00]", ti3.ToString());
        Assert.AreEqual("[12:00, 13:00:15]", ti4.ToString());
        Assert.AreEqual("[12:00:15, 13:00:15]", ti5.ToString());
    }
    
    [Test]
    public void ContainsTest()
    {
        //Arrange
        const string param1  = "[12:00, 20:00]";
        const string param2  = "[12:15, 20:15]";
        const string param3  = "[11:45, 19:45]";
        const string param4  = "[12:15, 19:45]";
        const string param5  = "[11:45, 20:15]";
        const string param6  = "[19:45, 12:15]";
        
        const string param7  = "[22:00, 06:00]";
        const string param8  = "[22:15, 06:15]";
        const string param9  = "[21:45, 05:45]";
        const string param10 = "[22:15, 05:45]";
        const string param11 = "[21:45, 06:15]";
        const string param12 = "[05:45, 22:15]";
        
        var ti1 = new TimeInterval(param1);
        var ti2 = new TimeInterval(param2);
        var ti3 = new TimeInterval(param3);
        var ti4 = new TimeInterval(param4);
        var ti5 = new TimeInterval(param5);
        var ti6 = new TimeInterval(param6);
        
        var ti7 = new TimeInterval(param7);
        var ti8 = new TimeInterval(param8);
        var ti9 = new TimeInterval(param9);
        var ti10 = new TimeInterval(param10);
        var ti11 = new TimeInterval(param11);
        var ti12 = new TimeInterval(param12);
        
        //Assert
        Assert.AreEqual(true, ti1.Contains(ti1));
        Assert.AreEqual(false, ti1.Contains(ti2));
        Assert.AreEqual(false, ti1.Contains(ti3));
        Assert.AreEqual(true, ti1.Contains(ti4));
        Assert.AreEqual(false, ti1.Contains(ti5));
        Assert.AreEqual(false, ti1.Contains(ti6));
        
        Assert.AreEqual(true, ti7.Contains(ti7));
        Assert.AreEqual(false, ti7.Contains(ti8));
        Assert.AreEqual(false, ti7.Contains(ti9));
        Assert.AreEqual(true, ti7.Contains(ti10));
        Assert.AreEqual(false, ti7.Contains(ti11));
        Assert.AreEqual(false, ti7.Contains(ti12));
    }

    [Test]
    public void GetLengthTest()
    {
        //Arrange
        const string param1 = "[06:00, 12:00]";
        const string param2 = "[22:00, 06:00]";
        var ti1 = new TimeInterval(param1);
        var ti2 = new TimeInterval(param2);
        
        const string expected1 = "06:00";
        const string expected2 = "08:00";
        var dtex1 = new DayTime(expected1);
        var dtex2 = new DayTime(expected2);

        //Act

        var dt1 = ti1.GetLength();
        var dt2 = ti2.GetLength();

        //Assert
        Assert.AreEqual(true, dt1.Equals(dtex1));  
        Assert.AreEqual(true, dt2.Equals(dtex2));
    }

    [Test]
    public void IntersectsTest()
    {
        //Arrange
        const string param1 = "[06:00, 12:00]";
        const string param2 = "[08:00, 14:00]";
        const string param3 = "[04:00, 10:00]";
        const string param4 = "[08:00, 10:00]";
        const string param5 = "[04:00, 14:00]";
        const string param6 = "[13:00, 18:00]";

        const string param7 = "[12:00, 06:00]";
        const string param8 = "[14:00, 08:00]";
        const string param9 = "[10:00, 04:00]";
        const string param10 = "[14:00, 04:00]";
        const string param11 = "[10:00, 08:00]";
        const string param12 = "[08:00, 10:00]";

        const string param13 = "[10:00, 08:00]";
        const string param14 = "[10:00, 04:00]";
        const string param15 = "[14:00, 08:00]";
        const string param16 = "[14:00, 04:00]";
        
        const string param17 = "[04:00, 14:00]";
        const string param18 = "[04:00, 10:00]";
        const string param19 = "[08:00, 14:00]";
        const string param20 = "[08:00, 10:00]";

        const string param21 = "[12:00, 20:00]";

        var ti1 = new TimeInterval(param1);
        var ti2 = new TimeInterval(param2);
        var ti3 = new TimeInterval(param3);
        var ti4 = new TimeInterval(param4);
        var ti5 = new TimeInterval(param5);
        var ti6 = new TimeInterval(param6);
        
        var ti7 = new TimeInterval(param7);
        var ti8 = new TimeInterval(param8);
        var ti9 = new TimeInterval(param9);
        var ti10 = new TimeInterval(param10);
        var ti11 = new TimeInterval(param11);
        var ti12 = new TimeInterval(param12);
        
        var ti13 = new TimeInterval(param13);
        var ti14 = new TimeInterval(param14);
        var ti15 = new TimeInterval(param15);
        var ti16 = new TimeInterval(param16);
        
        var ti17 = new TimeInterval(param17);
        var ti18 = new TimeInterval(param18);
        var ti19 = new TimeInterval(param19);
        var ti20 = new TimeInterval(param20);

        var ti21 = new TimeInterval(param21);
        
        //Act
        bool result1 = ti1.Intersects(ti1);
        bool result2 = ti1.Intersects(ti2);
        bool result3 = ti1.Intersects(ti3);
        bool result4 = ti1.Intersects(ti4);
        bool result5 = ti1.Intersects(ti5);
        bool result6 = ti1.Intersects(ti6);
        
        bool result7 = ti7.Intersects(ti7);
        bool result8 = ti7.Intersects(ti8);
        bool result9 = ti7.Intersects(ti9);
        bool result10 = ti7.Intersects(ti10);
        bool result11 = ti7.Intersects(ti11);
        bool result12 = ti7.Intersects(ti12);
        
        bool result13 = ti1.Intersects(ti13);
        bool result14 = ti1.Intersects(ti14);
        bool result15 = ti1.Intersects(ti15);
        bool result16 = ti1.Intersects(ti16);

        bool result17 = ti7.Intersects(ti17);
        bool result18 = ti7.Intersects(ti18);
        bool result19 = ti7.Intersects(ti19);
        bool result20 = ti7.Intersects(ti20);

        bool result21 = ti1.Intersects(ti21);
        
        //Assert
        Assert.AreEqual(true, result1);
        Assert.AreEqual(true, result2);
        Assert.AreEqual(true, result3);
        Assert.AreEqual(true, result4);
        Assert.AreEqual(true, result5);
        Assert.AreEqual(false, result6);
        
        Assert.AreEqual(true, result7);
        Assert.AreEqual(true, result8);
        Assert.AreEqual(true, result9);
        Assert.AreEqual(true, result10);
        Assert.AreEqual(true, result11);
        Assert.AreEqual(false, result12);
        
        Assert.AreEqual(true, result13);
        Assert.AreEqual(true, result14);
        Assert.AreEqual(true, result15);
        Assert.AreEqual(false, result16);

        Assert.AreEqual(true, result17);
        Assert.AreEqual(true, result18);
        Assert.AreEqual(true, result19);
        Assert.AreEqual(false, result20);

        Assert.AreEqual(true, result21);
    }
}