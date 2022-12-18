using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    public List<GameObject> garbageList;
    [SerializeField] private int garbageLimit = 10;

    public void AddGarbage (GameObject garbage)
    {
            if (garbageList.Capacity > garbageLimit) { Destroy(garbageList[0]); garbageList.Remove(garbageList[0]); }
            garbageList.Add(garbage);
    }

}
