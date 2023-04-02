using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    [Range(-1,1)]
    [SerializeField] private int direction;
    [SerializeField] private AnimationCurve windStrenght;
    private PointEffector2D wind;
    private ParticleSystem particles;
    private float chance;
    private ParticleSystem.MainModule main;

    void Start()
    {
     
        wind =  GetComponentInChildren<PointEffector2D>();
        particles = GetComponentInChildren<ParticleSystem>();
        main = particles.main;
        Thread newThread = new Thread(new ThreadStart(Update));
    }

  
    void Update()
    {
        chance = Random.Range(0,100);
        if (chance == 50)
        {
            wind.forceMagnitude = wind.forceMagnitude * -1f;
            Debug.Log("Changed");
            main.simulationSpeed = 0;
        }
    }
}
