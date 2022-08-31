using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{

    [SerializeField] private ParticleSystem[] fallouts;
    [SerializeField] private float Min = 0.5f, Max;
    int _Season;
    bool isChanging;
    [SerializeField] private float time,interval; 

    void Start()
    {
        if (fallouts[_Season] != null)
        {
            _Season = FindObjectOfType<MapManager>().Map;
            Debug.Log("Season: " + _Season);
            ParticleSystem.MainModule main = fallouts[_Season].main;
            InvokeRepeating("ChangeWeather", interval, interval);
        }
        else
        {
            Destroy(this);
        }      
    }


    void Update()
    {
        if (isChanging)
        {
            var main = fallouts[_Season].main;
            Debug.Log(main.simulationSpeed);
            main.simulationSpeed = Mathf.Clamp(Min,Max,time);
            if (main.simulationSpeed == Max) isChanging = false;

        }
    }

   public void ChangeWeather ()
    {
        var main = fallouts[_Season].main;
        Min = main.simulationSpeed;
        Max = Random.Range(0.1f,1.1f);
        isChanging = true;
    }

}
