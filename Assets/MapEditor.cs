using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    public static MapEditor instance { get; set; }


    public List<Map> t;

    private GameObject currentEnvironment;
    public int MapID;

    private void Awake()
    {
        instance = this;
    }


    [EditorCools.Button(name: "Second Button")]
    public void NameDoesnotMatter() => Debug.Log("Second Button Clicked!");

    void Start()
    {
        if (!PlayerPrefs.HasKey("Season") || PlayerPrefs.GetInt("Season") == 4)
        {

        }
        else MapID = PlayerPrefs.GetInt("Season");
  
    }

    public void SwitchBackground(Color color)
    {
        Camera.main.backgroundColor = color;
    }

    public void SwitchEnvironment(GameObject environment)
    {
        currentEnvironment?.SetActive(false);
        currentEnvironment = environment;
        currentEnvironment.SetActive(true);
    }
    public void SwitchMapID(int ID)
    {
        MapID = ID;
    }
}
