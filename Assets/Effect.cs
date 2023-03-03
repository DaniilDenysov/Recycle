using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [Range(0, 600)]
    [SerializeField] protected float effectTime = 60f;
    public static event EventHandler <GameObject> onEffectStarted, onEffectStopped;
    [SerializeField] protected AnimationCurve func;
    public virtual void Start()
    {
        EffectStart();
    }

    public virtual void SetFunc(AnimationCurve newFunc) => func = newFunc;

    public virtual Effect GetEffectType() => this;

    public virtual object GetEffect() => null;

    public override string ToString()
    {
        return "Effect";
    }

    public virtual void Update()
    {
        effectTime -= Time.unscaledDeltaTime;
        if (effectTime > 0) return;
        EffectStopped();
    }


    public virtual void EffectStart ()
    {
        onEffectStarted?.Invoke(this, gameObject);
    }

    public virtual void EffectStopped ()
    {
        onEffectStopped?.Invoke(this, gameObject);
        Destroy(this);
    }
}
