using System.IO;
using UnityEngine;

namespace DeadLords
{
    public class TestLoader : MonoBehaviour
    {
        public void Save(Data info, string savePath)
        {
            string str = JsonUtility.ToJson(info);
            //str = Crypt(str);
            File.WriteAllText(savePath, str);
        }

        public Data Load(string savePath)
        {
            string str = File.ReadAllText(savePath);
            //str = Crypt(str);

            Data stats = JsonUtility.FromJson<Data>(str);
            return stats;
        }

        public string Crypt(string text)
        {
            string result = string.Empty;

            foreach (char c in text)
            {
                result += (char)(c ^ 27);
            }

            return result;
        }

        public class Data
        {
            public string name = "Belarus doomed";
            public string anotherShit = "DOOMED";
            [SerializeField] private string someShit = "striiiiptiz";

            public string GetName { get { return name; } }

            public void DoSome()
            {
                Debug.Log("TestLoader trick");
            }
        }
    }
}