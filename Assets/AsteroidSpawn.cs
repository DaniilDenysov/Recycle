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
        GameObject temp = _cashedList[UnityEngine.Random.Range(0, _cashedList.Count)];
        if (CanBeSpawned(temp)) Instantiate(temp, new Vector3(_spawnRange,
            transform.position.y, 2), Quaternion.identity);
    }
}
