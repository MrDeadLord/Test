using UnityEngine;
using UnityEditor;
using DeadLords;

[CustomEditor(typeof(TestLoader))]
public class TestLoadInfoModif : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TestLoader test = (TestLoader)target;

        string path = Application.dataPath + "/" + "TestInfo.txt";

        GUILayout.BeginHorizontal();
        bool saveButton = GUILayout.Button("Save");
        bool loadButton = GUILayout.Button("Load");
        GUILayout.EndHorizontal();

        if (saveButton)
        {
            TestLoader saver = new TestLoader();
            TestLoader.Data name = new TestLoader.Data();

            saver.Save(name, path);

            Debug.Log("saved. " + path);
        }

        if (loadButton)
        {
            TestLoader loader = new TestLoader();

            Debug.Log("Loaded");
            Debug.Log(loader.Load(path).anotherShit);
        }
    }
}
