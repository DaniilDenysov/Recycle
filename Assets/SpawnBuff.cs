using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuff : Spawn
{

    public override void Start()
    {
      //  Effect.onEffectStarted += Effect_onEffectStarted;
        //Effect.onEffectStopped += Effect_onEffectStopped;
        base.Start();
    }

    /* private void Effect_onEffectStopped(object sender, GameObject e)
     {

     }

     private void Effect_onEffectStarted(object sender, GameObject e)
     {

     }

    public override void SpawnObject()
    {
        base.SpawnObject();
    }*/
}
