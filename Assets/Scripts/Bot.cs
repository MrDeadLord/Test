using DeadLords.Interface;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace DeadLords
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AICharacterControl))]
    public class Bot : MonoBehaviour, ISetDamage
    {
        [SerializeField] private float _hp = 100;
        private bool _isDead = false;
        public NavMeshAgent agent { get; private set; }
        [HideInInspector] public int walkSpaceAreaMask;   //Зона, по которой будет перемещаться бот. Задается BotsController-ом
        private AICharacterControl agentsAI;
        private float _curTime; //Для счетчика ожидания
        [Space(10)] [SerializeField] [Tooltip("Время ожидания на точке(при потрулировании)")] private float _timeWait = 3;
        [SerializeField] [Tooltip("Дистанция реагирования")] private float _activeDist = 30;
        [SerializeField] [Tooltip("Угол реагирования")] private float _activeAngle = 70;
        [Space(10)] [SerializeField] [Tooltip("Игрок")] private Transform _target;
        [SerializeField] [Tooltip("Оружие бота")] private Weapons _weapon;
        [SerializeField] [Tooltip("Патроны оружия")] private Ammunition _ammo;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agentsAI = GetComponent<AICharacterControl>();
        }

        void Update()
        {
            //Если NPC мертв, дальше ничего не считаем
            if (_isDead)
            {
                Dying();
                return;
            }

            float dist = Vector3.Distance(transform.position, _target.position);    //Расстояние до игрока

            if (agentsAI.target == null)
                GenerateWayPoint();   //Если нет цели, генерируем рандомный путь

            //Если расстояние в пределах реакции - идем дальше
            if (dist <= _activeDist)
            {
                if (!IsBlocked() && TargetOnSightAngle())
                {
                    agentsAI.SetTarget(_target);
                    _weapon.Shoot(_ammo);
                }
            }
        }

        /// <summary>
        /// Случайная генерация точек для пути NPC
        /// </summary>
        private void GenerateWayPoint()
        {
            _curTime += Time.deltaTime;

            if (_curTime > _timeWait)
            {
                _curTime = 0;

                Vector3 _wayPoint = Random.insideUnitCircle * 20;
                NavMeshHit navMeshHit;
                NavMesh.SamplePosition(transform.position + _wayPoint, out navMeshHit, 20, NavMesh.AllAreas);
                agent.SetDestination(navMeshHit.position);
            }
        }

        /// <summary>
        /// Проверка находится ли игрок в поле зрения врага
        /// </summary>
        /// <returns>Находится(true)/Не находится(false)</returns>
        private bool TargetOnSightAngle()
        {
            if (Vector3.Angle(transform.position, _target.position) > _activeAngle ||
                Vector3.Angle(transform.position, _target.position) < -_activeAngle)
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка не заблокирован ли путь к игроку
        /// </summary>
        /// <returns>true если заблокирован, false - если нет</returns>
        private bool IsBlocked()
        {
            RaycastHit hit;
            Debug.DrawLine(transform.position, _target.position, Color.red);

            if (Physics.Linecast(transform.position, _target.position, out hit))
                if (hit.transform == _target)
                    return false;

            return true;
        }

        /// <summary>
        /// Смерть
        /// </summary>
        private void Dying()
        {
            Destroy(gameObject);
            Main.Instance.GetBotsControlCenter.RemoveBot(gameObject.GetComponent<Bot>());
        }

        public void ApplyDamage(float damage)
        {
            if (_hp <= 0)
                _isDead = true;
            else
                _hp -= damage;
        }

        #region Для редактора

        /// <summary>
        /// Получение времени, дистанции и угла активации бота
        /// </summary>
        public float[] GetActiveTimeDistAngle
        {
            get
            {
                float[] names = { _timeWait, _activeDist, _activeAngle};
                return names;
            }
        }
        #endregion
    }
}