using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.DataUI
{
    internal class DataUIStyle
    {
        private readonly DataUILayout _layout;
        private readonly DataUIData _data;
        private readonly UISettings _settings;

        public DataUIStyle(DataUILayout layout,DataUIData data, UISettings settings) {

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

            PortraitViewStyle(_layout.IconViewObj);
            PortraitImageStyle(_layout.IconImageObj);

            HeaderStyle(_layout.IconHeaderObj);
            HeaderStyle(_layout.DataHeaderObj);
            HeaderStyle(_layout.TasksHeaderObj);

            HeaderTextStyle(_layout.IconHeaderTextObj);
            HeaderTextStyle(_layout.DataHeaderTextObj);
            HeaderTextStyle(_layout.TasksHeaderTextObj);

            ViewportStyle(_layout.DataViewPortObj);
            ViewportStyle(_layout.TasksViewPortObj);

            ScrollbarVerticalStyle(_layout.DataScrollbarVerticalObj);
            ScrollbarVerticalStyle(_layout.TasksScrollbarVerticalObj);

            HandleStyle(_layout.DataHandleObj);
            HandleStyle(_layout.TasksHandleObj);

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
            var button = _layout.DragButtonObj.GetComponent<Button>();
            button.colors = _settings.TitleBarSettings.ButtonColors;

            button = _layout.LanguageButtonObj.GetComponent<Button>();
            button.colors = _settings.TitleBarSettings.ButtonColors;

            var closeButton = _layout.CloseButtonObj.GetComponent<Button>();
            closeButton.colors = _settings.TitleBarSettings.CloseButtonColors;

            //Image
            var image = _layout.DragButtonImageObj.AddComponent<Image>();
            image.sprite = _settings.TitleBarSettings.DragSprite;

            image = _layout.LanguageButtonImageObj.AddComponent<Image>();
            image.sprite = _settings.TitleBarSettings.LanguageSprite;

            image = _layout.CloseButtonImageObj.AddComponent<Image>();
            image.sprite = _settings.TitleBarSettings.CloseSprite;

            closeButton.image = _layout.CloseButtonImageObj.GetComponent<Image>();
            closeButton.colors = _settings.TitleBarSettings.CloseButtonColors;
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
            portrait.sprite = _settings.DataUISettings.IconSettings.IconSprite;
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
            _data.DataFiller.TextObjects.ForEach(obj => { TextStyle(obj); });
            _data.TasksFiller.TextObjects.ForEach(obj => { TextStyle(obj); });
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
