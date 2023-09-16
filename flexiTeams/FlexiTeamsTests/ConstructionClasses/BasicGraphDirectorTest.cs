using System;
using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Diretor;
using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Graph.Nodes;
using FlexiTeams.Inventory;
using NUnit.Framework;

namespace FlexiTeamsTests.ConstructionClasses;

[TestFixture]
public class BasicGraphDirectorTest
{
    [Test]
    public void ConstructFromCSVTest()
    {
        string path = "C:/Users/paul9/OneDrive/FlexiTeams/flexiteams_ar-application/flexiTeams/FlexiTeamsTests/Resources/workflows.csv";
        
        var wPool = new WorkFlowPool();
        var tPool = new TaskPool();


        AdjListsGraph graph  = BasicGraphDirector.ConstructFromCsv(path, wPool, tPool );

        Console.WriteLine(graph.ToString());

    }
    
}