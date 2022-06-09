#if UNITY_EDITOR
 
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(EnemieRange))]
public class Range_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemieRange _script = (EnemieRange)target;
        if (GUILayout.Button("Play a Turn"))
        {
            _script.Action();
        }
    }
}
#endif
