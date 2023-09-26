using FlexiTeams.DataClasses.Resource;
using Newtonsoft.Json.Bson;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class ResourceUI
{
    private static readonly Vector2 sizeDelta = new Vector2(1200, 800);

    public static void Create(Resource resource, Vector3 position, Quaternion rotation, UISettings settings)
    {
        var resourceUI = new GameObject(resource.Id.ToString());
        Components(resourceUI, position, rotation, settings);
        TitleBar(resourceUI.transform, settings);

    }

    private static void Components(GameObject resourceUI, Vector3 position, Quaternion rotation, UISettings settings)
    {
        //Rect Transform
        var rectTransform = resourceUI.AddComponent<RectTransform>();
        rectTransform.SetPositionAndRotation(position, rotation);
        rectTransform.sizeDelta = sizeDelta;
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

        //Vertical Layout Group
        var layoutGroup = resourceUI.AddComponent<VerticalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.childForceExpandWidth = false;
        layoutGroup.spacing = settings.spacing;
        

        //Canvas
        var canvas = resourceUI.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        //Image
        var image = resourceUI.AddComponent<Image>();
        image.color = settings.BackgroundColor;

    }

    private static void TitleBar(Transform parent, UISettings settings)
    {
        GameObject titleBar = new GameObject("TitleBar");

        //Rect Transform
        var transform = titleBar.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.pivot = new Vector2(0, 1);
        transform.localScale = Vector3.one;

        //Image
        var image = titleBar.AddComponent<Image>();
        image.color = settings.TitleBarSettings.BackgroundColor;

        //LayoutGroup
        var layoutGroup = titleBar.AddComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.childForceExpandWidth = false;

        //LayoutElement
        var layoutElement = titleBar.AddComponent<LayoutElement>();
        layoutElement.preferredHeight = 100;

        TitleBarTitle(transform, settings);
        TitleBarButtons(transform, settings);

    }

    private static void TitleBarTitle(Transform parent, UISettings settings)
    {
        var title = new GameObject("Title");

        //Rect Transform
        var transform = title.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
        transform.pivot = new Vector2(0, 1);

        //Layout Element
        var layoutElement = title.AddComponent<LayoutElement>();
        layoutElement.flexibleWidth = 1;


        TitleBarText(transform, settings);

    }

    private static void TitleBarText(Transform parent, UISettings settings)
    {
        var text = new GameObject("Text");

        //Rect Tranform
        var transform = text.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
        transform.pivot = new Vector2(0, 1);
        transform.anchorMin = new Vector2(0, 1);
        transform.anchorMax = new Vector2(0, 1);
        transform.anchoredPosition = Vector3.zero;

        //Layout Element
        var layoutElement = text.AddComponent<LayoutElement>();
        layoutElement.preferredHeight = 100;
        layoutElement.flexibleWidth = 1;

        //Content Size Fitter
        var contentSizeFitter = text.AddComponent<ContentSizeFitter>();
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        //TMP
        var tmp = text.AddComponent<TextMeshProUGUI>();
        tmp.font = settings.TitleBarSettings.TMP_FontAsset;
        tmp.text = "Resource";
        tmp.color = settings.TitleBarSettings.TextColor;
        tmp.fontSize = settings.TitleBarSettings.FontSize;
        tmp.fontStyle = settings.TitleBarSettings.FontStyle;
        tmp.horizontalAlignment = HorizontalAlignmentOptions.Left;
        tmp.verticalAlignment = VerticalAlignmentOptions.Middle;
        tmp.overflowMode = TextOverflowModes.Truncate;
        tmp.margin = new Vector4(settings.spacing, 0, 0, 0 );
    }

    private static void TitleBarButtons(Transform parent, UISettings settings)
    {
        var buttons = new GameObject("Buttons");

        //Rect Tranform
        var transform = buttons.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //Canvas
        buttons.AddComponent<Canvas>();

        //Layout ELement
        var layoutElement = buttons.AddComponent<LayoutElement>();
        layoutElement.preferredWidth = 300;

        //Layout Group
        var layoutGroup = buttons.AddComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandWidth = false;
        layoutGroup.childForceExpandHeight = false;

        //Tracked Device Graphics Raycaster
        buttons.AddComponent<TrackedDeviceGraphicRaycaster>();

        //Buttons

        //Drag Button
        var dragButton = new GameObject("DragButton");
            //Transform
            transform =  dragButton.AddComponent<RectTransform>();
            transform.SetParent(buttons.transform);
            transform.localScale = Vector3.one;
            //Button
            var button = dragButton.AddComponent<Button>();
            button.colors = settings.TitleBarSettings.NormalButtonColors;
            //Layout Element
            layoutElement = dragButton.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 100;
            layoutElement.preferredHeight = 100;

            //Image
            var Image = new GameObject("Image");
            transform = Image.AddComponent<RectTransform>();
            transform.SetParent(dragButton.transform);
            transform.localScale = Vector3.one;
            transform.anchoredPosition = Vector3.zero;

            var image = Image.AddComponent<Image>();
            image.sprite = settings.TitleBarSettings.DragSprite;

        //Language Button
        var languageButton = new GameObject("LanguageButton");
            //Transform
            transform = languageButton.AddComponent<RectTransform>();
            transform.SetParent (buttons.transform);
            transform.localScale = Vector3.one;
            //Button
            button = languageButton.AddComponent<Button>();
            button.colors = settings.TitleBarSettings.NormalButtonColors;
            //Layout Element
            layoutElement = languageButton.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 100;
            layoutElement.preferredHeight = 100;

            //Image
            Image = new GameObject("Image");
            transform = Image.AddComponent<RectTransform>();
            transform.SetParent(languageButton.transform);
            transform.localScale = Vector3.one;
            transform.anchoredPosition = Vector3.zero;

            image = Image.AddComponent<Image>();
            image.sprite = settings.TitleBarSettings.LanguageSprite;


        //Close Button
        var closeButton = new GameObject("CloseButton");
            //Transform
            transform = closeButton.AddComponent<RectTransform>();
            transform.SetParent(buttons.transform);
            transform.localScale = Vector3.one;
            //Button
            button = closeButton.AddComponent<Button>();
            button.colors = settings.TitleBarSettings.CloseButtonColors;
            //Layout Element
            layoutElement = closeButton.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 100;
            layoutElement.preferredHeight = 100;

            //Image
            Image = new GameObject("Image");
            transform = Image.AddComponent<RectTransform>();
            transform.SetParent(closeButton.transform);
            transform.localScale = Vector3.one;
            transform.anchoredPosition = Vector3.zero;

            image = Image.AddComponent<Image>();
            image.sprite = settings.TitleBarSettings.CloseSprite;

    }

}