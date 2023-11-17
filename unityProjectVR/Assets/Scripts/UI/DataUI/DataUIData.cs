using Assets.Scripts.UI.ResourceUI.ContentFiller;
using FlexiTeams.DataClasses.Data;
using FlexiTeams.DataClasses.Resource;
using System.Collections.Generic;
using TMPro;

namespace Assets.Scripts.UI.DataUI
{
    internal class DataUIData
    {
        private readonly DataUILayout _layout;
        private readonly Data _data;
        private readonly UISettings _settings;


        public DataFiller DataFiller;
        public TasksFiller TasksFiller;

        public DataUIData(DataUILayout layout, Data data, UISettings settings)
        {
            _layout = layout;
            _data = data;
            _settings = settings;
            FillInData();
        }

        private void FillInData()
        {
            FillInTitleBar();
            FillInHeader();

            DataFiller = new(_layout.DataContenObj, _data, _settings);
            TasksFiller = new(_layout.TasksContenObj, _data, _settings);
        }

        private void FillInTitleBar()
        {
            var tmp = _layout.TitleBarTextObj.AddComponent<TextMeshProUGUI>();
            tmp.text = _settings.Language.DataUI.Title;
        }

        private void FillInHeader()
        {
            var tmp = _layout.IconHeaderTextObj.AddComponent<TextMeshProUGUI>();
            tmp.text = _settings.Language.DataUI.Icon;

            tmp = _layout.DataHeaderTextObj.AddComponent<TextMeshProUGUI>();
            tmp.text = _settings.Language.DataUI.Info;

            tmp = _layout.TasksHeaderTextObj.AddComponent<TextMeshProUGUI>();
            tmp.text = _settings.Language.DataUI.Tasks;
        }
    }
}
