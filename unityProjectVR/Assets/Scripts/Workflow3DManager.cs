using FlexiTeams.ConstructionClasses.Builder;
using FlexiTeams.ConstructionClasses.Diretor;
using FlexiTeams.FlexiTeamsGraph;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workflow3DManager : MonoBehaviour
{

    [SerializeField]
    private string path;
    // Start is called before the first frame update
    void Start()
    {
        AdjListsGraph graph = new AdjListsGraph();
        BasicGraphDirector.ConstructFromCsv(path, graph, new BasicWorkflowBuilder(), new BasicTaskBuilder());

        Debug.Log(graph.GetWorkflowNodes().Count);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
