using UnityEngine;

namespace DeadLords
{
    public class BaseStats : MonoBehaviour
    {
        [Tooltip("Здоровье")] public float hp;
        [Tooltip("Броня")] public float armor;
        [Tooltip("Скорость передвижения")] public float moveSpeed;

        [Tooltip("Сила")] public float strengh;
        [Tooltip("Ловкость")] public float agility;
        [Tooltip("Интелект")] public float intelligence;

        [Tooltip("Инициатива")] public float initiative;
    }
}