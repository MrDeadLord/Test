using DeadLords.Controller;
using DeadLords.Helper;
using UnityEngine;

namespace DeadLords
{
    public class Main : MonoBehaviour
    {
        public static Main Instance { get; private set; }

        private GameObject _controllers;
        private InputController _inputController;
        private WeaponsController _weaponsController;
        private ObjManager _objManager;
        private OtherStuffCollector _otherStuffCollector;
        private BotsControlCenter _botsControlCenter;
        private SpawnController _spawnController;

        private void Start()
        {
            Instance = this;
            _controllers = new GameObject("Controllers");
            _objManager = GetComponent<ObjManager>();
            _otherStuffCollector = GetComponent<OtherStuffCollector>();
            _inputController = _controllers.AddComponent<InputController>();
            _weaponsController = _controllers.AddComponent<WeaponsController>();
            _botsControlCenter = _controllers.AddComponent<BotsControlCenter>();
            _spawnController = _controllers.AddComponent<SpawnController>();
        }


        #region Получение контроллеров
        public InputController GetInputController
        {
            get { return _inputController; }
        }

        public WeaponsController GetWeaponsController
        {
            get { return _weaponsController; }
        }

        public ObjManager GetObjManager
        {
            get { return _objManager; }
        }

        public OtherStuffCollector GetOtherStuffCollector
        {
            get { return _otherStuffCollector; }
        }

        public BotsControlCenter GetBotsControlCenter
        {
            get { return _botsControlCenter; }
        }

        public SpawnController GetSpawnController
        {
            get { return _spawnController; }
        }
        #endregion
    }
}