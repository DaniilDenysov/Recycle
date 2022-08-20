using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherChanger : MonoBehaviour
{


    [SerializeField] private SpriteRenderer Ground;
    [SerializeField] private Text Results, Record;
    [SerializeField] private Spawn spawn;
    [SerializeField] private ParticleSystem[] Weather;
    [SerializeField] private Color[] MapColor,TextColor;
    [SerializeField] private Vector2[] RandomBetweenTwoRates, RandomBetweenTwoTimes;
    float Rate, TimeToChange;
    
    void Start()
    {
        Results.color = TextColor[spawn.Map];
        Record.color = TextColor[spawn.Map];
        Ground.color = MapColor[spawn.Map];
        
        if (spawn.Map < 6)
        {
            ChangeRate();
            Weather[spawn.Map].GetComponent<ParticleSystem>().emissionRate = Rate;
            Debug.Log("Rate:" + Rate);
            InvokeRepeating("RandomChangeOfWeather", 0.1f, TimeToChange);
            Debug.Log("TimeToChange:" + TimeToChange);
        }
    }
    public void RandomChangeOfWeather ()
    {
        Weather[spawn.Map].GetComponent<ParticleSystem>().emissionRate = Rate;
        ChangeRate();
    }
    public void ChangeRate ()
    {
        TimeToChange = Random.Range(RandomBetweenTwoTimes[spawn.Map].x, RandomBetweenTwoTimes[spawn.Map].y);
        Rate = Random.Range(RandomBetweenTwoRates[spawn.Map].x, RandomBetweenTwoRates[spawn.Map].y);
        Debug.Log("Rate:" + Rate);
        Debug.Log("TimeToChange:" + TimeToChange);
    }

}
