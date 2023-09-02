using System;
using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Diretor;
using FlexiTeams.FlexiTeamsGraph;
using NUnit.Framework;

namespace FlexiTeamsTests.ConstructionClasses;

[TestFixture]
public class CSVGraphDirectorTest
{
    [Test]
    public void ConstructFromCSVTest()
    {
        string path = "C:/Users/paul9/OneDrive/FlexiTeams/Resourcen/workflows.csv";
        

        AdjListsGraph graph = new AdjListsGraph();

        CSVGraphDirector.ConstructFromCsv(path, graph, new BasicWorkflowBuilder(), new BasicTaskBuilder());

        Console.WriteLine(graph.ToString());

    }
    
}