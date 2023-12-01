using Assets.Scripts.UI.Common;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace Assets.Scripts.UI.ResourceUI
{
    internal class ResourceUILayout
    {
        private readonly UISettings _settings;

        public GameObject ResourceUIObj;

        public GameObject TitleBarObj;

        public GameObject TitleObj;
        public GameObject TitleBarTextObj;

        public GameObject ButtonsObj;
        public GameObject DragButtonObj;
        public GameObject LanguageButtonObj;
        public GameObject CloseButtonObj;
        public GameObject DragButtonImageObj;
        public GameObject LanguageButtonImageObj;
        public GameObject CloseButtonImageObj;

        public GameObject WindowObj;
        public GameObject TopPanelObj;
        public GameObject BottomPanelObj;

        public GameObject PortraitObj;
        public GameObject PortraitHeaderObj;
        public GameObject PortraitHeaderTextObj;
        public GameObject PortraitViewObj;
        public GameObject PortraitImageObj;

        public GameObject PersonalInfoObj;
        public GameObject PersonalInfoHeaderObj;
        public GameObject PersonalInfoHeaderTextObj;
        public GameObject PersonalInfoScrollViewObj;
        public GameObject PersonalInfoViewPortObj;
        public GameObject PersonalInfoSlidingAreaObj;
        public GameObject PersonalInfoHandleObj;
        public GameObject PersonalInfoContenObj;
        public GameObject PersonalInfoScrollbarVerticalObj;

        public GameObject ProfessionalInfoObj;
        public GameObject ProfessionalInfoHeaderObj;
        public GameObject ProfessionalInfoHeaderTextObj;
        public GameObject ProfessionalInfoScrollViewObj;
        public GameObject ProfessionalInfoViewPortObj;
        public GameObject ProfessionalInfoSlidingAreaObj;
        public GameObject ProfessionalInfoHandleObj;
        public GameObject ProfessionalInfoContenObj;
        public GameObject ProfessionalInfoScrollbarVerticalObj;

        public GameObject SkillsObj;
        public GameObject SkillsHeaderObj;
        public GameObject SkillsHeaderTextObj;
        public GameObject SkillsScrollViewObj;
        public GameObject SkillsViewPortObj;
        public GameObject SkillsSlidingAreaObj;
        public GameObject SkillsHandleObj;
        public GameObject SkillsContenObj;
        public GameObject SkillsScrollbarVerticalObj;

        public GameObject TraitsObj;
        public GameObject TraitsHeaderObj;
        public GameObject TraitsHeaderTextObj;
        public GameObject TraitsScrollViewObj;
        public GameObject TraitsViewPortObj;
        public GameObject TraitsSlidingAreaObj;
        public GameObject TraitsHandleObj;
        public GameObject TraitsContenObj;
        public GameObject TraitsScrollbarVerticalObj;

        public ResourceUILayout(GameObject resourceUIObj, UISettings settings) {
            
            ResourceUIObj = resourceUIObj;
            _settings = settings;
            Layout();
        }

        private void Layout()
        {
            //Vertical Layout Group
            var layoutGroup = ResourceUIObj.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = true;
            layoutGroup.spacing = 0;

            //Canvas
            var canvas = ResourceUIObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

            //Tracked DEvice Graphic Raycaster
            var _ = ResourceUIObj.AddComponent<TrackedDeviceGraphicRaycaster>();

            TitleBar();
            Window();
        }

        private void TitleBar()
        {
           TitleBarObj = new GameObject("TitleBar")
            {
                layer = 5
            };

            //Rect Transform
            var transform = TitleBarObj.AddComponent<RectTransform>();
            transform.SetParent(ResourceUIObj.transform);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.pivot = new Vector2(0, 1);
            transform.localScale = Vector3.one;

            //LayoutGroup
            var layoutGroup = TitleBarObj.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;

            //LayoutElement
            var layoutElement = TitleBarObj.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 100;

            TitleBarTitle();
            TitleBarButtons();
        }

        private void TitleBarTitle()
        {
            TitleObj = new GameObject("Title")
            {
                layer = 5
            };

            //Rect Transform
            var transform = TitleObj.AddComponent<RectTransform>();
            transform.SetParent(TitleBarObj.transform);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;
            transform.pivot = new Vector2(0, 1);

            //Layout Element
            var layoutElement = TitleObj.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1;

            TitleBarText();
        }

        private void TitleBarText()
        {
            TitleBarTextObj = new GameObject("Text")
            {
                layer = 5
            };

            //Rect Tranform
            var transform = TitleBarTextObj.AddComponent<RectTransform>();
            transform.SetParent(TitleObj.transform);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;
            transform.pivot = new Vector2(0, 1);
            transform.anchorMin = new Vector2(0, 1);
            transform.anchorMax = new Vector2(0, 1);
            transform.anchoredPosition = Vector3.zero;

            //Layout Element
            var layoutElement = TitleBarTextObj.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 100;
            layoutElement.flexibleWidth = 1;

            //Content Size Fitter
            var contentSizeFitter = TitleBarTextObj.AddComponent<ContentSizeFitter>();
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        private void TitleBarButtons()
        {
            ButtonsObj = new GameObject("Buttons")
            {
                layer = 5
            };

            //Rect Tranform
            var transform = ButtonsObj.AddComponent<RectTransform>();
            transform.SetParent(TitleBarObj.transform);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            //Canvas
            ButtonsObj.AddComponent<Canvas>();

            //tracked Device Raycaster
            ButtonsObj.AddComponent<TrackedDeviceGraphicRaycaster>();

            //Layout ELement
            var layoutElement = ButtonsObj.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 300;

            //Layout Group
            var layoutGroup = ButtonsObj.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childForceExpandHeight = false;

            //Buttons
            DragButtonObj = TitleBarButton("DragButton");
            LanguageButtonObj = TitleBarButton("LanguageButton");
            CloseButtonObj = TitleBarButton("CloseButton");

            DragButtonImageObj = TitleBarButtonImage(DragButtonObj.transform);
            LanguageButtonImageObj = TitleBarButtonImage(LanguageButtonObj.transform);
            CloseButtonImageObj = TitleBarButtonImage(CloseButtonObj.transform);

            var dragbutton = DragButtonObj.AddComponent<DragButton>();
            dragbutton.Obj = ResourceUIObj;
            var image = DragButtonObj.AddComponent<Image>();
            dragbutton.image = DragButtonObj.GetComponent<Image>();

            var langauageButton = LanguageButtonObj.AddComponent<Button>();
            image = LanguageButtonObj.AddComponent<Image>();
            langauageButton.image = LanguageButtonObj.GetComponent<Image>();

            var closeButton = CloseButtonObj.AddComponent<CloseButton>();
            closeButton.Obj = ResourceUIObj;
            closeButton.onClick.AddListener(closeButton.OnClick);

        }

        private GameObject TitleBarButton(string name)
        {
            var button = new GameObject(name)
            {
                layer = 5
            };

            //Transform
            var transform = button.AddComponent<RectTransform>();
            transform.SetParent(ButtonsObj.transform);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            //Layout Element
            var layoutElement = button.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 100;
            layoutElement.preferredHeight = 100;

            return button;
        }

        private GameObject TitleBarButtonImage(Transform parent)
        {
            //Image
            var image = new GameObject("Image")
            {
                layer = 5
            };
            var transform = image.AddComponent<RectTransform>();
            transform.SetParent(parent);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;
            transform.anchoredPosition = Vector3.zero;

            return image;
        }

        private void Window()
        {
            WindowObj = new GameObject("Window")
            {
                layer = 5
            };

            //Transform
            var transform = WindowObj.AddComponent<RectTransform>();
            transform.SetParent(ResourceUIObj.transform);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            //LayoutElement
            var layoutElement = WindowObj.AddComponent<LayoutElement>();
            layoutElement.flexibleHeight = 1;

            //LayoutGroup
            var layoutGroup = WindowObj.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = true;
            layoutGroup.childForceExpandHeight = true;
            layoutGroup.childForceExpandWidth = true;

            int spacing = (int)_settings.ResourceUISettings.Spacing;
            layoutGroup.padding = new RectOffset(spacing, spacing, spacing, spacing);
            layoutGroup.spacing = spacing;

            TopPanel();
            BottomPanel();
        }

        private void TopPanel()
        {
            TopPanelObj = new GameObject("TopPanel")
            {
                layer = 5
            };

            //Transform
            var transform = TopPanelObj.AddComponent<RectTransform>();
            transform.SetParent(WindowObj.transform);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            //Layout Group
            var layoutGroup = TopPanelObj.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.childControlHeight = true;
            layoutGroup.childForceExpandHeight = true;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.spacing = _settings.ResourceUISettings.Spacing;

            PortraitObj = PortraitPanel();
            PortraitHeaderObj = PortraitHeader(PortraitObj.transform);
            PortraitHeaderTextObj = HeaderText(PortraitHeaderObj.transform);
            PortraitViewObj = PortraitView(PortraitObj.transform);
            PortraitImageObj = PortraitImage(PortraitViewObj.transform);

            PersonalInfoObj = Panel(TopPanelObj.transform, "PersonalInfo");
            PersonalInfoHeaderObj = Header(PersonalInfoObj.transform);
            PersonalInfoHeaderTextObj = HeaderText(PersonalInfoHeaderObj.transform);

            PersonalInfoScrollViewObj = ScrollView(PersonalInfoObj.transform);
            PersonalInfoViewPortObj = Viewport(PersonalInfoScrollViewObj.transform);
            PersonalInfoScrollbarVerticalObj = ScrollbarVertical(PersonalInfoScrollViewObj.transform);
            PersonalInfoSlidingAreaObj = SlidingArea(PersonalInfoScrollbarVerticalObj.transform);
            PersonalInfoHandleObj = Handle(PersonalInfoSlidingAreaObj.transform);
            PersonalInfoContenObj = Content(PersonalInfoViewPortObj.transform);

            AddScrollBar(PersonalInfoScrollbarVerticalObj);
            AddScrollRect(PersonalInfoScrollViewObj);
        }

        private GameObject PortraitPanel()
        {
            var panel = new GameObject("Portrait")
            {
                layer = 5
            };

            //Transform
            var transform = panel.AddComponent<RectTransform>();
            transform.SetParent(TopPanelObj.transform);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            //LayoutElement
            var layoutElement = panel.AddComponent<LayoutElement>();
            layoutElement.preferredWidth = 250;

            //LayoutGroup
            var layoutGroup = panel.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = true;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;

            return panel;
        }

        private GameObject PortraitView(Transform parent)
        {

            var portrait = new GameObject("View")
            {
                layer = 5
            };

            //Transform
            var transform = portrait.AddComponent<RectTransform>();
            transform.SetParent(parent);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            //Layout Element
            var layoutELement = portrait.AddComponent<LayoutElement>();
            layoutELement.flexibleHeight = 1;
            layoutELement.preferredWidth = 250;

            return portrait;
        }

        private GameObject PortraitImage(Transform parent)
        {
            var imageObj = new GameObject("Image");

            //Transform
            var transform = imageObj.AddComponent<RectTransform>();
            transform.SetParent(parent);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;
            transform.anchoredPosition = Vector3.zero;
            transform.sizeDelta = new Vector2(200, 200);

            return imageObj;
        }

        private void BottomPanel()
        {
            BottomPanelObj = new GameObject("BottomPanel")
            {
                layer = 5
            };

            //Transform
            var transform = BottomPanelObj.AddComponent<RectTransform>();
            transform.SetParent(WindowObj.transform);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            //Layout Group
            var layoutGroup = BottomPanelObj.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.childControlHeight = true;
            layoutGroup.childControlWidth = true;
            layoutGroup.childForceExpandHeight = true;
            layoutGroup.childForceExpandWidth = true;
            layoutGroup.spacing = _settings.ResourceUISettings.Spacing;

            ProfessionalInfoObj = Panel(BottomPanelObj.transform, "ProfessionalInfo");
            ProfessionalInfoHeaderObj = Header(ProfessionalInfoObj.transform);
            ProfessionalInfoHeaderTextObj = HeaderText(ProfessionalInfoHeaderObj.transform);
            ProfessionalInfoScrollViewObj = ScrollView(ProfessionalInfoObj.transform);
            ProfessionalInfoViewPortObj = Viewport(ProfessionalInfoScrollViewObj.transform);
            ProfessionalInfoScrollbarVerticalObj = ScrollbarVertical(ProfessionalInfoScrollViewObj.transform);
            ProfessionalInfoSlidingAreaObj = SlidingArea(ProfessionalInfoScrollbarVerticalObj.transform);
            ProfessionalInfoHandleObj = Handle(ProfessionalInfoSlidingAreaObj.transform);
            ProfessionalInfoContenObj = Content(ProfessionalInfoViewPortObj.transform);
            AddScrollBar(ProfessionalInfoScrollbarVerticalObj);
            AddScrollRect(ProfessionalInfoScrollViewObj);

            SkillsObj = Panel(BottomPanelObj.transform, "Skills");
            SkillsHeaderObj = Header(SkillsObj.transform);
            SkillsHeaderTextObj = HeaderText(SkillsHeaderObj.transform);
            SkillsScrollViewObj = ScrollView(SkillsObj.transform);
            SkillsViewPortObj = Viewport(SkillsScrollViewObj.transform);
            SkillsScrollbarVerticalObj = ScrollbarVertical(SkillsScrollViewObj.transform);
            SkillsSlidingAreaObj = SlidingArea(SkillsScrollbarVerticalObj.transform);
            SkillsHandleObj = Handle(SkillsSlidingAreaObj.transform);
            SkillsContenObj = Content(SkillsViewPortObj.transform);
            AddScrollBar(SkillsScrollbarVerticalObj);
            AddScrollRect(SkillsScrollViewObj);

            TraitsObj = Panel(BottomPanelObj.transform, "Traits");
            TraitsHeaderObj = Header(TraitsObj.transform);
            TraitsHeaderTextObj = HeaderText(TraitsHeaderObj.transform);
            TraitsScrollViewObj = ScrollView(TraitsObj.transform);
            TraitsViewPortObj = Viewport(TraitsScrollViewObj.transform);
            TraitsScrollbarVerticalObj = ScrollbarVertical(TraitsScrollViewObj.transform);
            TraitsSlidingAreaObj = SlidingArea(TraitsScrollbarVerticalObj.transform);
            TraitsHandleObj = Handle(TraitsSlidingAreaObj.transform);
            TraitsContenObj = Content(TraitsViewPortObj.transform);
            AddScrollBar(TraitsScrollbarVerticalObj);
            AddScrollRect(TraitsScrollViewObj);
        }

        private GameObject Panel(Transform parent, string name)
        {
            var panel = new GameObject(name)
            {
                layer = 5
            };

            //Transform
            var transform = panel.AddComponent<RectTransform>();
            transform.SetParent(parent);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            //LayoutElement
            var layoutElement = panel.AddComponent<LayoutElement>();
            layoutElement.flexibleWidth = 1;

            //LayoutGroup
            var layoutGroup = panel.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlWidth = true;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;

            return panel;
        }

        private GameObject PortraitHeader(Transform parent)
        {
            var header = new GameObject("Header")
            {
                layer = 5
            };

            //Transform
            var transform = header.AddComponent<RectTransform>();
            transform.SetParent(parent);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            //LayoutElement
            var layoutElement = header.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 50;
            layoutElement.preferredWidth = 250;

            return header;
        }

        private GameObject Header(Transform parent)
        {
            var header = new GameObject("Header")
            {
                layer = 5
            };

            //Transform
            var transform = header.AddComponent<RectTransform>();
            transform.SetParent(parent);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            //LayoutElement
            var layoutElement = header.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 50;
            layoutElement.flexibleWidth = 1;

            return header;
        }

        private GameObject HeaderText(Transform parent)
        {
            var text = new GameObject("Text")
            {
                layer = 5
            };

            //Transform
            var transform = text.AddComponent<RectTransform>();
            transform.SetParent(parent);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            return text;
        }

        private GameObject ScrollView(Transform parent)
        {
            var scrollView = new GameObject("ScrollView")
            {
                layer = 5
            };

            //Transform
            var transform = scrollView.AddComponent<RectTransform>();
            transform.SetParent(parent);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            //LayoutGroup
            var layoutGroup = scrollView.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.childControlHeight = true;
            layoutGroup.childControlWidth = true;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;

            //Layout Element
            var layoutElement = scrollView.AddComponent<LayoutElement>();
            layoutElement.flexibleHeight = 1;
            layoutElement.flexibleWidth = 1;

            return scrollView;
        }

        private GameObject Viewport(Transform parent)
        {
            var viewport = new GameObject("Viewport")
            {
                layer = 5
            };

            //Transform
            var transform = viewport.AddComponent<RectTransform>();
            transform.SetParent(parent);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;
            transform.pivot = new Vector2(0, 1);

            //LayoutElement
            var layoutElement = viewport.AddComponent<LayoutElement>();
            layoutElement.flexibleHeight = 1;
            layoutElement.flexibleWidth = 1;

            //Mask
            _ = viewport.AddComponent<Mask>();

            return viewport;
        }

        private GameObject ScrollbarVertical(Transform parent)
        {
            var ScrollbarVertical = new GameObject("ScrollbarVertical")
            {
                layer = 5
            };

            //Transform
            var transform = ScrollbarVertical.AddComponent<RectTransform>();
            transform.SetParent(parent);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            //Layout Element
            var layoutElement = ScrollbarVertical.AddComponent<LayoutElement>();
            layoutElement.flexibleHeight = 1;
            layoutElement.preferredWidth = 20;

            return ScrollbarVertical;
        }

        private GameObject Content(Transform parent)
        {
            var content = new GameObject("Content")
            {
                layer = 5
            };

            //Transform
            var transform = content.AddComponent<RectTransform>();
            transform.SetParent(parent);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;
            transform.anchoredPosition = Vector3.zero;
            transform.anchorMin = Vector2.zero;
            transform.anchorMax = Vector2.one;
            transform.pivot = new Vector2(0, 1);
            transform.sizeDelta = new Vector2(0, 0);

            //Content Size Fitter
            var contentSizeFitter = content.AddComponent<ContentSizeFitter>();
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            //Layout Group
            var layoutGroup = content.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = true;
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.padding = new RectOffset(20, 0, 20, 0);
            layoutGroup.spacing = 20;

            return content;
        }

        private GameObject SlidingArea(Transform parent)
        {
            var slidingArea = new GameObject("SlidingArea")
            {
                layer = 5
            };

            //Transform
            var transform = slidingArea.AddComponent<RectTransform>();
            transform.SetParent(parent);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;
            transform.anchorMin = Vector2.zero;
            transform.anchorMax = Vector2.one;

            return slidingArea;
        }

        private GameObject Handle(Transform parent)
        {
            var handle = new GameObject("Handle")
            {
                layer = 5
            };

            //Transform
            var transform = handle.AddComponent<RectTransform>();
            transform.SetParent(parent);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.localScale = Vector3.one;

            return handle;
        }

        private void AddScrollRect(GameObject scrollView)
        {
            //Scroll Rect
            var scrollRect = scrollView.AddComponent<ScrollRect>();
            scrollRect.content = scrollView.transform.Find("Viewport/Content").GetComponent<RectTransform>();
            scrollRect.vertical = true;
            scrollRect.horizontal = false;
            scrollRect.viewport = scrollView.transform.Find("Viewport").GetComponent<RectTransform>();
            scrollRect.verticalScrollbar = scrollView.transform.Find("ScrollbarVertical").GetComponent<Scrollbar>();
        }

        private void AddScrollBar(GameObject scrollbarVertical)
        {
            //Scrollbar
            var scrollbar = scrollbarVertical.AddComponent<Scrollbar>();
            scrollbar.targetGraphic = scrollbarVertical.transform.Find("SlidingArea/Handle").GetComponent<Image>();
            scrollbar.handleRect = scrollbarVertical.transform.Find("SlidingArea/Handle").GetComponent<RectTransform>();
            scrollbar.direction = Scrollbar.Direction.BottomToTop;
            scrollbar.size = 0.2f;
            scrollbar.value = 0;

            var slidingAreaTransform = scrollbarVertical.transform.Find("SlidingArea").GetComponent<RectTransform>();
            var handleTranform = scrollbarVertical.transform.Find("SlidingArea/Handle").GetComponent<RectTransform>();

            slidingAreaTransform.anchoredPosition = new Vector3(10, 10, 0);
            slidingAreaTransform.sizeDelta = new Vector2(10, 10);

            handleTranform.anchoredPosition = new Vector3(-10, -10, 0);
            handleTranform.sizeDelta = new Vector2(-10, -10);
        }
    }
}