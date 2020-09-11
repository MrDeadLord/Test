using DeadLords.Interface;
using UnityEngine;

namespace DeadLords
{
    [RequireComponent(typeof(Collider))]
    public class MeleeHit : MonoBehaviour
    {
        private float _damage;
        private float _meleeTime;
        private Collider _collider;

        private void Start()
        {
            _meleeTime = GetComponentInParent<Weapons>()._meleeTime;
            _collider = GetComponent<Collider>();
            _damage = GetComponentInParent<Weapons>()._meleeDamage;

            _collider.enabled = false;
        }

        private void Update()
        {
            if (_collider.enabled)
                Invoke("DisableMelee", _meleeTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Bullet") return;

            if (collision.collider.tag == "Enemy")
            {
                SetDamage(collision.gameObject.GetComponent<ISetDamage>());
            }
        }

        private void DisableMelee()
        {
            _collider.enabled = false;
        }

        private void SetDamage(ISetDamage obj)
        {
            if (obj != null)
            {
                obj.ApplyDamage(_damage);
            }
        }
    }
}