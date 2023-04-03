using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EffectsManager : MonoBehaviour
{
    
    public static EffectsManager instance { get; set; }

    [SerializeField] private AudioMixer mixer;

    private void Awake()
    {
        instance = this;
    }

    public Effect GetEffectOfType (Type effect)
    {
        return (Effect)GetComponent(effect);
    }

    public bool CheckEffects (Type effect)
    {
        if (GetComponent(effect) != null) return true;
        return false;
    }

    public  AudioMixer GetMixer()
    {
        return mixer;
    }
  
}
