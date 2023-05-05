using flexiTeams.Util;
using NUnit.Framework;

namespace FlexiTeamsTests;

[TestFixture]
public class ISO_639_1_Tests
{
    [Test]
    public void GetISO_639_LanguageNames()
    {
        
    }
    
    [Test]
    public void GetISO_639_Code()
    {
        Assert.AreEqual( "de", ISO_639_1.GetISO_639_Code("German"));
    }
    
    [Test]
    public void GetISO_639_LanguageNameNull()
    {
        
    }
    
    [Test]
    public void GetISO_639_CodeNull()
    {
        
    }
    
    [Test]
    public void GetISO_639_LanguageNameEmptyString()
    {
        
    }
    
    [Test]
    public void GetISO_639_CodeEmptyString()
    {
        
    }
    
    [Test]
    public void IsValidISO_639_languageName()
    {
        
    }
    
    [Test]
    public void IsValidISO_639_Code()
    {
        
    }
    
    [Test]
    public void IsValidISO_639_languageNameNull()
    {
        
    }
    
    [Test]
    public void IsValidISO_639_CodeNull()
    {
        
    }
    
    [Test]
    public void IsNotValidISO_639_languageName()
    {
        
    }
    
    [Test]
    public void IsNotValidISO_639_Code()
    {
        
    }

    
}