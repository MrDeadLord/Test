using UnityEngine;

namespace DeadLords     //Для объеденения подгрупп моих личных кодов в проекте
{
    /// <summary>
    /// Базовый класс для ВСЕХ объектов
    /// </summary>
    public abstract class BaseEnvoirment : MonoBehaviour        //Абстрактный класс, что бы его нельзя было изменить извне
    {
        #region Переменные
        protected GameObject _gameObj;
        protected Transform _transform;
        protected Vector3 _position;
        protected Quaternion _rotation;
        protected Vector3 _scale;
        protected int _layer;
        protected Color _color;
        protected Material _mat;
        protected Rigidbody _rigBody;
        protected Bounds _bound;
        protected string _name;
        protected bool _isVisible;
        private Vector3 _center;
        #endregion

        #region Получение этих переменных

        /// <summary>
        /// Получение ссылки на GameObject
        /// </summary>
        public GameObject GetGameObject
        {
            get { return _gameObj; }
        }

        /// <summary>
        /// Получение Transform объекта
        /// </summary>
        public Transform GetTransform
        {
            get { return _transform; }
        }

        /// <summary>
        /// Позиция объяекта
        /// </summary>
        public Vector3 Position
        {
            get
            {
                if (_gameObj != null) { _position = _gameObj.transform.position; }
                return _position;
            }
            set
            {
                _position = value;
                if (_position != null) { }
            }
        }

        /// <summary>
        /// Поворот объекта
        /// </summary>
        public Quaternion Rotation
        {
            get
            {
                if (_gameObj != null)
                    _rotation = _gameObj.transform.rotation;

                return _rotation;
            }
            set
            {
                _rotation = value;
                if (_gameObj != null)
                    _gameObj.transform.rotation = _rotation;
            }
        }

        /// <summary>
        /// Размер объекта
        /// </summary>
        public Vector3 Scale
        {
            get
            {
                if (_gameObj != null) { _scale = _gameObj.transform.localScale; }
                return _scale;
            }
            set
            {
                _scale = value;
                if (_gameObj != null) { _gameObj.transform.localScale = _scale; }
            }
        }

        /// <summary>
        /// Слой объекта(Editable)
        /// </summary>
        public int Layers
        {
            get { return _layer; }
            set
            {
                _layer = value;
                if (_gameObj != null)
                {
                    _gameObj.layer = _layer;
                    AskLayer(_gameObj.transform, value);
                }
            }
        }

        /// <summary>
        /// Цвет материала(Editable)
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                if (_mat != null) { _mat.color = _color; }
                AskColor(_gameObj.transform, _color);
            }
        }

        /// <summary>
        /// Получение материала
        /// </summary>
        public Material GetMaterial
        {
            get { return _mat; }
        }

        /// <summary>
        /// Получение физики объекта(RigidBody)
        /// </summary>
        public Rigidbody GetRigidBody
        {
            get { return _rigBody; }
        }

        /// <summary>
        /// Имя объекта(Editable)
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _gameObj.name = _name;
            }
        }

        /// <summary>
        /// Скрывает/показывает объект(вкл/выкл рендер объекта)
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                AskVisible(GetTransform, _isVisible);
            }
        }
        #endregion

        #region Приватные функции

        /// <summary>
        /// Выставление слоя объекту и всем вложенным в него объектам(на любом уровне вложенности)
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="lvl">Слой</param>
        private void AskLayer(Transform obj, int lvl)
        {
            obj.gameObject.layer = lvl;

            if (obj.childCount > 0)
                foreach (Transform child in obj) { AskLayer(child, lvl); }
        }

        /// <summary>
        /// Выставление цвета объекту и всем вложенным в него объектам(на любом уровне вложенности)
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="color">Цвет</param>
        private void AskColor(Transform obj, Color color)
        {
            obj.GetComponent<MeshRenderer>().material.color = color;

            if (obj.childCount > 0)
                foreach (Transform child in obj) { AskColor(child, color); }
        }

        private void AskVisible(Transform obj, bool value)
        {
            if (obj.GetComponent<MeshRenderer>())
                obj.GetComponent<MeshRenderer>().enabled = _isVisible;
            if (obj.GetComponent<SkinnedMeshRenderer>())
                obj.GetComponent<SkinnedMeshRenderer>().enabled = _isVisible;
            if (obj.childCount > 0)
            {
                foreach (Transform child in obj)
                    AskVisible(child, value);
            }
        }
        #endregion

        /// <summary>
        /// В данном случае - кэширование переменных
        /// </summary>
        protected virtual void Awake()
        {
            _gameObj = gameObject;
            _name = _gameObj.name;
            _rigBody = _gameObj.GetComponent<Rigidbody>();
            _transform = _gameObj.transform;
            if (_gameObj.GetComponent<Renderer>()) { _mat = _gameObj.GetComponent<Renderer>().material; }
        }       //Класс сделан виртуальным, чтобы его можно было изменять
    }
}