using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [Range(0, 600)]
    [SerializeField] protected float effectTime = 60f;
    public static event EventHandler onEffectStarted, onEffectStopped;

    public virtual void Start()
    {
        EffectStart();
        onEffectStarted?.Invoke(this, EventArgs.Empty);
    }

    public virtual void Update()
    {
        effectTime -= Time.deltaTime;
        if (effectTime > 0) return;
        onEffectStopped?.Invoke(this, EventArgs.Empty);
        EffectStopped();
    }

    public virtual void EffectStart ()
    {
 
    }

    public virtual void EffectStopped ()
    {
        Destroy(this);
    }
}
