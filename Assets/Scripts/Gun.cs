using UnityEngine;

namespace DeadLords
{
    public class Gun : Weapons
    {
        [Space(10)] [SerializeField] [Tooltip("Видимость оружия при старте")] private bool _isVisibleOnStart;
        void Start()
        {
            if (_isVisibleOnStart)
                IsVisible = true;
            else
                IsVisible = false;

            _shootEffect = GetComponentInChildren<ParticleSystem>();
            _animator = GetComponent<Animator>();
        }

        public override void Shoot(Ammunition ammunition)
        {
            if (_canFire && ammunition && _ammoCopacity[0] != 0)
            {
                Bullet bullet = Instantiate(ammunition, _barrel.position, _barrel.rotation) as Bullet;

                if (bullet)
                {
                    _shootEffect.Play();
                    _animator.SetTrigger("Shoot");

                    bullet.GetComponent<Rigidbody>().AddForce(_barrel.forward * _force);
                    bullet.Name = "Bullet";
                    _ammoCopacity[0] -= 1;
                    _canFire = false;
                    _timer.Start(_rechargeTime);
                }
            }
            else if (_ammoCopacity[0] <= 0)
            {
                Reload();
            }
        }

        public override void Melee()
        {
            _animator.SetTrigger("Melee");

            GetComponentInChildren<BoxCollider>().enabled = true;

            _canFire = false;
            _timer.Start(_meleeTime);
        }

        public override void Reload()
        {
            _animator.SetTrigger("Reloading");

            _canFire = false;
            _timer.Start(_changeMagTime);
            _ammoCopacity[0] = _ammoCopacity[1];
        }

        public override void AfterShoot()
        {
            _animator.SetTrigger("AfterFire");
        }
    }
}