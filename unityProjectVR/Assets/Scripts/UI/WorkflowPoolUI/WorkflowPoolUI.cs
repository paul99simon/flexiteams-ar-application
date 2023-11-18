using Assets.Scripts.Application;
using FlexiTeams.DataClasses.Workflow;
using FlexiTeams.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class WorkflowPoolUI : MonoBehaviour
{

    private VR_AR_Application appplication;
    private Transform content;
    private UISettings settings;
    private WorkflowPool _pool;

    // Start is called before the first frame update
    void Start()
    {
        appplication = GameObject.Find("Application").GetComponent<VR_AR_Application>();
        settings = appplication.Settings;
        content = GameObject.Find("WorkflowPoolUI").transform.Find("Panel_List/Scroll View/Viewport/Content");
        _pool = appplication.WorkflowPool;
        _pool.List.ForEach(workflow => AddWorkflowButtonObject(workflow));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddWorkflowButtonObject(Workflow workflow)
    {
        var workflowObject = new GameObject("Workflow");
        SetComponents(workflowObject, workflow);
        SetChildren(workflowObject.transform, workflow);
    }

    private void SetComponents(GameObject workflowObj, Workflow workflow)
    {
        //Rect Transform
        var transform = workflowObj.AddComponent<RectTransform>();
        transform.SetParent(content.transform, false);
        transform.sizeDelta = new Vector2(100, 50);

        //UILayer
        workflowObj.layer = 5;

        //Horizontal Layout Group
        HorizontalLayoutGroup group = workflowObj.AddComponent<HorizontalLayoutGroup>();
        group.childAlignment = TextAnchor.UpperLeft;
        group.childControlHeight = true;
        group.childControlWidth = true;
        group.childForceExpandHeight = false;
        group.childForceExpandWidth = false;

        //Layout Element
        LayoutElement layoutElement = workflowObj.AddComponent<LayoutElement>();
        layoutElement.flexibleWidth = 1;

        //Image
        Image image = workflowObj.AddComponent<Image>();
        image.sprite = settings.BackgroundSprite;
        image.type = Image.Type.Sliced;

        //ButtonComponent
        var button = workflowObj.AddComponent<WorkflowButton>();
        button.Id = workflow.Id;
        button.interactable = true;
        button.image = image;
        button.transition = Selectable.Transition.ColorTint;
        var navigation = new Navigation
        {
            mode = Navigation.Mode.Automatic
        };
        button.navigation = navigation;
    }

    private void SetChildren(Transform parent, Workflow workflow)
    {

        var typeObj = new GameObject("Type");
        var venueObj = new GameObject("Venue");
        var durationObj = new GameObject("Duration");
        var buttonsObj = new GameObject("Buttons");
        
        SetButtonsComponents(parent, buttonsObj);
        SetChildrenComponents(parent, typeObj);
        SetChildrenComponents(parent, venueObj);
        SetChildrenComponents(parent, durationObj);

        var typeTextObj = new GameObject("Text");
        var venueTextObj = new GameObject("Text");
        var durationTextObj = new GameObject("Text");

        SetTextComponents(typeObj.transform, typeTextObj, workflow.Type.ToString());
        SetTextComponents(venueObj.transform, venueTextObj, workflow.Venue.ToString());
        SetTextComponents(durationObj.transform, durationTextObj, workflow.Minutes.ToString());

        var visibilityButtonObj = new GameObject("Button");
        var deleteButtonObj = new GameObject("Button");

        SetButtonComponents(buttonsObj.transform, visibilityButtonObj);
        SetButtonComponents(buttonsObj.transform, deleteButtonObj);

        var visibilityImageObj = new GameObject("Image");
        var deleteImageObj = new GameObject("Image");
        
        SetButtonImageComponents(visibilityButtonObj.transform, visibilityImageObj, settings.WorkflowPoolUISettings.VisibilityOnSprite);
        SetButtonImageComponents(deleteButtonObj.transform, deleteImageObj, settings.WorkflowPoolUISettings.DeleteSprite);
    }

    private void SetChildrenComponents(Transform parent, GameObject obj)
    {
        //Rect Transform
        var transform = obj.AddComponent<RectTransform>();
        transform.SetParent(parent, false);

        //UILayer
        obj.layer = 5;

        //Layout Element
        LayoutElement layoutElement = obj.AddComponent<LayoutElement>();
        layoutElement.flexibleWidth = 1;

        //Layout Group
        //var layoutGroup = obj.AddComponent<HorizontalLayoutGroup>();
    }

    private void SetButtonsComponents(Transform parent, GameObject buttonsObj)
    {
        //Rect Transform
        var transform = buttonsObj.AddComponent<RectTransform>();
        transform.SetParent(parent, false);

        //UILayer
        buttonsObj.layer = 5;

        //Layout Group
        var layoutGroup = buttonsObj.AddComponent<HorizontalLayoutGroup>();
        layoutGroup.childControlHeight = true;
        layoutGroup.childControlWidth = true;
        layoutGroup.childForceExpandHeight = true;
        layoutGroup.childForceExpandWidth = false;

        //Layout Element
        var layoutElement = buttonsObj.AddComponent<LayoutElement>();
        layoutElement.preferredWidth = 120;
    }

    private void SetTextComponents(Transform parent, GameObject textObj, string text)
    {
        //Rect Transform
        var transform = textObj.AddComponent<RectTransform>();
        transform.SetParent(parent, false);
        transform.anchorMin = new Vector2(0, 1);
        transform.anchorMax = new Vector2(0, 1);
        transform.pivot = new Vector2(0, 1);
        transform.localScale = Vector3.one;
        transform.anchoredPosition = new Vector2(0, 0);

        //Layout Element
        var layoutElement = textObj.AddComponent<LayoutElement>();
        layoutElement.preferredHeight = 50;
        layoutElement.flexibleWidth = 1;

        //Content Size Fitter
        var contentSizeFitter = textObj.AddComponent<ContentSizeFitter>();
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

        //TextMeshPro
        var textMeshPro = textObj.AddComponent<TextMeshProUGUI>();
        textMeshPro.horizontalAlignment = HorizontalAlignmentOptions.Left;
        textMeshPro.verticalAlignment = VerticalAlignmentOptions.Middle;
        textMeshPro.text = text;
        textMeshPro.fontSize = 24;
        textMeshPro.enableWordWrapping = false;
        textMeshPro.overflowMode = TextOverflowModes.Truncate;
        textMeshPro.color = Color.black;
    }

    private void SetButtonComponents(Transform parent, GameObject buttonObj)
    {
        //Transform
        var transform = buttonObj.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
        transform.sizeDelta = new Vector2(50, 50);

        //Layout Element
        var layoutElement = buttonObj.AddComponent<LayoutElement>();
        layoutElement.preferredWidth = 50;

        //Raycaster
        var rayCaster = buttonObj.AddComponent<TrackedDeviceGraphicRaycaster>();

        //Image
        var image = buttonObj.AddComponent<Image>();
        image.sprite = settings.BackgroundSprite;

        //Button
        var button = buttonObj.AddComponent<Button>();
        button.image = image;
    }

    private void SetButtonImageComponents(Transform parent, GameObject imageObj, Sprite sprite)
    {
        var transform = imageObj.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        transform.localScale = Vector3.one;
        transform.sizeDelta = new Vector2(40, 40);

        var image = imageObj.AddComponent<Image>();
        image.sprite = sprite;
        image.color = Color.black;
    }
}