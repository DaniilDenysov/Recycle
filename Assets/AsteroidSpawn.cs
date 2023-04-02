using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : Spawn
{
    public override void Start()
    {
        base.Start();
    }
    public override void SpawnObject()
    {
       /* GameObject temp = _cashedList[UnityEngine.Random.Range(0, _cashedList.Count)];
        if (CanBeSpawned(temp))
        {
            GameObject spawned = Instantiate(temp, new Vector3(Random.Range(-_spawnRange,_spawnRange),
            transform.position.y, 2), Quaternion.identity);
            spawned.GetComponent<Asteroid>().SetAngle(Random.Range(10,90));
        }*/
    }
}
