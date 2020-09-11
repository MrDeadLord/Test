using UnityEngine;
using DeadLords.Interface;

namespace DeadLords
{
    public class Box : BaseEnvoirment, ISetDamage
    {
        [SerializeField] private float _hp = 100;
        private bool _isDead = false;
        private float _step = 2f;
        private float _deathTime = 5f;

        private void Update()
        {
            if (_isDead)
            {
                Color color = GetMaterial.color;
                if(color.a > 0)
                {
                    color.a -= _step / 100;

                    Color = color;
                }

                if(color.a < 1)
                {
                    Destroy(_gameObj.GetComponent<BoxCollider>());
                    Destroy(_gameObj, _deathTime);
                }
            }
        }

        public void ApplyDamage(float damage)
        {
            if (_hp > 0)
                _hp -= damage;
            else if(_hp <= 0)
            {
                _hp = 0;
                _isDead = true;
                Color = Color.red;
            }
        }
    }
}