#if UNITY_EDITOR
 
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(EnemieBase))]
public class Car_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemieBase _script = (EnemieBase)target;
        if (GUILayout.Button("Play a Turn"))
        {
            _script.Action();
        }
    }
}
#endif
