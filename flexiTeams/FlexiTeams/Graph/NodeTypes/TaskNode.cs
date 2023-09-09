using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.DataClasses.Resource.Wrapper;
using FlexiTeams.DataClasses.Wrapper;
using Task = FlexiTeams.DataClasses.Task.Task;

namespace FlexiTeams.Graph.Nodes;

public class TaskNode : Node
{
    public Task Task { get; }

    public Dictionary<Profession, ResourceId> ResourceAllocation { get; } = new();
    public Dictionary<DataName, DataId> DataAllocation { get; } = new();

    public TaskNode(Task task)
    {
        Task = task;
        Update();
    }

    private void Update()
    {
        ResourceAllocation.Clear();
        DataAllocation.Clear();

        Task.RequiredProfessions.ForEach(profession =>
        {
            ResourceAllocation.Add(profession, null);
        });

        Task.RequiredData.ForEach(dataName =>
        {
            DataAllocation.Add(dataName, null);
        });
    }

    public override string GetLanguage()
    {
        return Task.GetLanguage();
    }

    public override void SetLanguage(string langCode)
    {
        Task.SetLanguage(langCode);
        Update();
    }
}