using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class InitializeEffect : MonoBehaviour
{
    [SerializeField] private string effect = "TicTac";
    [SerializeField] private AnimationCurve time_func,effect_func;
    [SerializeField] private AudioClip soundEffect;

    private void OnMouseDown()
    {
        var vari = effect;
        EffectsManager.instance.gameObject.AddComponent(System.Type.GetType(effect));
        EffectsManager.instance.gameObject.GetComponent<Effect>().SetTimeFunc(time_func);
        EffectsManager.instance.gameObject.GetComponent<Effect>().SetEffectFunc(effect_func);
        SoundManager.instance.PlaySound(soundEffect);
        DestroyObject();
    }

    private void DestroyObject ()
    {
        Destroy(this.gameObject);
    }
}

