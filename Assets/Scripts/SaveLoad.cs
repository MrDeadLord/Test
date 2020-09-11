using System.Xml;
using UnityEngine;

namespace DeadLords
{
    public class SaveLoad : MonoBehaviour
    {
        //string savePath = Application.dataPath +"/"+ "SaveData";  //Пример savePath

        /// <summary>
        /// Сохранение данных
        /// </summary>
        /// <param name="_baseStats">Скрипт хар-тик персонажа</param>
        /// <param name="savePath">Путь сохранения файла с именем файла</param>
        public void Save(BaseStats _baseStats, string savePath)
        {
            XmlDocument saveDoc = new XmlDocument();

            XmlNode rootNode = saveDoc.CreateElement("BaseStats");
            saveDoc.AppendChild(rootNode);

            XmlElement element;
            element = saveDoc.CreateElement("Strengh");
            element.SetAttribute("value", _baseStats.strengh.ToString());
            rootNode.AppendChild(element);

            element = saveDoc.CreateElement("Agility");
            element.SetAttribute("value", Crypt(_baseStats.agility.ToString()));
            rootNode.AppendChild(element);

            element = saveDoc.CreateElement("Intelligence");
            element.SetAttribute("value", _baseStats.intelligence.ToString());
            rootNode.AppendChild(element);

            element = saveDoc.CreateElement("Initiative");
            element.SetAttribute("value", _baseStats.initiative.ToString());
            rootNode.AppendChild(element);

            XmlNode infoNode;
            XmlAttribute attribute;
            infoNode = saveDoc.CreateElement("Info");
            attribute = saveDoc.CreateAttribute("Unity");
            attribute.Value = Application.unityVersion;
            infoNode.Attributes.Append(attribute);
            infoNode.InnerText = "CompanyName:" + Application.companyName;
            rootNode.AppendChild(infoNode);

            saveDoc.Save(savePath);
        }

        /// <summary>
        /// Загрузка файла и возврат списка параметров в BaseStats
        /// </summary>
        /// <param name="savePath">Путь файла загрузки</param>
        /// <returns></returns>
        public BaseStats Load(string savePath)
        {
            try
            {
                BaseStats loadedStats = new BaseStats();
                XmlTextReader reader = new XmlTextReader(savePath);

                while (reader.Read())
                {
                    if (reader.IsStartElement("Strengh"))
                        loadedStats.strengh = float.Parse(reader.GetAttribute("value"));

                    if (reader.IsStartElement("Agility"))
                        loadedStats.agility = float.Parse(Crypt(reader.GetAttribute("value")));

                    if (reader.IsStartElement("Intelligence"))
                        loadedStats.intelligence = float.Parse(reader.GetAttribute("value"));

                    if (reader.IsStartElement("Initiative"))
                        loadedStats.intelligence = float.Parse(reader.GetAttribute("value"));
                }

                reader.Close();

                return loadedStats;
            }
            catch (System.Exception)
            {
                Debug.LogError("Ошибка чтения файла");
                return null;
            }
        }

        public string Crypt(string text)
        {
            string result = string.Empty;

            foreach (char c in text)
                result += (char)(c ^ 27);

            return result;
        }
    }
}