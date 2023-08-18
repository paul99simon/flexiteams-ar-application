using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using CsvHelper;
using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Diretor;
using FlexiTeams.Util;
using NUnit.Framework;

namespace FlexiTeamsTests;

[TestFixture]
public class DataPoolXmlWriterTest
{
    [Test]
    public void createDataPoolXml()
    {
        List<string> consumedDataList = new ();

        
        using var reader = new StreamReader("C:/Users/paul9/OneDrive/FlexiTeams/Resourcen/workflows.csv");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        CSVGraphDirector.ConstructFromCSVReader(csv, new BasicTaskBuilder(), new BasicDataBuilder(),new BasicWorkflowBuilder());
        csv.Read();
        csv.Read();
        while (csv.Read())
        {
            string consumedData = csv.GetField(9);
            string[] temp = consumedData.Split(',');
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = temp[i].Trim();
            }
            
            consumedDataList.AddRange(temp);

            Dictionary<string, int> dict = new();
            
            foreach (var s in consumedDataList)
            {
                if(!dict.ContainsKey(s)) dict.Add(s, 0);
                dict[s]++;
            }

            consumedDataList = new List<string>();
             foreach (var pair in dict)
             {
                 if(pair.Value ==  1) consumedDataList.Add(pair.Key); 
             }
        }

        XmlDocument doc =DataPoolXmlWriter.DataXml(consumedDataList, 50);
        using TextWriter text = new StreamWriter("C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/dataPools/55DataPool.xml");
        doc.Save(text);

    }
    
}