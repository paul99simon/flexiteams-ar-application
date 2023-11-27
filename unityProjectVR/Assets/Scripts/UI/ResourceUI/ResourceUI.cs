using Assets.Scripts.UI.ResourceUI;
using FlexiTeams.DataClasses.Resource;
using UnityEngine;

namespace Assets.Scripts.UI.ResourceUI
{


    public class ResourceUI
    {
        private Resource _resource;
        private Vector3 _position;
        private Quaternion _rotation;
        private UISettings _settings;
        private Vector2 _size;

        public GameObject ResourceUIObj;

            public ResourceUI(Resource resource,Vector3 position, Quaternion rotation, Vector2 size, UISettings settings) {
        
                this._resource = resource;
                this._position = position;
                this._rotation = rotation;
                this._size = size;
                this._settings = settings;

                Create();
            }

            public ResourceUI(Resource resource, Vector3 position, Quaternion rotation, Vector2 size) {
        
                this._resource = resource;
                this._position = position;
                this._rotation = rotation;
                this._size = size;
                _settings = new();

                Create();
            }

            private void Create()
            {
                ResourceUIObj = new GameObject(_resource.Id.ToString())
                {
                    layer = 5
                };

                //Rect Transform
                var rectTransform = ResourceUIObj.AddComponent<RectTransform>();
                rectTransform.SetPositionAndRotation(_position, _rotation);
                rectTransform.sizeDelta = _size;
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

                var layout = new ResourceUILayout(ResourceUIObj, _settings);
                var data = new ResourceUIData(layout, _resource, _settings);
                var style = new ResourceUIStyle(layout, data, _resource, _settings);
            }
    }
}