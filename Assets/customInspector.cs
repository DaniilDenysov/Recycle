using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapManager))]
public class customInspector : Editor
{
  /*  public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MapManager map = (MapManager)target;
        if (GUILayout.Button("Switch map"))
        {
            map.SwitchMap();
        }
    }*/
}
