using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class InitializeEffect : MonoBehaviour,IWarning
{
    [SerializeField] private string effect = "TicTac";
    [SerializeField] private AnimationCurve time_func,effect_func;
    [SerializeField] private AudioClip soundEffect;
    [SerializeField] private GameObject warningPref;
    private GameObject warning;

    [SerializeField] private object Sc;

    private void Start()
    {
        warning = Instantiate(warningPref, transform.position, Quaternion.identity);
        Destroy(gameObject,10);
    }

    private void Update()
    {
        Follow();
    }

    private void OnMouseDown()
    {
        var vari = effect;
        ParticleSystem vfx = GetComponentInChildren<ParticleSystem>();
        vfx.Play();
        vfx.transform.SetParent(null);
        Type type = System.Type.GetType(effect);
        if (EffectsManager.instance.CheckEffects(type))
        {
            EffectsManager.instance.GetEffectOfType(type).SetEffectTime(60); 
            DestroyObject();
        }
        else
        {
            object temp = EffectsManager.instance.gameObject.AddComponent(type);
            Effect newEffect = (Effect)temp;
            newEffect.SetTimeFunc(time_func);
            newEffect.SetEffectFunc(effect_func);
            SoundManager.instance.PlaySound(soundEffect);
            DestroyObject();
        }
    }

    private void DestroyObject ()
    {
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        Destroy(warning);
    }
    public void Follow()
    {
        warning.transform.position = transform.position;
    }
}

