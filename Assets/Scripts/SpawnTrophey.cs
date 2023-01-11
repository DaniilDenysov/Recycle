using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnTrophey : MonoBehaviour
{
    public BoxCollider2D spawnRange;
    private System.Random random = new System.Random();

    [Range(0, 100f)] public float spawnProbability = 50f;

    private float totalProbability = 0f;

    [System.Serializable]
    public class Trophey
    {
        public GameObject prefab;
        [Range(0, 100f)] public float probability = 50f;
        public double weight;
    }

    [SerializeField] private Trophey [] tropheys;

    private void Start()
    {
        InvokeRepeating(nameof(spawnTrophey),10,10);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            spawnTrophey();
        }
    }

    public void spawnTrophey ()
    {
        float rand = Random.Range(0, 101);
        Debug.Log("Spawn: " + rand);
        if (rand <= spawnProbability) Instantiate(tropheys[getIndex()].prefab,new Vector3(Random.Range(spawnRange.size.x / 2, -spawnRange.size.x / 2),transform.position.y, 2),Quaternion.identity);
    }
    public void calculateProbability()
    {

        totalProbability = 0f;
        foreach (Trophey trophey in tropheys)
        {
            totalProbability += trophey.probability;
            trophey.weight = totalProbability;
        }
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
