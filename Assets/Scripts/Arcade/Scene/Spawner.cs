using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   
    [SerializeField] public GameObject[] trash;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private DefeatManager defeatManager;
    [SerializeField] private GameObject[] Buff;
    [SerializeField] private float range, spawnRate = 2f;


    void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        defeatManager = FindObjectOfType<DefeatManager>();
        StartCoroutine(Spawn(spawnRate));
        InstantiateBuff();
        StartCoroutine(SpawnBuff(30));
    }
    public void InstantiateBuff()
    {
        Instantiate(Buff[Random.Range(0, Buff.Length - 1)], new Vector2(14, 8), Quaternion.identity);
    }
    public void InstantiateGarbage(int amount)
    {
        if (!pauseManager.Paused && !defeatManager.Lost)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(trash[Random.Range(0,trash.Length - 1)], new Vector3(Random.Range(range / 2, -range / 2), transform.position.y, 2), Quaternion.identity).gameObject.layer = 12;
            }
        }
    }
    IEnumerator Spawn(float Time)
    {
        yield return new WaitForSeconds(Time);
        InstantiateGarbage(1);
        StartCoroutine(Spawn(Time));
    }
    IEnumerator SpawnBuff(int Time)
    {
        yield return new WaitForSeconds(Time);
        InstantiateBuff();
        StartCoroutine(SpawnBuff(Time));
    }
}
