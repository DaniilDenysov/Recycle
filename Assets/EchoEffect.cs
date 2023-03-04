using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    private float time = 0.2f, timeBetweenSpawn = 0, timePassed;
    private string filePath = "Prefabs / YellowBin / New /";
    public GameObject f;

    private void Update()
    {
        timeBetweenSpawn -= Time.deltaTime;
        if (timeBetweenSpawn <= 0)
        {
          Destroy(Instantiate(f,transform.position, transform.rotation),0.5f);
            timeBetweenSpawn = time;
        }
    }
}
