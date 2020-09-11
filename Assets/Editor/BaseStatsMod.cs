using UnityEngine;
using UnityEditor;
using DeadLords;

[CustomEditor(typeof(BaseStats))]
public class BaseStatsMod : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BaseStats test = (BaseStats)target;

        string path = Application.dataPath + "/" + "SavedData.Xml";

        GUILayout.BeginHorizontal();
        bool saveButton = GUILayout.Button("Save");
        bool loadButton = GUILayout.Button("Load");
        GUILayout.EndHorizontal();

        if (saveButton)
        {
            var saver = new SaveLoad();

            saver.Save(test, path);

            Debug.Log("saved. " + path);
        }
        
        if (loadButton)
        {
            var loader = new SaveLoad();

            test.agility = loader.Load(path).agility;

            Debug.Log("Loaded");
            Debug.Log(test.agility);
        }
    }
}
