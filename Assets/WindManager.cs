using System.Collections;
using System.Collections.Generic;
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
        StartCoroutine(changeDirection());
        wind =  GetComponentInChildren<PointEffector2D>();
        particles = GetComponentInChildren<ParticleSystem>();
        main = particles.main;
    }

  
    void Update()
    {
        chance = Random.Range(0,100);
    }

    IEnumerator changeDirection ()
    {
        yield return new WaitUntil(() => chance == 50);
        wind.forceMagnitude = wind.forceMagnitude * -1f;
        Debug.Log("Changed");
        main.simulationSpeed = 0;
        StartCoroutine(changeDirection());
    }
}
