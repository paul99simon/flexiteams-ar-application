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
    public void CreateDataPoolXml()
    {
        int count = 20;
        List<string> consumedDataList = new ();

        
        using var reader = new StreamReader("C:/Users/paul9/OneDrive/FlexiTeams/Resourcen/workflows.csv");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
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

            List<string> list  = new();
       
            
            foreach (var s in consumedDataList)
            {
                if(!list.Contains(s)) list.Add(s);
            }

            consumedDataList = list;
        }

        XmlDocument doc =DataPoolXmlWriter.DataXml(consumedDataList, count);
        using TextWriter text = new StreamWriter("C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/dataPools/" + count + "DataPool.xml");
        doc.Save(text);

    }
    
}