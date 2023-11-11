using FlexiTeams.DataClasses.Resource;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.UI.ResourceUI
{
    internal class PersonalInfoFiller
    {
        public List<GameObject> TextObjects = new();

        private readonly GameObject _content;
        private readonly Resource _resource;
        private readonly UISettings _settings;


        public PersonalInfoFiller(GameObject content, Resource resource, UISettings settings) {
            
            _content = content;
            _resource = resource;
            _settings = settings;

            FillInData();
        }

        private void FillInData()
        {
            //FullName
            List<string> names = new();

            if (_resource.Prefix != null) names.Add(_resource.Prefix.ToString());
            _resource.FirstNames.ForEach(name => names.Add(name.ToString()));
            _resource.LastNames.ForEach(name => names.Add(name.ToString()));
            string fullName = string.Join(" ", names);
            fullName = "- " + fullName;

            //Age
            string age = _resource.Age.Years.ToString();
            age = "- " + age + " " + _settings.Language.Years;

            //marital State
            string maritalState = _resource.MaritalState.ToString();
            maritalState = "- " + maritalState;

            TextObj(fullName);
            TextObj(age);
            TextObj(maritalState);

            if (_resource.Children != null)
            {
                string children = "- " + _settings.Language.ResourceUI.Children + ": ";

                _resource.Children.ForEach(child =>
                {
                    children += child.Age + " " + _settings.Language.Years + " / ";
                });

                children = children[..^3];

                TextObj(children);
            }

            _resource.Stressors?.ForEach(stressor =>
            {
                string temp = "- " + stressor.ToString();
                TextObj(temp);
            });

            _resource.PersonalInfos?.ForEach(personalInfo =>
            {
                string temp = "- " + personalInfo.ToString();
                TextObj(temp);
            });
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
