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
        int _localRange = UnityEngine.Random.Range(0, _resourcesLink.Count);
        int _prefabNumber = UnityEngine.Random.Range(1, Resources.LoadAll<GameObject>(_resourcesLink[_localRange]).Length);
        GameObject temp = Resources.Load<GameObject>(_resourcesLink[_localRange] + _prefabName + _prefabNumber);
        if (CanBeSpawned(temp)) Instantiate(temp, new Vector3(UnityEngine.Random.Range(_spawnRange, -_spawnRange), transform.position.y, 2), Quaternion.identity);
    }
}
