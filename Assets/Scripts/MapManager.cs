using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapManager : MonoBehaviour
{
    public static MapManager instance { get; set; }

   
    [SerializeField] private List<Map> t;

    public Color [] background;
    [SerializeField] private GameObject[] render;
    private GameObject currentEnvironment;
    public int MapID;

    private void Awake()
    {
        instance = this;
    }


    [EditorCools.Button(name: "Second Button")]
    private void NameDoesnotMatter() => Debug.Log("Second Button Clicked!");

    void Start()
    {
        if (!PlayerPrefs.HasKey("Season") || PlayerPrefs.GetInt("Season") == 4) MapID = Random.Range(0, background.Length);
        else MapID = PlayerPrefs.GetInt("Season");
        Camera.main.backgroundColor = background[MapID];
        render[MapID].SetActive(true);
    }

    public void SwitchBackground (Color color)
    {
        Camera.main.backgroundColor = color;
    }
    
    public void SwitchEnvironment(GameObject environment)
    {
        currentEnvironment?.SetActive(false);
        currentEnvironment = environment;
        currentEnvironment.SetActive(true);
    }
    public void SwitchMapID (int ID)
    {
        MapID = ID;
    }
}

