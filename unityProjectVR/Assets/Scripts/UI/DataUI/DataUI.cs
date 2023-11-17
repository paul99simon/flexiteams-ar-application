using FlexiTeams.DataClasses.Data;
using UnityEngine;


namespace Assets.Scripts.UI.DataUI {

    public class DataUI
    {
        private Data _data;
        private Vector3 _position;
        private Quaternion _rotation;
        private UISettings _settings;
        private Vector2 _size;

        public GameObject ResourceUIObj;

        public DataUI(Data data, Vector3 position, Quaternion rotation, Vector2 size, UISettings settings)
        {

            this._data = data;
            this._position = position;
            this._rotation = rotation;
            this._size = size;
            this._settings = settings;

            Create();
        }

        public DataUI(Data data, Vector3 position, Quaternion rotation, Vector2 size)
        {

            this._data = data;
            this._position = position;
            this._rotation = rotation;
            this._size = size;
            _settings = new();

            Create();
        }

        private void Create()
        {
            ResourceUIObj = new GameObject(this._data.Id.ToString())
            {
                layer = 5
            };

            //Rect Transform
            var rectTransform = ResourceUIObj.AddComponent<RectTransform>();
            rectTransform.SetPositionAndRotation(_position, _rotation);
            rectTransform.sizeDelta = _size;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

            var layout = new DataUILayout(ResourceUIObj, _settings);
            var data = new DataUIData(layout, _data, _settings);
            var style = new DataUIStyle(layout, data, _settings);
        }
    }
}