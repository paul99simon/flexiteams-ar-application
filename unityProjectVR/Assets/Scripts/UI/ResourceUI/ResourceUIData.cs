using Assets.Scripts.UI.ResourceUI.ContentFiller;
using FlexiTeams.DataClasses.Resource;
using System.Collections.Generic;
using TMPro;

namespace Assets.Scripts.UI.ResourceUI
{
    internal class ResourceUIData
    {
        private readonly ResourceUILayout _layout;
        private readonly Resource _resource;
        private readonly UISettings _settings;


        public PersonalInfoFiller PersonalInfoFiller;
        public ProfessionalInfoFiller ProfessionalInfoFiller;
        public SkillsFiller SkillsFiller;
        public TraitsFiller TraitsFiller;

        public ResourceUIData(ResourceUILayout layout, Resource resource, UISettings settings)
        {
            _layout = layout;
            _resource = resource;
            _settings = settings;
            FillInData();
        }

        private void FillInData()
        {
            FillInTitleBar();
            FillInHeader();

            PersonalInfoFiller = new(_layout.PersonalInfoContenObj, _resource, _settings);
            ProfessionalInfoFiller = new(_layout.ProfessionalInfoContenObj, _resource, _settings);
            SkillsFiller = new(_layout.SkillsContenObj, _resource, _settings);
            TraitsFiller = new(_layout.TraitsContenObj, _resource, _settings);
        }

        private void FillInTitleBar()
        {
            var tmp = _layout.TitleBarTextObj.AddComponent<TextMeshProUGUI>();
            tmp.text = _settings.Language.ResourceUI.Title;
        }

        private void FillInHeader()
        {
            var tmp = _layout.PortraitHeaderTextObj.AddComponent<TextMeshProUGUI>();

            //FullName
            List<string> names = new();
            _resource.FirstNames.ForEach(name => names.Add(name.ToString()));
            string fullName = string.Join(" ", names);
            tmp.text = fullName;

            tmp = _layout.PersonalInfoHeaderTextObj.AddComponent<TextMeshProUGUI>();
            tmp.text = _settings.Language.ResourceUI.PersonalInfo;

            tmp = _layout.ProfessionalInfoHeaderTextObj.AddComponent<TextMeshProUGUI>();
            tmp.text = _settings.Language.ResourceUI.ProfessionalInfo;

            tmp = _layout.SkillsHeaderTextObj.AddComponent<TextMeshProUGUI>();
            tmp.text = _settings.Language.ResourceUI.Skills;

            tmp = _layout.TraitsHeaderTextObj.AddComponent<TextMeshProUGUI>();
            tmp.text = _settings.Language.ResourceUI.Traits;
        }


    }
}
