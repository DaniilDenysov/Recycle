using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class InitializeEffect : MonoBehaviour
{
    [SerializeField] private Effect effect;

    private void Awake()
    {
        effect = GetEffectType();
        if (effect == null) DestroyObject();
    }

    public Effect GetEffectType() {
       if (TryGetComponent<Effect>(out Effect getEffect)) return getEffect;
       return null;
    }
    private void OnMouseDown()
    {
        var vari = effect;
        EffectsManager.instance.gameObject.AddComponent(System.Type.GetType(vari.ToString()));
        DestroyObject();
    }

    private void DestroyObject ()
    {
        Destroy(this.gameObject);
    }
}

