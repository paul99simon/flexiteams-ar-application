using FlexiTeams.DataClasses.Task;
using UnityEngine;

namespace Assets.Scripts.UI.TaskUI {

    public class TaskUI1
    {
        private Task _task;
        private Vector3 _position;
        private Quaternion _rotation;
        private UISettings _settings;
        private Vector2 _size;

        public GameObject TaskUIObj;

        public TaskUI1(Task task, Vector3 position, Quaternion rotation, Vector2 size, UISettings settings)
        {

            this._task = task;
            this._position = position;
            this._rotation = rotation;
            this._size = size;
            this._settings = settings;

            Create();
        }

        public TaskUI1(Task task, Vector3 position, Quaternion rotation, Vector2 size)
        {

            this._task = task;
            this._position = position;
            this._rotation = rotation;
            this._size = size;
            _settings = new();

            Create();
        }

        private void Create()
        {
            TaskUIObj = new GameObject(_task.Id.ToString())
            {
                layer = 5
            };

            //Rect Transform
            var rectTransform = TaskUIObj.AddComponent<RectTransform>();
            rectTransform.SetPositionAndRotation(_position, _rotation);
            rectTransform.sizeDelta = _size;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

            var layout = new TaskUILayout(TaskUIObj, _settings);
            var data = new TaskUIData(layout, _task, _settings);
            var style = new TaskUIStyle(layout, data, _settings);
        }
    }

/*


    private static void FillInContent(Transform root, Resource resource, UISettings settings)
    {
        var professionalInfoContent = root.Find("Window/TopPanel/ProfessionalBackground/Scroll View/Viewport/Content");
        var personalInfoContent = root.Find("Window/BottomPanel/PersonalBackground/Scroll View/Viewport/Content");
        var skillsContent = root.Find("Window/BottomPanel/Skills/Scroll View/Viewport/Content");
        var traitsInfoContent = root.Find("Window/BottomPanel/Traits/Scroll View/Viewport/Content");

        FillInProfessionalInfo(professionalInfoContent, resource, settings);
        FillPersonalInfo(personalInfoContent, resource, settings);
        FillInSkills(skillsContent, resource, settings);
        FillInTraits(traitsInfoContent, resource, settings);
    }


    private static void FillInProfessionalInfo(Transform content, Resource resource, UISettings settings)
    {
        string professions = "- ";
        resource.Professions.ForEach(profession => { professions += profession.ToString() + " / "; });
        professions = professions[..^3];

        string departments = "- ";
        resource.Departments.ForEach(department => { departments += department.ToString() + " / "; });
        departments = departments[..^3];

        DataViewText(content, professions, settings);
        DataViewText(content, departments, settings);
    }

    private static void FillInSkills(Transform content, Resource resource, UISettings settings)
    {
        resource.Skills.ForEach(skill =>  FillInSKill(content, skill, settings));
    }

    private static void FillInSKill(Transform parent, Skill skill, UISettings settings)
    {
        var Skill = new GameObject("Skill");

        //Transfrom
        var transform = Skill.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        transform.pivot = new Vector2(0, 1);

        //LayoutElement
        var layoutElement = Skill.AddComponent<LayoutElement>();
        layoutElement.flexibleWidth = 1;
        layoutElement.preferredHeight = 25;

        //Layout Group
        var layoutGroup = Skill.AddComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.childForceExpandWidth = false;

        Checkmark(transform, settings);
        
    }

    private static void Checkmark(Transform parent, UISettings settings)
    {
        var Checkmark = new GameObject("Checkmark");
        
        var transform = Checkmark.AddComponent<RectTransform>();
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        transform.pivot = new Vector2(0, 1);

        //LayoutElement
        var layoutElement = Checkmark.AddComponent<LayoutElement>();
        layoutElement.preferredWidth = 20;
        layoutElement.flexibleHeight = 1;

        //Image
        var image = Checkmark.AddComponent<Image>();
        image.sprite = settings.Info.BackgroundSprite;
        
        //Toggle
        var toggle = image.AddComponent<Toggle>();

        var Image = new GameObject("Image");

        //Transform
        transform = Image.AddComponent<RectTransform>();
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        transform.pivot = new Vector2(0, 1);

        //image
        image = Image.AddComponent<Image>();
        image.sprite = settings.Skills.Checkmark;

    }
    */
}