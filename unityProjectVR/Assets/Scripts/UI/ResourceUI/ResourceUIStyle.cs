using Assets.Scripts.UI.Common;
using FlexiTeams.DataClasses.Resource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.ResourceUI
{
    internal class ResourceUIStyle
    {
        private readonly ResourceUILayout _layout;
        private readonly ResourceUIData _data;
        private readonly UISettings _settings;
        private readonly Resource _resource;

        public ResourceUIStyle(ResourceUILayout layout,ResourceUIData data, Resource resource, UISettings settings) {

            _layout = layout;
            _data = data;
            _resource = resource;
            _settings = settings;
            Style();
        }

        private void Style()
        {
            TitleBarStyle();
            TitleBarTextStyle();
            TitleBarButtonsStyle();

            WindowStyle();

            PortraitViewStyle(_layout.PortraitViewObj);
            PortraitImageStyle(_layout.PortraitImageObj);

            HeaderStyle(_layout.PortraitHeaderObj);
            HeaderStyle(_layout.PersonalInfoHeaderObj);
            HeaderStyle(_layout.ProfessionalInfoHeaderObj);
            HeaderStyle(_layout.SkillsHeaderObj);
            HeaderStyle(_layout.TraitsHeaderObj);

            HeaderTextStyle(_layout.PortraitHeaderTextObj);
            HeaderTextStyle(_layout.PersonalInfoHeaderTextObj);
            HeaderTextStyle(_layout.ProfessionalInfoHeaderTextObj);
            HeaderTextStyle(_layout.SkillsHeaderTextObj);
            HeaderTextStyle(_layout.TraitsHeaderTextObj);

            ViewportStyle(_layout.PersonalInfoViewPortObj);
            ViewportStyle(_layout.ProfessionalInfoViewPortObj);
            ViewportStyle(_layout.SkillsViewPortObj);
            ViewportStyle(_layout.TraitsViewPortObj);

            ScrollbarVerticalStyle(_layout.PersonalInfoScrollbarVerticalObj);
            ScrollbarVerticalStyle(_layout.ProfessionalInfoScrollbarVerticalObj);
            ScrollbarVerticalStyle(_layout.SkillsScrollbarVerticalObj);
            ScrollbarVerticalStyle(_layout.TraitsScrollbarVerticalObj);

            HandleStyle(_layout.PersonalInfoHandleObj);
            HandleStyle(_layout.ProfessionalInfoHandleObj);
            HandleStyle(_layout.SkillsHandleObj);
            HandleStyle(_layout.TraitsHandleObj);

            DataStyle();
        }

        private void TitleBarStyle()
        {
            var image = _layout.TitleBarObj.AddComponent<Image>();
            image.color = _settings.TitleBarSettings.BackgroundColor;
        }

        private void TitleBarTextStyle()
        {
            var tmp = _layout.TitleBarTextObj.GetComponent<TextMeshProUGUI>();
            tmp.font = _settings.TMP_FontAsset;
            tmp.color = _settings.TitleBarSettings.TextColor;
            tmp.fontSize = _settings.TitleBarSettings.FontSize;
            tmp.fontStyle = _settings.FontStyle;
            tmp.horizontalAlignment = HorizontalAlignmentOptions.Left;
            tmp.verticalAlignment = VerticalAlignmentOptions.Middle;
            tmp.overflowMode = TextOverflowModes.Truncate;
            tmp.margin = new Vector4(_settings.ResourceUISettings.Spacing, 0, 0, 0);
        }

        private void TitleBarButtonsStyle()
        {
            //Colors
            var dragButton = _layout.DragButtonObj.GetComponent<Button>();
            dragButton.colors = _settings.TitleBarSettings.ButtonColors;

            var languageButton = _layout.LanguageButtonObj.GetComponent<Button>();
            languageButton.colors = _settings.TitleBarSettings.ButtonColors;

            var closeButton = _layout.CloseButtonObj.GetComponent<CloseButton>();
            closeButton.colors = _settings.TitleBarSettings.CloseButtonColors;

            //Image
            var image = _layout.DragButtonImageObj.AddComponent<Image>();
            image.sprite = _settings.TitleBarSettings.DragSprite;

            image = _layout.LanguageButtonImageObj.AddComponent<Image>();
            image.sprite = _settings.TitleBarSettings.LanguageSprite;

            image = _layout.CloseButtonImageObj.AddComponent<Image>();
            image.sprite = _settings.TitleBarSettings.CloseSprite;

            closeButton.image = _layout.CloseButtonImageObj.GetComponent<Image>();
        }

        private void WindowStyle()
        {
            //Image
            var image = _layout.WindowObj.AddComponent<Image>();
            image.color = _settings.BackgroundColor;
        }

        private void PortraitViewStyle(GameObject view)
        {
            //Image
            var image = view.AddComponent<Image>();
            image.sprite = _settings.BackgroundSprite;
            image.color = _settings.BackgroundColor;
            image.type = Image.Type.Sliced;
        }

        private void PortraitImageStyle(GameObject image)
        {
            //image
            var portrait = image.AddComponent<Image>();
            portrait.sprite = Resources.Load<Sprite>(_resource.Photos[0].Path);
            portrait.type = Image.Type.Sliced;
        }

        private void HeaderStyle(GameObject header)
        {
            //Image
            var image = header.AddComponent<Image>();
            image.sprite = _settings.HeaderSettings.BackgroundSprite;
            image.color = _settings.HeaderSettings.BackgroundColor;
            image.type = Image.Type.Sliced;
        }

        private void HeaderTextStyle(GameObject headerText)
        {
            //TMP
            var tmp = headerText.GetComponent<TextMeshProUGUI>();
            tmp.font = _settings.TMP_FontAsset;
            tmp.fontStyle = _settings.HeaderSettings.FontStyles;
            tmp.fontSize = _settings.HeaderSettings.FontSize;
            tmp.verticalAlignment = VerticalAlignmentOptions.Middle;
            tmp.horizontalAlignment = HorizontalAlignmentOptions.Center;
            tmp.enableWordWrapping = false;
            tmp.color = _settings.HeaderSettings.TextColor;
        }

        private void ViewportStyle(GameObject viewport)
        {
            //Image
            var image = viewport.AddComponent<Image>();
            image.sprite = _settings.BackgroundSprite;
            image.color = _settings.BackgroundColor;
            image.type = Image.Type.Sliced;
        }

        private void ScrollbarVerticalStyle(GameObject scrollbarVertical)
        {
            //Image
            var image = scrollbarVertical.AddComponent<Image>();
            image.sprite = _settings.ScrollbarSprite;
            image.color = _settings.ScrollbarColor;
            image.type = Image.Type.Sliced;
        }

        private void HandleStyle(GameObject handle)
        {
            //Image
            var image = handle.AddComponent<Image>();
            image.sprite = _settings.HandleSprite;
            image.color = _settings.HandleColor;
            image.type = Image.Type.Sliced;
        }

        private void DataStyle()
        {
            _data.PersonalInfoFiller.TextObjects.ForEach(obj => { TextStyle(obj); });
            _data.ProfessionalInfoFiller.TextObjects.ForEach(obj => { TextStyle(obj); });
            _data.SkillsFiller.TextObjects.ForEach(obj => { TextStyle(obj); });
            _data.TraitsFiller.TextObjects.ForEach(obj => { TextStyle(obj); });
        }

        private void TextStyle(GameObject textObj)
        {
            //TMP Pro
            var tmp = textObj.transform.GetComponent<TextMeshProUGUI>();
            tmp.color = _settings.TextColor;
            tmp.font = _settings.TMP_FontAsset;
            tmp.fontStyle = _settings.FontStyle;
            tmp.fontSize = _settings.FontSize;

            tmp.verticalAlignment = VerticalAlignmentOptions.Middle;
            tmp.horizontalAlignment = HorizontalAlignmentOptions.Left;
            tmp.enableWordWrapping = true;
            tmp.overflowMode = TextOverflowModes.Truncate;
        }
    }
}
