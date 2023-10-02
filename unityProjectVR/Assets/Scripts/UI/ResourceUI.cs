using FlexiTeams.DataClasses.Resource;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class ResourceUI
{
    private static readonly Vector2 sizeDelta = new(1200, 760);

    public static void Create(Resource resource, Vector3 position, Quaternion rotation, UISettings settings)
    {
        var resourceUI = new GameObject(resource.Id.ToString());
        resourceUI.layer = 5;
        Components(resourceUI, position, rotation, settings);
        TitleBar(resourceUI.transform, settings);
        Window(resourceUI.transform, settings, resource);

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
        layoutGroup.childForceExpandWidth = true;
        layoutGroup.spacing = 0;


        //Canvas
        var canvas = resourceUI.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private static void TitleBar(Transform parent, UISettings settings)
    {
        var titleBar = new GameObject("TitleBar");
        titleBar.layer = 5;

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
        title.layer = 5;

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
        text.layer = 5;

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
        tmp.margin = new Vector4(settings.spacing, 0, 0, 0);
    }

    private static void TitleBarButtons(Transform parent, UISettings settings)
    {
        var buttons = new GameObject("Buttons");
        buttons.layer = 5;

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
        dragButton.layer = 5;
        //Transform
        transform = dragButton.AddComponent<RectTransform>();
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
        Image.layer = 5;
        transform = Image.AddComponent<RectTransform>();
        transform.SetParent(dragButton.transform);
        transform.localScale = Vector3.one;
        transform.anchoredPosition = Vector3.zero;

        var image = Image.AddComponent<Image>();
        image.sprite = settings.TitleBarSettings.DragSprite;

        //Language Button
        var languageButton = new GameObject("LanguageButton");
        languageButton.layer = 5;
        //Transform
        transform = languageButton.AddComponent<RectTransform>();
        transform.SetParent(buttons.transform);
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
        Image.layer = 5;
        transform = Image.AddComponent<RectTransform>();
        transform.SetParent(languageButton.transform);
        transform.localScale = Vector3.one;
        transform.anchoredPosition = Vector3.zero;

        image = Image.AddComponent<Image>();
        image.sprite = settings.TitleBarSettings.LanguageSprite;


        //Close Button
        var closeButton = new GameObject("CloseButton");
        closeButton.layer = 5;
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
        Image.layer = 5;
        transform = Image.AddComponent<RectTransform>();
        transform.SetParent(closeButton.transform);
        transform.localScale = Vector3.one;
        transform.anchoredPosition = Vector3.zero;

        image = Image.AddComponent<Image>();
        image.sprite = settings.TitleBarSettings.CloseSprite;

    }

    private static void Window(Transform parent, UISettings settings, Resource resource)
    {
        var window = new GameObject("Window");
        window.layer = 5;

        //Transform
        var transform = window.AddComponent<RectTransform>();
        transform.SetParent(parent.transform);
        transform.localScale = Vector3.one;

        //LayoutElement
        var layoutElement = window.AddComponent<LayoutElement>();
        layoutElement.flexibleHeight = 1;

        //LayoutGroup
        var layoutGroup = window.AddComponent<VerticalLayoutGroup>();
        layoutGroup.childControlWidth = true;
        layoutGroup.childControlHeight = true;
        layoutGroup.childForceExpandHeight = true;
        layoutGroup.childForceExpandWidth = true;

        int spacing = (int)settings.spacing;
        layoutGroup.padding = new RectOffset(spacing, spacing, spacing, spacing);
        layoutGroup.spacing = spacing;

        //Image
        var image = window.AddComponent<Image>();
        image.color = settings.BackgroundColor;

        TopPanel(transform, settings, resource);
        BottomPanel(transform, settings);

    }

    private static void TopPanel(Transform parent, UISettings settings, Resource resource)
    {
        var TopPanel = new GameObject("TopPanel");
        TopPanel.layer = 5;

        //Transform
        var transform = TopPanel.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //Layout Group
        var layoutGroup = TopPanel.AddComponent<HorizontalLayoutGroup>();
        layoutGroup.childControlHeight = true;
        layoutGroup.childForceExpandHeight = true;
        layoutGroup.childForceExpandWidth = false;
        layoutGroup.spacing = settings.spacing;

        PortraitPanel(transform, settings, resource);
        ProfessionalBackgroundPanel(transform, settings);
    }

    private static void BottomPanel(Transform parent, UISettings settings)
    {
        var bottomPanel = new GameObject("BottomPanel");
        bottomPanel.layer = 5;

        //Transform
        var transform = bottomPanel.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //Layout Group
        var layoutGroup = bottomPanel.AddComponent<HorizontalLayoutGroup>();
        layoutGroup.childControlHeight = true;
        layoutGroup.childControlWidth = true;
        layoutGroup.childForceExpandHeight = true;
        layoutGroup.childForceExpandWidth = true;
        layoutGroup.spacing = settings.spacing;

        PersonalBackgroundPanel(transform, settings);
        SkillPanel(transform, settings);
        TraitPanel(transform, settings);
    }

    private static void PortraitPanel(Transform parent, UISettings settings, Resource resource)
    {
        var portraitPanel = new GameObject("Portrait");
        portraitPanel.layer = 5;

        //Transform
        var transform = portraitPanel.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //LayoutElement
        var layoutElement = portraitPanel.AddComponent<LayoutElement>();
        layoutElement.preferredWidth = 250;

        //LayoutGroup
        var layoutGroup = portraitPanel.AddComponent<VerticalLayoutGroup>();
        layoutGroup.childControlWidth = true;
        layoutGroup.childControlHeight = true;
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.childForceExpandWidth = false;

        //FullName
        List<string> names = new List<string>();

        resource.FirstNames.ForEach(name => names.Add(name.ToString()));

        string fullName = String.Join(" ", names);

        PortraitHeader(transform, fullName, settings);
        Portrait(transform, settings);
    }

    private static void ProfessionalBackgroundPanel(Transform parent, UISettings settings)
    {
        var professionalBackgroundPanel = new GameObject("ProfessionalBackground");
        professionalBackgroundPanel.layer = 5;

        //Transform
        var transform = professionalBackgroundPanel.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //LayoutElement
        var layoutElement = professionalBackgroundPanel.AddComponent<LayoutElement>();
        layoutElement.flexibleWidth = 1;

        //LayoutGroup
        var layoutGroup = professionalBackgroundPanel.AddComponent<VerticalLayoutGroup>();
        layoutGroup.childControlWidth = true;
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.childForceExpandWidth = false;

        HeaderPanel(transform, "Professional Info", settings);
        ScrollView(transform, settings);
    }

    private static void PersonalBackgroundPanel(Transform parent, UISettings settings)
    {
        var personalBackgroundPanel = new GameObject("PersonalBackgroung");
        personalBackgroundPanel.layer = 5;

        //Transform
        var transform = personalBackgroundPanel.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //LayoutGroup
        var layoutGroup = personalBackgroundPanel.AddComponent<VerticalLayoutGroup>();
        layoutGroup.childControlWidth = true;
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.childForceExpandWidth = false;

        HeaderPanel(transform, "Personal Info", settings);
        ScrollView(transform, settings);
    }

    private static void SkillPanel(Transform parent, UISettings settings)
    {
        var skillPanel = new GameObject("Skills");
        skillPanel.layer = 5;

        //Transform
        var transform = skillPanel.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //LayoutGroup
        var layoutGroup = skillPanel.AddComponent<VerticalLayoutGroup>();
        layoutGroup.childControlWidth = true;
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.childForceExpandWidth = false;

        HeaderPanel(transform, "Skills", settings);
        ScrollView(transform, settings);
    }

    private static void TraitPanel(Transform parent, UISettings settings)
    {
        var traitPanel = new GameObject("Traits");
        traitPanel.layer = 5;

        //Transform
        var transform = traitPanel.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //LayoutGroup
        var layoutGroup = traitPanel.AddComponent<VerticalLayoutGroup>();
        layoutGroup.childControlWidth = true;
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.childForceExpandWidth = false;

        HeaderPanel(transform, "Traits", settings);
        ScrollView(transform, settings);

    }

    private static void HeaderPanel(Transform parent, string text, UISettings settings)
    {
        var header = new GameObject("Header");
        header.layer = 5;

        //Transform
        var transform = header.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //LayoutElement
        var layoutElement = header.AddComponent<LayoutElement>();
        layoutElement.preferredHeight = 50;
        layoutElement.flexibleWidth = 1;

        //Image
        var image = header.AddComponent<Image>();
        image.sprite = settings.HeaderSettings.BackgroundSprite;
        image.color = settings.HeaderSettings.BackgroundColor;
        image.type = Image.Type.Sliced;

        Header(transform, text, settings);
    }

    private static void Header(Transform parent, string text, UISettings settings)

    {
        var header = new GameObject("Text");
        header.layer = 5;

        //Transform
        var transform = header.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //TMP
        var tmp = header.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.font = settings.HeaderSettings.TMP_FontAsset;
        tmp.fontStyle = settings.HeaderSettings.FontStyles;
        tmp.fontSize = settings.HeaderSettings.FontSize;
        tmp.verticalAlignment = VerticalAlignmentOptions.Middle;
        tmp.horizontalAlignment = HorizontalAlignmentOptions.Center;
        tmp.enableWordWrapping = false;
        tmp.color = settings.HeaderSettings.TextColor;
    }

    private static void ScrollView(Transform parent, UISettings settings)
    {
        var ScrollView = new GameObject("Scroll View");
        ScrollView.layer = 5;

        //Transform
        var transform = ScrollView.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //LayoutGroup
        var layoutGroup = ScrollView.AddComponent<HorizontalLayoutGroup>();
        layoutGroup.childControlHeight = true;
        layoutGroup.childControlWidth = true;
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.childForceExpandWidth = false;

        //Layout Element
        var layoutElement = ScrollView.AddComponent<LayoutElement>();
        layoutElement.flexibleHeight = 1;
        layoutElement.flexibleWidth = 1;

        Viewport(transform, settings);
        ScrollbarVertical(transform, settings);

        //Scroll Rect
        var scrollRect = ScrollView.AddComponent<ScrollRect>();
        scrollRect.content = transform.Find("Viewport/Content").GetComponent<RectTransform>();
        scrollRect.vertical = true;
        scrollRect.horizontal = false;
        scrollRect.viewport = transform.Find("Viewport").GetComponent<RectTransform>();
        scrollRect.verticalScrollbar = transform.Find("Scrollbar Vertical").GetComponent<Scrollbar>();

    }

    private static void Viewport(Transform parent, UISettings settings)
    {
        var Viewport = new GameObject("Viewport");
        Viewport.layer = 5;

        //Transform
        var transform = Viewport.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
        transform.pivot = new Vector2(0, 1);

        //LayoutElement
        var layoutElement = Viewport.AddComponent<LayoutElement>();
        layoutElement.flexibleHeight = 1;
        layoutElement.flexibleWidth = 1;

        //Image
        var image = Viewport.AddComponent<Image>();
        image.sprite = settings.DataViewSettings.BackgroundSprite;
        image.color = settings.DataViewSettings.BackgroundColor;
        image.type = Image.Type.Sliced;

        //Mask
        var mask = Viewport.AddComponent<Mask>();

        Content(transform);

    }

    private static void ScrollbarVertical(Transform parent, UISettings settings)
    {
        var ScrollbarVertical = new GameObject("Scrollbar Vertical");
        ScrollbarVertical.layer = 5;

        //Transform
        var transform = ScrollbarVertical.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //Image
        var image = ScrollbarVertical.AddComponent<Image>();
        image.sprite = settings.DataViewSettings.ScrollbarSprite;
        image.color = settings.DataViewSettings.ScrollbarColor;
        image.type = Image.Type.Sliced;


        //Layout Element
        var layoutElement = ScrollbarVertical.AddComponent<LayoutElement>();
        layoutElement.flexibleHeight = 1;
        layoutElement.preferredWidth = 20;

        SlidingArea(transform, settings);

        //Scrollbar
        var scrollbar = ScrollbarVertical.AddComponent<Scrollbar>();
        scrollbar.targetGraphic = transform.Find("Sliding Area/Handle").GetComponent<Image>();
        scrollbar.handleRect = transform.Find("Sliding Area/Handle").GetComponent<RectTransform>();
        scrollbar.direction = Scrollbar.Direction.TopToBottom;
        scrollbar.size = 0.2f;
        scrollbar.value = 0;


        var slidingAreaTransform = transform.Find("Sliding Area").GetComponent<RectTransform>();
        var handleTranform = transform.Find("Sliding Area/Handle").GetComponent<RectTransform>();

        slidingAreaTransform.anchoredPosition = new Vector3(10, 10, 0);
        slidingAreaTransform.sizeDelta = new Vector2(10, 10);

        handleTranform.anchoredPosition = new Vector3(-10, -10, 0);
        handleTranform.sizeDelta = new Vector2(-10, -10);

    }

    private static void Content(Transform parent)
    {
        var Content = new GameObject("Content");
        Content.layer = 5;

        //Transform
        var transform = Content.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //Content Size Fitter
        var contentSizeFitter = Content.GetComponent<ContentSizeFitter>();

        //Layout Group
        var layoutGroup = Content.AddComponent<VerticalLayoutGroup>();
    }

    private static void SlidingArea(Transform parent, UISettings settings)
    {
        var SlidingArea = new GameObject("Sliding Area");
        SlidingArea.layer = 5;

        //Transform
        var transform = SlidingArea.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
        transform.anchorMin = Vector2.zero;
        transform.anchorMax = Vector2.one;

        Handle(transform, settings);
    }

    private static void Handle(Transform parent, UISettings settings)
    {
        var Handle = new GameObject("Handle");
        Handle.layer = 5;

        //Transform
        var transform = Handle.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //Image
        var image = Handle.AddComponent<Image>();
        image.sprite = settings.DataViewSettings.HandleSprite;
        image.color = settings.DataViewSettings.HandleColor;
        image.type = Image.Type.Sliced;
    }

    private static void PortraitHeader(Transform parent, string text, UISettings settings)
    {
        var header = new GameObject("Header");
        header.layer = 5;

        //Transform
        var transform = header.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //LayoutElement
        var layoutElement = header.AddComponent<LayoutElement>();
        layoutElement.preferredHeight = 50;
        layoutElement.preferredWidth = 250;

        //Image
        var image = header.AddComponent<Image>();
        image.sprite = settings.HeaderSettings.BackgroundSprite;
        image.color = settings.HeaderSettings.BackgroundColor;
        image.type = Image.Type.Sliced;

        Header(transform, text, settings);
    }

    private static void Portrait(Transform parent, UISettings settings){

        var portrait = new GameObject("View");
        portrait.layer = 5;

        //Transform
        var  transform = portrait.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;

        //Layout Element
        var layoutELement = portrait.AddComponent<LayoutElement>();
        layoutELement.flexibleHeight = 1;
        layoutELement.preferredWidth = 250;

        //Image
        var image = portrait.AddComponent<Image>();
        image.sprite = settings.DataViewSettings.BackgroundSprite;
        image.color = settings.DataViewSettings.BackgroundColor;
        image.type = Image.Type.Sliced;

        var ImageObj = new GameObject("Image");

        //Transform
        transform = ImageObj.AddComponent<RectTransform>();
        transform.SetParent(portrait.transform);
        transform.localScale = Vector3.one;
        transform.anchoredPosition = Vector3.zero;
        transform.sizeDelta = new Vector2(200, 200);

        //image
        image = ImageObj.AddComponent<Image>();
        image.sprite = settings.DataViewSettings.PortraitSprite;
        image.color = Color.black;
    }
}