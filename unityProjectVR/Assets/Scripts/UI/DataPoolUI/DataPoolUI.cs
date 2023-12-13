using Assets.Scripts.Application;
using FlexiTeams.DataClasses.Data.Wrapper;
using FlexiTeams.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataPoolUI : MonoBehaviour
{

    private VR_AR_Application application;
    private Transform content;
    private UISettings settings;
    private DataPool _pool;

    // Start is called before the first frame update
    void Start()
    {
        application = GameObject.Find("Application").GetComponent<VR_AR_Application>();
        settings = application.Settings;
        content = GameObject.Find("DataPoolUI").transform.Find("Panel_List/Scroll View/Viewport/Content");
        _pool = application.DataPool;
        foreach (var pair in _pool.Stock)
        {
            AddDataButtonObject(pair.Key, pair.Value);
        }

    }

    private void AddDataButtonObject(DataName dataName, int count)
    {
        var dataObject = new GameObject("Data");
        SetButtonObjectComponents();
        SetChildren();
        
        void SetButtonObjectComponents(){
            
            //Rect Transform
            var transform = dataObject.AddComponent<RectTransform>();
            transform.SetParent(content, false);
            transform.sizeDelta = new Vector2(100, 50);

            //UILayer
            dataObject.layer = 5;

            //Horizontal Layout Group
            HorizontalLayoutGroup group = dataObject.AddComponent<HorizontalLayoutGroup>();
            group.padding.left = 20;
            group.childAlignment = TextAnchor.UpperLeft;
            group.childControlHeight = true;
            group.childControlWidth = true;
            group.childForceExpandHeight = true;
            group.childForceExpandWidth = false;

            //Layout Element
            LayoutElement layoutElement = dataObject.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1;

            //Image
            Image image = dataObject.AddComponent<Image>();
            image.sprite = settings.BackgroundSprite;
            image.type = Image.Type.Sliced;

            //ButtonComponent
            DataButton button = dataObject.AddComponent<DataButton>();
            button.interactable = true;
            button.image = image;
            button.transition = Selectable.Transition.ColorTint;
            button.onClick.AddListener(button.OnClick);
            button.colors = settings.ButtonColors;


            var navigation = new Navigation
            {
                mode = Navigation.Mode.Automatic
            };
            button.navigation = navigation;
        }
        void SetChildren(){

            
            GameObject nameObject = new GameObject("Name");
            GameObject roleObject = new GameObject("Role");

            GameObject nameImageObject = new GameObject("Image");
            GameObject nameTextObject = new GameObject("Text");
            GameObject roleTextObject = new GameObject("Text");

            SetNameObjectComponents();
            SetRoleObjectComponents();

            SetNameImageObjectComponents();
            SetNameTextObjectComponents();
            SetCountTextObjectComponents();

            void SetNameObjectComponents()
            {
                //Rect Transform
                var transform = nameObject.AddComponent<RectTransform>();
                transform.SetParent(dataObject.transform, false);

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
                transform.SetParent(dataObject.transform, false);

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
                image.sprite = settings.DataPoolUI.ToolSprite;
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

                string type = dataName.ToString();

                //TextMeshPro
                var textMeshPro = nameTextObject.AddComponent<TextMeshProUGUI>();
                textMeshPro.margin = new Vector4(20,0,0,0);
                textMeshPro.horizontalAlignment = HorizontalAlignmentOptions.Left;
                textMeshPro.verticalAlignment = VerticalAlignmentOptions.Middle;
                textMeshPro.text = type;
                textMeshPro.fontSize = 24;
                textMeshPro.enableWordWrapping = false;
                textMeshPro.overflowMode = TextOverflowModes.Truncate;
                textMeshPro.color = Color.black;
            }

            void SetCountTextObjectComponents()
            {
                //Rect Transform
                var transform = roleTextObject.AddComponent<RectTransform>();
                transform.SetParent(roleObject.transform, false);
                transform.anchorMin = new Vector2(0, 1);
                transform.anchorMax = new Vector2(0, 1);
                transform.pivot = new Vector2(0, 1);
                transform.localScale = Vector3.one;
                transform.sizeDelta = new Vector2(50, 50);
                transform.anchoredPosition = new Vector2(0, 0);

                //Layout Element
                var layoutElement = roleTextObject.AddComponent<LayoutElement>();
                layoutElement.preferredHeight = 50;
                layoutElement.flexibleWidth = 1;

                //Content Size Fitter
                var contentSizeFitter = roleTextObject.AddComponent<ContentSizeFitter>();
                contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

                //Concats the Professions

                //TextMeshPro
                var textMeshPro = roleTextObject.AddComponent<TextMeshProUGUI>();
                textMeshPro.margin = new Vector4(20, 0, 0, 0);
                textMeshPro.horizontalAlignment = HorizontalAlignmentOptions.Left;
                textMeshPro.verticalAlignment = VerticalAlignmentOptions.Middle;
                textMeshPro.text = count.ToString();
                textMeshPro.fontSize = 24;
                textMeshPro.enableWordWrapping = false;
                textMeshPro.overflowMode = TextOverflowModes.Truncate;
                textMeshPro.color = Color.black;
            }
        }
    }
}
