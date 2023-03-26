using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnTrophey : Spawn
{
    public override void Start()
    {
        base.Start();
    }

    public override bool CanBeSpawned(GameObject prefab)
    {
        float rand = Random.Range(0,101);
        Debug.Log(prefab.name);
        Trophey trophey = prefab.GetComponent<Trophey>();
        Debug.Log(trophey.name);
        if (rand <= trophey.GetProbability() && !DataManager.instance.alreadyExists(trophey.GetID())) return true;
        return false;
    }
}
