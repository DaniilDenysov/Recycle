using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{

    [SerializeField] private ParticleSystem[] fallouts;
    [SerializeField] private float Min = 0.5f, Max, actualMax;
    [SerializeField] private AnimationCurve animationCurve, animationCurveTime;
    int _Season;
    bool isChanging;
    private bool increases, decreases;
    [SerializeField] private float time,interval;
    float i = 0;
    int direction;
    void Start()
    {
        if (fallouts[_Season] != null)
        {
            _Season = FindObjectOfType<MapManager>().MapID;
            Debug.Log("Season: " + _Season);
            ParticleSystem.MainModule main = fallouts[_Season].main;
            InvokeRepeating("ChangeWeather", interval, interval);
        }
        else
        {
            Destroy(this);
        }      
    }


    void FixedUpdate()
    {
        if (isChanging)
        {
            var main = fallouts[_Season].main;
            if (main.simulationSpeed != Max)
            {
                main.simulationSpeed += direction * 0.0001f;  
            }
        }
      /*  i = Mathf.SmoothStep(0, 100, 10000);
        Debug.Log("Lerp: " + i);
        if (isChanging)
        {
            var main = fallouts[_Season].main;
            if (main.simulationSpeed != actualMax)
            {
                main.simulationSpeed = Mathf.Lerp(main.simulationSpeed, actualMax,100);
                Debug.Log("Speed: " + main.simulationSpeed + " Actual max: " + actualMax);
            }
            if (main.simulationSpeed == actualMax) isChanging = false;
        }*/
        /*     if (main.simulationSpeed != Max || main.simulationSpeed != Min)
          {
              if (isChanging)
              {
                 // var main = fallouts[_Season].main;
                  Debug.Log(main.simulationSpeed);
                  main.simulationSpeed = Mathf.Clamp(Min, Max, animationCurveTime.Evaluate(Max));
                  if (main.simulationSpeed == Max) isChanging = false;

              }
              else
              {

              }
          }*/
    }

    public void ChangeWeather ()
    {
        var main = fallouts[_Season].main;
        
       /// main.simulationSpeed = animationCurve.Evaluate(Random.Range(Min,Max));
      //  Min = main.simulationSpeed;
        Max = Random.Range(0.1f,1.1f);
        if (main.simulationSpeed > Max)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        isChanging = true;
    }

}
