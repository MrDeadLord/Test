using UnityEngine;

namespace DeadLords.Helper
{
    /// <summary>
    /// Хранит ссылки на все объекты, которые может использовать ГГ
    /// </summary>
    public class ObjManager : MonoBehaviour
    {
        [SerializeField] private Ammunition[] _ammunitions = new Ammunition[2];
        [SerializeField] private Weapons[] _weapons = new Weapons[2];
        [Space(10)]
        [SerializeField] private Light _flashlight;

        public Ammunition[] Ammunitions
        {
            get { return _ammunitions; }
        }

        public Weapons[] Weapons
        {
            get { return _weapons; }
        }

        public Light Flashlight
        {
            get { return _flashlight; }
        }
    }
}