using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Map
{
    [Range(1, 10)]
    [SerializeField] private int MapID;
    [SerializeField] private Color background;
    [SerializeField] private GameObject Environment;

    [EditorCools.Button(name: "Second Button")]
    public void SwitchMap()
    {
        MapManager.instance.SwitchBackground(background);
        MapManager.instance.SwitchEnvironment(Environment);
        MapManager.instance.SwitchMapID(MapID);
    }
}
