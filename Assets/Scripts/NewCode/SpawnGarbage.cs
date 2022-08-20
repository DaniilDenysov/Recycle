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

    void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        defeatManager = FindObjectOfType<DefeatManager>();
        StartCoroutine(Spawn(5));
    }
    public void InstantiateGarbage (int amount)
    {
        if (!pauseManager.Paused && !defeatManager.Lost)
        {
            for (int i = 0; i < amount; i++)
            {
                Debug.Log("Count:" + changer.Copacity);
                int Rand = Random.Range(0, changer.Copacity);
                Instantiate(List[Rand].trash[Random.Range(0, List[Rand].trash.Length - 1)], new Vector3((float)Random.Range(-GetComponent<BoxCollider2D>().size.x, GetComponent<BoxCollider2D>().size.x), transform.position.y,2), Quaternion.identity);               
            }
        }
    }
    IEnumerator Spawn (int Time)
    {
        yield return new WaitForSeconds(Time);
        InstantiateGarbage(1);
        StartCoroutine(Spawn(Time));
    }
}


