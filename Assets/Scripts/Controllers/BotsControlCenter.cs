using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Controller
{
    public class BotsControlCenter : MonoBehaviour
    {
        private List<Bot> _botsList = new List<Bot>();

        private int _walkArea = 3;  //Первая пользовательская зона, где будет находиться бот
        void Start()
        {
            Init();
        }

        /// <summary>
        /// Работа со списком ботов в и извне класса
        /// </summary>
        public List<Bot> GetBotsList
        {
            get { return _botsList; }
            set { _botsList = value; }
        }

        /// <summary>
        /// Расставление приоритетов и зон перемещения ботов
        /// </summary>
        public void Init()
        {
            _botsList = Main.Instance.GetOtherStuffCollector.BotsList;

            int i = -1;

            //Первого бота добавляем в первую зону перемещения, остальных - в пользовательские
            foreach (var tempBot in GetBotsList)
            {
                tempBot.agent.avoidancePriority = ++i;
                
                if (i == 0)
                    tempBot.walkSpaceAreaMask = i;
                else
                {
                    tempBot.walkSpaceAreaMask = _walkArea;
                    _walkArea++;
                }                    
            }
        }

        /// <summary>
        /// Добавление бота в список ботов
        /// </summary>
        /// <param name="bot">Бот со скриптом Bot</param>
        public void AddBot(Bot bot)
        {
            if (!GetBotsList.Contains(bot))
                GetBotsList.Add(bot);
        }

        /// <summary>
        /// Удаление бота из списка ботов
        /// </summary>
        /// <param name="bot">Удаляемый бот</param>
        public void RemoveBot(Bot bot)
        {
            if (GetBotsList.Contains(bot))
                GetBotsList.Remove(bot);
        }
    }
}