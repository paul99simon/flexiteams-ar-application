using Assets.Scripts.Application;
using FlexiTeams;
using FlexiTeams.ConstructionClasses.Director;
using FlexiTeams.DataClasses.Resource;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePoolUI : MonoBehaviour
{
    private VR_AR_Application application;
    private Transform content;
    private UISettings settings;
    private ResourcePool _pool;

    // Start is called before the first frame update
    void Start()
    {
        application = GameObject.Find("Application").GetComponent<VR_AR_Application>();
        settings = application.Settings;
        content = GameObject.Find("ResourcePoolUI").transform.Find("Panel_List/Scroll View/Viewport/Content");
        _pool = application.ResourcePool;
        _pool.List.ForEach(resource => AddResourceButtonObject(resource));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddResourceButtonObject(Resource resource)
    {
        var resourceObject = new GameObject("Resource");
        SetButtonObjectComponents();
        SetChildren();
        
        void SetButtonObjectComponents(){
            
            //Rect Transform
            var transform = resourceObject.AddComponent<RectTransform>();
            transform.SetParent(content, false);
            transform.sizeDelta = new Vector2(100, 50);

            //UILayer
            resourceObject.layer = 5;

            //Horizontal Layout Group
            HorizontalLayoutGroup group = resourceObject.AddComponent<HorizontalLayoutGroup>();
            group.padding.left = 20;
            group.childAlignment = TextAnchor.UpperLeft;
            group.childControlHeight = true;
            group.childControlWidth = true;
            group.childForceExpandHeight = true;
            group.childForceExpandWidth = false;

            //Layout Element
            LayoutElement layoutElement = resourceObject.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1;

            //Image
            Image image = resourceObject.AddComponent<Image>();
            image.sprite = settings.BackgroundSprite;
            image.type = Image.Type.Sliced;

            //ButtonComponent
            var button = resourceObject.AddComponent<ResourceButton>();
            button.Id = resource.Id;
            button.application = application;
            button.interactable = true;
            button.image = image;
            button.transition = Selectable.Transition.ColorTint;
            var navigation = new Navigation
            {
                mode = Navigation.Mode.Automatic
            };
            button.colors = settings.ButtonColors;
            button.navigation = navigation;
            button.onClick.AddListener(button.OnClick);
        }

        void SetChildren(){
            
            GameObject nameObject = new GameObject("Name");
            GameObject roleObject = new GameObject("Role");

            GameObject nameImageObject = new GameObject("Image");
            GameObject nameTextObject = new GameObject("Text");
            GameObject roleImageObject = new GameObject("Role");
            GameObject roleTextObject = new GameObject("Text");

            SetNameObjectComponents();
            SetRoleObjectComponents();

            SetNameImageObjectComponents();
            SetNameTextObjectComponents();
            SetRoleImageObjectComponents();
            SetRoleTextObjectComponents();

            void SetNameObjectComponents()
            {
                //Rect Transform
                var transform = nameObject.AddComponent<RectTransform>();
                transform.SetParent(resourceObject.transform, false);

                //UILayer
                nameObject.layer = 5;

                //Layout Element
                LayoutElement layoutElement = nameObject.AddComponent<LayoutElement>();
                layoutElement.flexibleWidth = 1;
            }

            void SetRoleObjectComponents()
            {
                //Rect Transform
                var transform = roleObject.AddComponent<RectTransform>();
                transform.SetParent(resourceObject.transform, false);

                //UILayer
                roleObject.layer = 5;

                LayoutElement layoutElement = roleObject.AddComponent<LayoutElement>();
                layoutElement.flexibleWidth = 1;
            }

            void SetNameImageObjectComponents()
            {
                //Rect Transform
                var transform = nameImageObject.AddComponent<RectTransform>();
                transform.SetParent(nameObject.transform, false);
                transform.anchorMin = new Vector2(0, 1);
                transform.anchorMax = new Vector2(0, 1);
                transform.pivot = new Vector2(0, 1);
                transform.localScale = new Vector2(1, 1);
                transform.sizeDelta = new Vector2(50, 50);
                transform.anchoredPosition = Vector2.zero;

                //UILayer
                nameImageObject.layer = 5;

                //Image
                Image image = nameImageObject.AddComponent<Image>();
                image.sprite = settings.ResourcePoolUISettings.NameSprite;
                image.type = Image.Type.Simple;
                image.color = Color.black;
        }

            void SetNameTextObjectComponents()
            {
                //Rect Transform
                var transform = nameTextObject.AddComponent<RectTransform>();
                transform.SetParent(nameObject.transform, false);
                transform.anchorMin = new Vector2(0, 1);
                transform.anchorMax = new Vector2(0, 1);
                transform.pivot = new Vector2(0, 1);
                transform.localScale = Vector3.one;
                transform.sizeDelta = new Vector2(50, 50);
                transform.anchoredPosition = new Vector2(50,0);

                //Layout Element
                var layoutElement = nameTextObject.AddComponent<LayoutElement>();
                layoutElement.preferredHeight = 50;
                layoutElement.flexibleWidth = 1;

                //Content Size Fitter
                var contentSizeFitter = nameTextObject.AddComponent<ContentSizeFitter>();
                contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                
                //Concats the first- and lastnames to get the fullname
                List<string> names = new List<string>();

                resource.FirstNames.ForEach(name => names.Add(name.ToString()));
                resource.LastNames.ForEach(name => names.Add(name.ToString()));

                string fullName = String.Join(" ", names);

                //TextMeshPro
                var textMeshPro = nameTextObject.AddComponent<TextMeshProUGUI>();
                textMeshPro.margin = new Vector4(20,0,0,0);
                textMeshPro.horizontalAlignment = HorizontalAlignmentOptions.Left;
                textMeshPro.verticalAlignment = VerticalAlignmentOptions.Middle;
                textMeshPro.text = fullName;
                textMeshPro.fontSize = 24;
                textMeshPro.enableWordWrapping = false;
                textMeshPro.overflowMode = TextOverflowModes.Truncate;
                textMeshPro.color = Color.black;
            }

            void SetRoleImageObjectComponents()
            {
                //Rect Transform
                var transform = roleImageObject.AddComponent<RectTransform>();
                transform.SetParent(roleObject.transform, false);
                transform.anchorMin = new Vector2(0, 1);
                transform.anchorMax = new Vector2(0, 1);
                transform.pivot = new Vector2(0, 1);
                transform.localScale = new Vector2(1, 1);
                transform.sizeDelta = new Vector2(50, 50);
                transform.anchoredPosition = Vector2.zero;

                //UILayer
                roleImageObject.layer = 5;

                //Image
                Image image = roleImageObject.AddComponent<Image>();
                image.sprite = settings.ResourcePoolUISettings.RoleSprite;
                image.type = Image.Type.Simple;
                image.color = Color.black;
            }

            void SetRoleTextObjectComponents()
            {
                //Rect Transform
                var transform = roleTextObject.AddComponent<RectTransform>();
                transform.SetParent(roleObject.transform, false);
                transform.anchorMin = new Vector2(0, 1);
                transform.anchorMax = new Vector2(0, 1);
                transform.pivot = new Vector2(0, 1);
                transform.localScale = Vector3.one;
                transform.sizeDelta = new Vector2(50, 50);
                transform.anchoredPosition = new Vector2(50, 0);

                //Layout Element
                var layoutElement = roleTextObject.AddComponent<LayoutElement>();
                layoutElement.preferredHeight = 50;
                layoutElement.flexibleWidth = 1;

                //Content Size Fitter
                var contentSizeFitter = roleTextObject.AddComponent<ContentSizeFitter>();
                contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

                //Concats the Professions
                var professions = new List<string>();
                resource.Professions.ForEach(profession => professions.Add(profession.ToString()));

                string allProfessions = String.Join(" ", professions);

                //TextMeshPro
                var textMeshPro = roleTextObject.AddComponent<TextMeshProUGUI>();
                textMeshPro.margin = new Vector4(20, 0, 0, 0);
                textMeshPro.horizontalAlignment = HorizontalAlignmentOptions.Left;
                textMeshPro.verticalAlignment = VerticalAlignmentOptions.Middle;
                textMeshPro.text = allProfessions;
                textMeshPro.fontSize = 24;
                textMeshPro.enableWordWrapping = false;
                textMeshPro.overflowMode = TextOverflowModes.Truncate;
                textMeshPro.color = Color.black;
            }
        }
    }
}
