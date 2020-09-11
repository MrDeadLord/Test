using DeadLords.Interface;
using UnityEngine;

namespace DeadLords
{
    public class Mine : MonoBehaviour
    {
        [SerializeField] [Tooltip("Эффект взрыва")] private ParticleSystem _explosion;
        [SerializeField] [Tooltip("Урон от взрыва")] private float _damage;

        private void OnTriggerEnter(Collider col)
        {
            if(col.tag == "Enemy" || col.tag == "Player")
            {
                SetDamage(col.gameObject.GetComponent<ISetDamage>());   //Нанесение урона

                Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);    //Создание взрыва

                Main.Instance.GetSpawnController.DeleteObject(gameObject);  //Удаление мины из списка

                Destroy(gameObject);
            }
        }

        public void SetDamage(ISetDamage obj)
        {
            if (obj != null)
                obj.ApplyDamage(_damage);
        }
    }
}