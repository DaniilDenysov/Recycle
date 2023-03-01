using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGarbage : MonoBehaviour
{

    [System.Serializable]
    public struct Garbage
    {
        [SerializeField] public GameObject [] trash;     
    }

    [System.Serializable]
    public struct newGarbage
    {
        [SerializeField] public List <GameObject> trash;
    }

    public List <Garbage> list;
    public List<newGarbage> nlist; 
    public string [] newList;

    [SerializeField] private GameObject [] Buff;
    [SerializeField] private float range;
    private GarbageCollector garbageCollector;
    private int spawnRange, localRange;



    void Start()
    {

        //StartCoroutine(Spawn(2));
          InvokeRepeating(nameof(InstantiateGarbage),2f,2f);
        // spawnFromPool();
        //InvokeRepeating(nameof(spawnFromPool),2,2);
        //cachePrefabs(3);
       // InstantiateBuff();
       // StartCoroutine(SpawnBuff(30));
        
    }
  /*  public void spawnFromPool ()
    {
        localRange = Random.Range(0, changer.Copacity);
        int toMove = Random.Range(0, list[localRange].trash.Length - 1);
        while (list[localRange].trash[toMove].active)
        {
            toMove = Random.Range(0, list[localRange].trash.Length); 
        }
        Debug.Log("LR:" + localRange + "TM:" + toMove);
        takeFromPool(localRange, toMove, new Vector3(Random.Range(range / 2, -range / 2), transform.position.y, 2),Quaternion.identity);
    }*/

    public void takeFromPool(int element, int i, Vector3 newPosition, Quaternion newQuaternion)
    {
        Debug.Log(list[element].trash[i].name);
        list[element].trash[i].transform.position = newPosition;
        list[element].trash[i].transform.rotation = newQuaternion;
        list[element].trash[i].SetActive(true);
    }

  /*  void cachePrefabs (int amount)
    {
        for (int i = 0;i<amount;i++)
        {
            nlist.Add(new newGarbage());
            for (int m = 1;m< Resources.LoadAll<GameObject>(newList[i]).Length;m++)
            {
                GameObject item = Resources.Load<GameObject>(newList[i] + "Prefab" + m);
                Debug.Log(item.name);
                nlist[i].trash = new List<GameObject>().Add(Resources.Load<GameObject>(newList[i] + "Prefab1"));
            }
            //nlist[i].trash.Add(Resources.Load<GameObject>(newList[i] + "Prefab1"));
           // nlist[i].trash.AddRange(Resources.LoadAll<GameObject>(newList[i]));
           // nlist[i].trash = new List<GameObject>(Resources.LoadAll<GameObject>(newList[i]));
        }
    }*/
    public void InstantiateBuff ()
    {
        Instantiate(Buff[Random.Range(0,Buff.Length -  1)], new Vector2(14,8), Quaternion.identity);
    }
    public void InstantiateGarbage ()
    {
     
                localRange = Random.Range(0,BinChanger.instance.GetCopacity());
              //  Debug.Log(spawnRange + "    Path: " + Resources.Load<GameObject>(newList[localRange] + "Prefab" + Random.Range(1, Resources.LoadAll <GameObject>(newList[localRange]).Length)));
          Instantiate(Resources.Load<GameObject>(newList[localRange] + "Prefab" + Random.Range(1, Resources.LoadAll<GameObject>(newList[localRange]).Length)), new Vector3(Random.Range(range / 2, -range / 2), transform.position.y, 2), Quaternion.identity);
       // Instantiate(_list[localRange][Random.Range(0,_list[localRange].Length)], new Vector3(Random.Range(range / 2, -range / 2), transform.position.y, 2), Quaternion.identity);
    }
    IEnumerator Spawn (int Time)
    {
        yield return new WaitForSeconds(Time);
        InstantiateGarbage();
        StartCoroutine(Spawn(Time));
    }
    IEnumerator SpawnBuff(int Time)
    {
        yield return new WaitForSeconds(Time);
        InstantiateBuff();
        StartCoroutine(SpawnBuff(Time));
    }
}


