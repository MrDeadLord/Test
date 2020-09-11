using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Controller
{
    public class SpawnController : BaseController
    {
        public GameObject _health, _mine;   //Модели аптечки и мины

        private Transform[] _spawnHealthPoints, _spawnMinesPoints;      //Точки спауна аптечек и мин
        private Vector3 _spawnPosition; //Точка спауна аптечки/мины
        private int _index;             //Рандомный индекс
        private GameObject newItem;     //Созданная аптечка/мина
        private List<Vector3> _existingHealthPoints = new List<Vector3>(),
            _existingMinesPoints = new List<Vector3>();   //Позиции, на которых есть аптечки и мины

        private void Start()
        {
            base.On();
            _spawnHealthPoints = Main.Instance.GetOtherStuffCollector.SpawnHealthParrent.GetComponentsInChildren<Transform>();

            _spawnMinesPoints = Main.Instance.GetOtherStuffCollector.SpawnMinesParrent.GetComponentsInChildren<Transform>();

            _health = Main.Instance.GetOtherStuffCollector.Health;
            _mine = Main.Instance.GetOtherStuffCollector.Mine;
        }

        private void Update()
        {
            if (_existingHealthPoints.Count > 1)
                return;
            else
            {
                base.On();
                Spawn(_health);
            }

            if (_existingMinesPoints.Count > 1)
                return;
            else
            {
                base.On();
                Spawn(_mine);
            }

            //Если кол-во аптечек и мин >1 - отключаем контроллер
            if (_existingHealthPoints.Count > 1 && _existingMinesPoints.Count > 1)
                base.Off();
        }

        /// <summary>
        /// Создание аптечек/мин
        /// </summary>
        /// <param name="item">Аптечка или мина</param>
        private void Spawn(GameObject item)
        {
            if (item == _health)
            {
                _index = Random.Range(0, _spawnHealthPoints.Length); //Рандомное число в пределах возможного
                _spawnPosition = _spawnHealthPoints[_index].transform.position; //Предполагаемое место размещения аптечки

                //Если на этой точке уже есть аптечка, то генерится новая точка, где ее нет
                while (_existingHealthPoints.Contains(_spawnPosition))
                {
                    _index = Random.Range(0, _spawnHealthPoints.Length);
                }

                _spawnPosition = _spawnHealthPoints[_index].transform.position; //Место, где появится аптечка

                newItem = Instantiate(_health, _spawnPosition, Quaternion.identity);    //Создание аптечки

                _existingHealthPoints.Add(_spawnPosition);   //Добавление индекса аптечки
            }

            if (item == _mine)
            {
                _index = Random.Range(0, _spawnMinesPoints.Length); //Рандомное число в пределах возможного
                _spawnPosition = _spawnMinesPoints[_index].transform.position; //Предполагаемое место размещения мины

                //Если на этой точке уже есть мина, то генерится новая точка, где ее нет
                while (_existingMinesPoints.Contains(_spawnPosition))
                {
                    _index = Random.Range(0, _spawnMinesPoints.Length);
                }

                _spawnPosition = _spawnMinesPoints[_index].transform.position; //Место, где появится мина

                newItem = Instantiate(_mine, _spawnPosition, Quaternion.identity);    //Создание мины

                _existingMinesPoints.Add(_spawnPosition);   //Добавление индекса мины
            }
        }

        /// <summary>
        /// Вызывается после подбора аптечки или взрыва мины, что бы удалить ее из списка
        /// </summary>
        public void DeleteObject(GameObject item)
        {
            //Если это аптечка, проверяем, есть ли она в списке и удаляем
            if(item == _health && _existingHealthPoints.Contains(item.transform.position))
            {
                _existingHealthPoints.Remove(item.transform.position);
            }

            //Усли же это мина, делаем тоже самое, но для списка мин
            if (item == _mine && _existingMinesPoints.Contains(item.transform.position))
            {
                _existingMinesPoints.Remove(item.transform.position);
            }
        }
    }
}