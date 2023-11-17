using Assets.Scripts.UI.ResourceUI.ContentFiller;
using Assets.Scripts.UI.TaskUI.ContentFIller;
using FlexiTeams.DataClasses.Task;
using TMPro;

namespace Assets.Scripts.UI.ResourceUI
{
    internal class TaskUIData
    {
        private readonly TaskUILayout _layout;
        private readonly Task _task;
        private readonly UISettings _settings;

        public TaskFiller TaskFiller;

        public TaskUIData(TaskUILayout layout, Task task, UISettings settings)
        {
            _layout = layout;
            _task = task;
            _settings = settings;
            FillInData();
        }

        private void FillInData()
        {
            FillInTitleBar();
            FillInHeader();

            TaskFiller = new(_layout.InfoContenObj, _task, _settings);

        }

        private void FillInTitleBar()
        {
            var tmp = _layout.TitleBarTextObj.AddComponent<TextMeshProUGUI>();
            tmp.text = _settings.Language.TaskUI.Title;
        }

        private void FillInHeader()
        {
            var tmp = _layout.IconHeaderTextObj.AddComponent<TextMeshProUGUI>();
            tmp.text = _settings.Language.TaskUI.Icon;

            tmp = _layout.InfoHeaderTextObj.AddComponent<TextMeshProUGUI>();
            tmp.text = _settings.Language.TaskUI.Info;

            tmp = _layout.AssignedResourcesHeaderTextObj.AddComponent<TextMeshProUGUI>();
            tmp.text = _settings.Language.TaskUI.AssignedResources;

            tmp = _layout.AssignedDataHeaderTextObj.AddComponent<TextMeshProUGUI>();
            tmp.text = _settings.Language.TaskUI.AssignedData;
        }


    }
}
