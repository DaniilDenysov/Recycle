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
    public string [] newList;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private DefeatManager defeatManager;
    [SerializeField] private BinChanger changer;
    [SerializeField] private GameObject [] Buff;
    [SerializeField] private float range;
    private GarbageCollector garbageCollector;
    private int spawnRange, localRange;


    void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        defeatManager = FindObjectOfType<DefeatManager>();
        garbageCollector = FindObjectOfType<GarbageCollector>();
        StartCoroutine(Spawn(2));
        InstantiateBuff();
        StartCoroutine(SpawnBuff(30));
        spawnRange = GetComponent<BinChanger>().Copacity;
    }
    public void InstantiateBuff ()
    {
        Instantiate(Buff[Random.Range(0,Buff.Length -  1)], new Vector2(14,8), Quaternion.identity);
    }
    public void InstantiateGarbage (int amount)
    {
        if (!pauseManager.Paused && !defeatManager.Lost)
        {
            for (int i = 0; i < amount; i++)
            {
                /*  Debug.Log("Count:" + changer.Copacity);
                  int Rand = Random.Range(0, changer.Copacity);
                  Instantiate(List[Rand].trash[Random.Range(0, List[Rand].trash.Length - 1)], new Vector3(Random.Range(range / 2, -range / 2), transform.position.y, 2), Quaternion.identity);*/

                localRange = Random.Range(0, spawnRange);
                Instantiate(Resources.Load<GameObject>(newList[localRange] + "Prefab" + Random.Range(1, Resources.LoadAll<GameObject>(newList[localRange]).Length)), new Vector3(Random.Range(range / 2, -range / 2), transform.position.y, 2), Quaternion.identity);
              
                //  garbageCollector.AddGarbage(Instantiate(List[Rand].trash[Random.Range(0, List[Rand].trash.Length - 1)], new Vector3(Random.Range(range / 2, -range / 2), transform.position.y,2), Quaternion.identity));               
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


