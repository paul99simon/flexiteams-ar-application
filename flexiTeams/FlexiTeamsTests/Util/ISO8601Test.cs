using FlexiTeams.Util;
using NUnit.Framework;
using System;

namespace FlexiTeamsTests.Util;

[TestFixture]
public class ISO8601Test
{

    [Test]
    public void Test()
    {
        string test1 = "P15Y11M20DT12H15M43S";

        string test2 = "P15Y11M20D";
        string test3 = "P15Y";
        string test4 = "P11M";
        string test5 = "P20D";
        string test6 = "P15Y11M";
        string test7 = "P15Y20D";
        string test8 = "P11M20D";
        string test9 = "PT12H15M43S";
        string test10 = "PT12H";
        string test11 = "PT15M";
        string test12 = "PT43S";
        string test13 = "PT12H15M";
        string test14 = "PT12H43S";
        string test15 = "PT15M43S";

        var iso1 = new ISO8601(test1);
        var iso2 = new ISO8601(test2);
        var iso3 = new ISO8601(test3);
        var iso4 = new ISO8601(test4);
        var iso5 = new ISO8601(test5);
        var iso6 = new ISO8601(test6);
        var iso7 = new ISO8601(test7);
        var iso8 = new ISO8601(test8);
        var iso9 = new ISO8601(test9);
        var iso10 = new ISO8601(test10);
        var iso11 = new ISO8601(test11);
        var iso12 = new ISO8601(test12);
        var iso13 = new ISO8601(test13);
        var iso14 = new ISO8601(test14);
        var iso15 = new ISO8601(test15);

        //iso1 "P15Y11M20DT12H15M43S";
        Assert.AreEqual(15, iso1.Years);
        Assert.AreEqual(11, iso1.Months);
        Assert.AreEqual(20, iso1.Days);
        Assert.AreEqual(12, iso1.Hours);
        Assert.AreEqual(15, iso1.Minutes);
        Assert.AreEqual(43, iso1.Seconds);


        //iso2 "P15Y11M20D"
        Assert.AreEqual(15, iso2.Years);
        Assert.AreEqual(11, iso2.Months);
        Assert.AreEqual(20, iso2.Days);

        //iso3 P15Y
        Assert.AreEqual(15, iso3.Years);

        //iso4 P11M
        Assert.AreEqual(11, iso4.Months);

        //iso5 P20D
        Assert.AreEqual(20, iso5.Days);

        //iso6 P15Y11M
        Assert.AreEqual(15, iso6.Years);
        Assert.AreEqual(11, iso6.Months);

        //iso7 P15Y20D
        Assert.AreEqual(15, iso7.Years);
        Assert.AreEqual(20, iso7.Days);

        //iso8 P11M20D
        Assert.AreEqual(11, iso8.Months);
        Assert.AreEqual(20, iso8.Days);

        //iso9 PT12H15M43S
        Assert.AreEqual(12, iso9.Hours);
        Assert.AreEqual(15, iso9.Minutes);
        Assert.AreEqual(43, iso9.Seconds);

        //iso10 PT12H
        Assert.AreEqual(12, iso10.Hours);

        //iso11 PT15M
        Assert.AreEqual(15, iso11.Minutes);

        //iso12 PT43S
        Assert.AreEqual(43, iso12.Seconds);

        //iso13 PT12H15M
        Assert.AreEqual(12, iso13.Hours);
        Assert.AreEqual(15, iso13.Minutes);

        //iso14 PT12H43S
        Assert.AreEqual(12, iso14.Hours);
        Assert.AreEqual(43, iso14.Seconds);

        //iso15 PT15M43S
        Assert.AreEqual(15, iso15.Minutes);
        Assert.AreEqual(43, iso15.Seconds);

    }
}
