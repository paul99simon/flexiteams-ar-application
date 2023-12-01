using Assets.Scripts.UI.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.TaskUI
{
    internal class TaskUIStyle
    {
        private readonly TaskUILayout _layout;
        private readonly TaskUIData _data;
        private readonly UISettings _settings;

        public TaskUIStyle(TaskUILayout layout,TaskUIData data, UISettings settings) {

            _layout = layout;
            _data = data;
            _settings = settings;
            Style();
        }

        private void Style()
        {
            TitleBarStyle();
            TitleBarTextStyle();
            TitleBarButtonsStyle();

            WindowStyle();

            IconViewStyle(_layout.IconViewObj);
            IconImageStyle(_layout.IconImageObj);

            HeaderStyle(_layout.IconHeaderObj);
            HeaderStyle(_layout.InfoHeaderObj);
            HeaderStyle(_layout.AssignedResourcesHeaderObj);
            HeaderStyle(_layout.AssignedDataHeaderObj);

            HeaderTextStyle(_layout.IconHeaderTextObj);
            HeaderTextStyle(_layout.InfoHeaderTextObj);
            HeaderTextStyle(_layout.AssignedResourcesHeaderTextObj);
            HeaderTextStyle(_layout.AssignedDataHeaderTextObj);

            ViewportStyle(_layout.InfoViewPortObj);
            ViewportStyle(_layout.AssignedResourcesViewPortObj);
            ViewportStyle(_layout.AssignedDataViewPortObj);

            ScrollbarVerticalStyle(_layout.InfoScrollbarVerticalObj);
            ScrollbarVerticalStyle(_layout.AssignedResourcesScrollbarVerticalObj);
            ScrollbarVerticalStyle(_layout.AssignedDataScrollbarVerticalObj);

            HandleStyle(_layout.InfoHandleObj);
            HandleStyle(_layout.AssignedResourcesHandleObj);
            HandleStyle(_layout.AssignedDataHandleObj);

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
            var dragButton = _layout.DragButtonObj.GetComponent<DragButton>();

            var button = _layout.LanguageButtonObj.GetComponent<Button>();
            button.colors = _settings.TitleBarSettings.ButtonColors;

            var closeButton = _layout.CloseButtonObj.GetComponent<CloseButton>();

            //Image
            var image = _layout.DragButtonObj.AddComponent<Image>();
            image = _layout.DragButtonImageObj.AddComponent<Image>();
            image.sprite = _settings.TitleBarSettings.DragSprite;

            image = _layout.LanguageButtonImageObj.AddComponent<Image>();
            image.sprite = _settings.TitleBarSettings.LanguageSprite;

            image = _layout.CloseButtonImageObj.AddComponent<Image>();
            image.sprite = _settings.TitleBarSettings.CloseSprite;

            dragButton.image = _layout.DragButtonObj.GetComponent<Image>();
            dragButton.colors = _settings.TitleBarSettings.ButtonColors;

            closeButton.image = _layout.CloseButtonImageObj.GetComponent<Image>();
            closeButton.colors = _settings.TitleBarSettings.CloseButtonColors;
        }

        private void WindowStyle()
        {
            //Image
            var image = _layout.WindowObj.AddComponent<Image>();
            image.color = _settings.BackgroundColor;
        }

        private void IconViewStyle(GameObject view)
        {
            //Image
            var image = view.AddComponent<Image>();
            image.sprite = _settings.BackgroundSprite;
            image.color = _settings.BackgroundColor;
            image.type = Image.Type.Sliced;
        }

        private void IconImageStyle(GameObject image)
        {
            //image
            var portrait = image.AddComponent<Image>();
            portrait.sprite = _settings.TaskUISettings.IconSettings.IconSprite;
            portrait.color = Color.black;
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
            _data.TaskFiller.TextObjects.ForEach(obj => { TextStyle(obj); });
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
