using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGarbage : MonoBehaviour
{

    [System.Serializable]
    public struct Garbage
    {
        [SerializeField] public int layer;
        [SerializeField] public GameObject [] trash;     
    }
    public List <Garbage> List;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private DefeatManager defeatManager;
    [SerializeField] private BinChanger changer;
    [SerializeField] private GameObject [] Buff;

    void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        defeatManager = FindObjectOfType<DefeatManager>();
        StartCoroutine(Spawn(2));
        InstantiateBuff();
        StartCoroutine(SpawnBuff(30));
    }
    public void InstantiateBuff ()
    {
        Instantiate(Buff[Random.Range(0,Buff.Length -  1)], new Vector2((float)Random.Range(GetComponent<BoxCollider2D>().size.x/2, -GetComponent<BoxCollider2D>().size.x/2), transform.position.y), Quaternion.identity);
    }
    public void InstantiateGarbage (int amount)
    {
        if (!pauseManager.Paused && !defeatManager.Lost)
        {
            for (int i = 0; i < amount; i++)
            {
                Debug.Log("Count:" + changer.Copacity);
                int Rand = Random.Range(0, changer.Copacity);
                Instantiate(List[Rand].trash[Random.Range(0, List[Rand].trash.Length - 1)], new Vector3((float)Random.Range(-GetComponent<BoxCollider2D>().size.x/2, GetComponent<BoxCollider2D>().size.x/2), transform.position.y,2), Quaternion.identity);               
            }
        }
    }
    IEnumerator Spawn (int Time)
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


