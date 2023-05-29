using System.Collections.Generic;
using FlexiTeams.Data.Wrapper;
using flexiTeams.Util;
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
        
        var monday = new List<TimeInterval>(){ti};
        var tuesday = new List<TimeInterval>(){ti};
        var wednesday = new List<TimeInterval>(){ti};
        var thursday = new List<TimeInterval>(){ti};
        var friday = new List<TimeInterval>(){ti};
        var saturday = new List<TimeInterval>();
        var sunday = new List<TimeInterval>();
        
        var schedule = new[] {monday, tuesday, wednesday, thursday, friday, saturday, sunday};
        
        var workagreement = new WorkAgreement(schedule);
        
        const int MONDAY = 0;
        const int TUESDAY = 1;
        const int WEDNESDAY = 2;
        const int THURSDAY = 3;
        const int FRIDAY = 4;
        const int SATURDAY = 5;
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
        bool result1 = workagreement.IsAgreedTime(MONDAY, ti1);
        bool result2 = workagreement.IsAgreedTime(MONDAY, ti2);
        bool result3 = workagreement.IsAgreedTime(MONDAY, ti3);
        bool result4 = workagreement.IsAgreedTime(MONDAY, ti4);

        bool result5 = workagreement.IsAgreedTime(SUNDAY, ti1);
        
        //Assert
        Assert.AreEqual(true, result1);
        Assert.AreEqual(false, result2);
        Assert.AreEqual(false, result3);
        Assert.AreEqual(false, result4);
        Assert.AreEqual(false, result5);
    }
}