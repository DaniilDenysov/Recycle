using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapManager : MonoBehaviour
{
    public static MapManager instance { get; set; }   
   [SerializeField] private List<Map> _map;
    private int _mapID;

    private void Awake()
    {
        instance = this;
    }



    void Start()
    {
        if (!PlayerPrefs.HasKey("Season") || PlayerPrefs.GetInt("Season") == 4) _mapID = Random.Range(0, _map.Count);
        else _mapID = PlayerPrefs.GetInt("Season");
        SwitchEnvironment();
    }

    public int GetMapID() => _mapID;

    public void SwitchEnvironment()
    {
        Debug.Log("Map:" + _mapID);
        _map[_mapID].GetEnvironment().SetActive(true);
        Camera.main.backgroundColor = _map[_mapID].GetBackgroundColor();
    }

    [System.Serializable]
    class Map
    {
        [SerializeField] private GameObject _environment;
        [SerializeField] private Color _backgroundColor;

        public GameObject GetEnvironment() => _environment;
        public Color GetBackgroundColor() => _backgroundColor;
    }
}

