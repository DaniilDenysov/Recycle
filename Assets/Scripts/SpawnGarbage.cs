using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGarbage : Spawn
{
    public override void Start()
    {
        RemoveExcess();
        base.Start();       
    }

    private void RemoveExcess ()
    {
        _resourcesLink.RemoveRange(BinChanger.instance.GetCopacity(), _resourcesLink.Count - BinChanger.instance.GetCopacity());
    }
}


