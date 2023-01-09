using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnTrophey : MonoBehaviour
{
    public BoxCollider2D spawnRange;
    private System.Random random = new System.Random();

    private float totalProbability = 0f;

    [System.Serializable]
    public class Trophey
    {
        public GameObject prefab;
        [Range(0, 100f)] public float probability = 50f;
        public double weight;
    }

    [SerializeField] private Trophey [] tropheys;

    private void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            spawnTrophey();
        }
    }

    public void spawnTrophey ()
    {
        Instantiate(tropheys[getIndex()].prefab,new Vector3(Random.Range(spawnRange.size.x / 2, -spawnRange.size.x / 2),transform.position.y, 2),Quaternion.identity);
    }
    public void calculateProbability()
    {

        totalProbability = 0f;
        foreach (Trophey trophey in tropheys)
        {
            totalProbability += trophey.probability;
            trophey.weight = totalProbability;
        }
        /* float newProbability = 0f;
         Debug.Log("Count: " + tropheys.Count);
         for (int i = 0;i<tropheys.Count;i++)
             newProbability += tropheys[i].probability;

         Debug.Log("Prob: " + newProbability);
         return newProbability;*/
    }
    public int getIndex ()
    {
        calculateProbability();
        double rand = random.NextDouble() * totalProbability;
        int localIndex = 0;
        Debug.Log("Rand: " + rand);
        for (int i = 0; i < tropheys.Length; i++)
            if (rand <= tropheys[i].weight) return i;

        return 0;
    }
}
