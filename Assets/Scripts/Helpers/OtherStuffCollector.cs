using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Helper
{
    public class OtherStuffCollector : MonoBehaviour
    {
        [SerializeField] [Tooltip("Все боты с тегом Enemy")] private List<Bot> _botsList = new List<Bot>();
        [Header("Interactive objects")]
        [SerializeField] [Tooltip("Точки спауна аптечек")] private Transform spawnHealthParrent;
        [SerializeField] [Tooltip("Точки спауна мин")] private Transform spawnMinesParrent;
        [Space(10)][SerializeField] [Tooltip("Аптечка")] private GameObject _health;
        [SerializeField] [Tooltip("Мина")] private GameObject _mine;

        public List<Bot> BotsList
        {
            get { return _botsList; }
            set { _botsList = value; }
        }

        public Transform SpawnHealthParrent
        {
            get { return spawnHealthParrent; }
        }

        public Transform SpawnMinesParrent
        {
            get { return spawnMinesParrent; }
        }

        public GameObject Health
        {
            get { return _health; }
        }

        public GameObject Mine
        {
            get { return _mine; }
        }
    }
}