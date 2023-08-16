using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Diretor;
using NUnit.Framework;

namespace FlexiTeamsTests.ConstructionClasses;

[TestFixture]
public class CSVGraphDirectorTest
{
    [Test]
    public void ConstructFromCSVTest()
    {
        using var reader = new StreamReader("C:/Users/paul9/OneDrive/FlexiTeams/Resourcen/workflows.csv");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        CSVGraphDirector.ConstructFromCSVReader(reader, new BasicTaskBuilder(), new BasicDataBuilder(),);
        csv.Read();
        csv.Read();
        while (csv.Read())
        {
            string temp = csv.GetField(0);
            if (temp.Equals("")) continue;
            Console.WriteLine(temp);
        }
    }
    
}