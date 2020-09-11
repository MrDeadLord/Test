using DeadLords.Interface;
using UnityEngine;

namespace DeadLords
{
    public class Firstaid : MonoBehaviour
    {
        [SerializeField] [Tooltip("Кол-во восстанавливаемого здоровья")] private float _value;
        [SerializeField] private ParticleSystem _healEffect;

        private void Start()
        {
            _value = -_value;
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Enemy" || col.tag == "Player")
            {
                Heal(col.gameObject.GetComponent<ISetDamage>());

                Instantiate(_healEffect, transform.position, gameObject.transform.rotation);

                Main.Instance.GetSpawnController.DeleteObject(gameObject);

                Destroy(gameObject);
            }
        }

        public void Heal(ISetDamage obj)
        {
            if (obj != null)
                obj.ApplyDamage(_value);
        }
    }
}