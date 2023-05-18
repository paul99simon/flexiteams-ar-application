using System;
using flexiTeams.Util;
using NUnit.Framework;

namespace FlexiTeamsTests;

[TestFixture]
public class ISO_639_1_Tests
{
    [Test]
    public void GetLanguage()
    {
        Assert.AreEqual("german", ISO_639_1.GetLanguage("DE"));
    }
    
    [Test]
    public void GetCode()
    {
        Assert.AreEqual( "de", ISO_639_1.GetCode("German"));
    }
    
    [Test]
    public void GetLanguageNull()
    {
        //Assert
        ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
        {
            ISO_639_1.GetLanguage(null);
        });
    }

    [Test]
    public void GetCodeNull()
    {
        //Assert
        ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
        {
            ISO_639_1.GetCode(null);
        });
    }
    
    [Test]
    public void GetLanguageEmptyString()
    {
        //Assert
        ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
        {
            ISO_639_1.GetLanguage("");
        });
    }
    
    [Test]
    public void GetCodeEmptyString()
    {
        //Assert
        ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
        {
            ISO_639_1.GetCode("");
        });
    }
    
    [Test]
    public void IsValidLanguage()
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