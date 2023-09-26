using FlexiTeams;
using FlexiTeams.FlexiTeamsGraph;
using FlexiTeams.Inventory;
using FlexiTeams.IO;
using UnityEngine;

public class ImportManager : MonoBehaviour
{
    [SerializeField]
    private string scenarioPath;
    private Import import;

    public ResourcePool ResourcePool;
    public DataPool DataPool;
    public WorkflowPool WorkflowPool;
    public TaskPool TaskPool;
    public AdjListsGraph Graph;

    // Start is called before the first frame update
    void Awake()
    {
        import = new(scenarioPath);
        ResourcePool = import.ResourcePool;
        DataPool = import.DataPool;
        WorkflowPool = import.WorkflowPool;
        TaskPool = import.TaskPool;
        Graph = import.Graph;
    }
}
