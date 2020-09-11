using UnityEngine;
using DeadLords.Helper;

namespace DeadLords
{
    /// <summary>
    /// Базовый клас для всего оружия
    /// </summary>
    public abstract class Weapons : BaseEnvoirment
    {
        [SerializeField] [Tooltip("Точка вылета пули")] protected Transform _barrel;

        [Space(10)] [SerializeField] [Tooltip("Скорость ускорения пули")] protected float _force = 500;
        [SerializeField] [Tooltip("Время между выстрелами. Скорострельность")] protected float _rechargeTime = 0.2f;
        [SerializeField] [Tooltip("Време перезарядки")] protected float _changeMagTime = 2f;
        [SerializeField] [Tooltip("Кол-во патронов в обойме")] protected int[] _ammoCopacity = { 30, 30 };

        [Space(10)] [Tooltip("Время удара ближнего боя")] public float _meleeTime = 2;
        [Tooltip("Урон в ближнем бою")] public float _meleeDamage = 50;

        protected ParticleSystem _shootEffect;
        protected Animator _animator;

        protected bool _canFire = true;
        protected Timer _timer = new Timer();   //Таймер, определяющий скорострельность

        protected virtual void Update()
        {
            if (_barrel.tag == "Melee") return;

            _timer.Update();
            if (_timer.IsEvent())
                _canFire = true;
        }

        public abstract void Shoot(Ammunition ammunition);

        public abstract void AfterShoot();

        public abstract void Melee();

        public abstract void Reload();

        #region Для редактора

        /// <summary>
        /// Точка, откуда летит пуля
        /// </summary>
        public Transform GetBarrel
        {
            get { return _barrel; }
        }
        #endregion
    }
}
