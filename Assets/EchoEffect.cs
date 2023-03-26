using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    private float time = 0.1f, timeBetweenSpawn = 0, timePassed;
    private string filePath = "Prefabs / YellowBin / New /";
    public GameObject f;

    private void Update()
    {
        timeBetweenSpawn -= Time.deltaTime;
        if (timeBetweenSpawn <= 0)
        {
            GameObject t = Instantiate(f, transform.position, transform.rotation);
            t.transform.localScale = transform.localScale;
            Destroy(t,0.5f);
            timeBetweenSpawn = time;
        }
    }
}
