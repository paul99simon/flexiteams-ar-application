using FlexiTeams.DataClasses.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.UI.ResourceUI
{
    // TODO: Add checkbox to Skill  
    public class SkillsFiller
    {
        public List<GameObject> TextObjects = new();

        private readonly GameObject _content;
        private readonly Resource _resource;
        private readonly UISettings _settings;

        public SkillsFiller(GameObject content, Resource resource, UISettings settings)
        {

            _content = content;
            _resource = resource;
            _settings = settings;

            FillInData();
        }

        private void FillInData()
        {
            _resource.Skills.ForEach(s =>
            {
                string skill = "- " + s.ToString();
                TextObj(skill);
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
