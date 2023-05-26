using System;
using flexiTeams.Util;
using NUnit.Framework;

namespace FlexiTeamsTests;

[TestFixture]
public class ISO_639_1_Tests
{
    [Test]
    public void GetLanguageTest()
    {
        //Arrange
        const string param1 = "DE";
        const string param2 = null;
        const string param3 = "";
        
        //Act
        string result1 = ISO_639_1.GetLanguage(param1);
        
        //Assert
        Assert.AreEqual("german", result1);
        Assert.Throws<ArgumentNullException>(() =>
        {
            ISO_639_1.GetLanguage(param2);
        });
        Assert.Throws<ArgumentNullException>(() =>
        {
            ISO_639_1.GetLanguage(param3);
        });
    }
    
    [Test]
    public void GetCodeTest()
    {
        //Arrange
        const string param1 = "German";
        const string param2 = null;
        const string param3 = "";
        
        //Act
        string result1 = ISO_639_1.GetCode(param1);
        
        //Assert
        Assert.AreEqual("de", result1);
        Assert.Throws<ArgumentNullException>(() =>
        {
            ISO_639_1.GetCode(param2);
        });
        Assert.Throws<ArgumentNullException>(() =>
        {
            ISO_639_1.GetCode(param3);
        });
    }
    
    [Test]
    public void IsValidLanguageTest()
    {
        Assert.IsTrue(ISO_639_1.IsValidLanguage("german"));
    }
    
    [Test]
    public void IsValidCode()
    {
        Assert.IsTrue(ISO_639_1.IsValidCode("de"));
    }
    
    [Test]
    public void IsValidLanguageNull()
    {
        //Assert
        ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
        {
            ISO_639_1.IsValidLanguage(null);
        });
    }
    
    [Test]
    public void IsValidCodeNull()
    {
        //Assert
        ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
        {
            ISO_639_1.IsValidCode(null);
        });
    }
    
    [Test]
    public void IsValidLanguageEmptyString()
    {
        //Assert
        ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
        {
            ISO_639_1.IsValidLanguage("");
        });
    }
    
    [Test]
    public void IsValidCodeEmptyString()
    {
        //Assert
        ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
        {
            ISO_639_1.IsValidCode("");
        });
    }
    
    [Test]
    public void IsNotValidLanguage()
    {
        Assert.IsFalse(ISO_639_1.IsValidLanguage("deutsch"));
    }
    
    [Test]
    public void IsNotValidCode()
    {
        Assert.IsFalse(ISO_639_1.IsValidCode("dex"));
    }
}