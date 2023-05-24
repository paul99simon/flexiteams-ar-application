using System;
using System.ComponentModel;
using flexiTeams.Util;
using NUnit.Framework;

namespace FlexiTeamsTests;

[TestFixture]
public class TimeIntervalTest
{
    
    [Test]
    public void ContainsTest1()
    {
        //Arrange
        TimeInterval ti1 = new TimeInterval("[06:00, 12:00]");
        TimeInterval ti2 = new TimeInterval("[07:00, 11:00]");
        bool result;

        //Act
        result = ti1.Contains(ti2);
        
        //Assert
        Assert.AreEqual(true, result);
    }
    
    
}