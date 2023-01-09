using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnTrophey : MonoBehaviour
{
    public BoxCollider2D spawnRange;
    private System.Random random;

    [System.Serializable]
    public class Trophey
    {
        public GameObject prefab;
        [Range(0, 100f)] public float probability = 50f;
    }

    [SerializeField] private List<Trophey> tropheys;

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            spawnTrophey();
        }
    }

    public void spawnTrophey ()
    {
        Instantiate(tropheys[getIndex()].prefab,new Vector3(Random.Range(spawnRange.size.x / 2, -spawnRange.size.x / 2),transform.position.y, 2),Quaternion.identity);
    }
    public float calculateProbability()
    {
        float newProbability = 0f;
        for (int i = 0;i<tropheys.Count;i++)
            newProbability += tropheys[i].probability;

        return newProbability;
    }
    public int getIndex ()
    {
        double rand = random.NextDouble() * calculateProbability();
        for (int i = 0;i<tropheys.Count;i++)
            if (rand <= tropheys[i].probability) return i;

        return 0;
    }
}
