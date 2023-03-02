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

    public  AudioMixer GetMixer()
    {
        return mixer;
    }
  
}
