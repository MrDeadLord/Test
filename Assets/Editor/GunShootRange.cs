using DeadLords;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Gun))]
public class GunShootRange : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Weapons weap = (Weapons)target;

        bool button = GUILayout.Button("Выстрел");

        if (button)
        {
            RaycastHit hit;
            Transform _barrel = weap.GetBarrel;

            Ray _ray = new Ray(_barrel.position, _barrel.forward);
            Physics.Raycast(_ray, out hit);

            if (hit.collider)
            {
                Debug.DrawLine(_barrel.position, hit.point, Color.red);
                Debug.Log("Hit " + hit.collider.name);
            }
            else
            {
                Debug.DrawRay(_barrel.position, _barrel.forward, Color.green, 10);
            }
        }
    }
}
