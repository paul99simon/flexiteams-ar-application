using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using Task = FlexiTeams.DataClasses.Task.Task;

namespace FlexiTeams.Graph.Nodes;

public class TaskNode : Node
{
    public Task Task { get; }

    public Dictionary<Profession, int> AssignedProfessions { get; } = new();
    public Dictionary<DataName, int> AssignedDataNames { get; } = new();

    public TaskNode(Task task)
    {
        Task = task;
        Update();
    }

    private void Update()
    {
        AssignedDataNames.Clear();
        AssignedProfessions.Clear();

        foreach (var dataName in Task.RequiredData)
        {
            AssignedDataNames.Add(dataName, 0);
        }
        foreach (var profession in Task.RequiredProfessions)
        {
            AssignedProfessions.Add(profession, 0);
        }
    }

    public override string GetLanguage()
    {
        return Task.GetLanguage();
    }

    public override void SetLanguage(string langCode)
    {
        Task.SetLanguage(langCode);
    }
}