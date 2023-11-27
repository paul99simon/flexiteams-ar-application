using FlexiTeams.DataClasses.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Task = FlexiTeams.DataClasses.Task.Task;

namespace Assets.Scripts.UI.TaskUI.ContentFiller
{
    public class TaskFiller
    {
        public List<GameObject> TextObjects = new();

        private readonly GameObject _content;
        private readonly Task _task;
        private readonly UISettings _settings;

        public TaskFiller(GameObject content, Task task, UISettings settings)
        {

            _content = content;
            _task = task;
            _settings = settings;

            FillInData();
        }

        private void FillInData()
        {
            string type = "- " + _task.Type.ToString();

            string venue = "- " +_settings.Language.Venue + ": " + _task.Venue.ToString();

            TextObj(type);
            TextObj(venue);

            if(_task.Minutes != 0 )
            {
                string duration ="- " + _settings.Language.Duration + ": " + _task.Minutes.ToString() + " " + _settings.Language.Minutes;
                TextObj(duration);
            }
        }

        private void TextObj(string text)
        {
            var textObj = new GameObject("Text") { layer = 5 };

            //Transform
            var transform = textObj.AddComponent<RectTransform>();
            transform.SetParent(_content.transform);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.anchoredPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.pivot = new Vector2(0, 1);

            //TMP Pro
            var tmp = transform.AddComponent<TextMeshProUGUI>();
            tmp.text = text;

            TextObjects.Add(textObj);
        }
    }
}
