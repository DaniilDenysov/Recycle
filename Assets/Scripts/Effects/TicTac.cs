using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;


public class TicTac : Effect
{
    [SerializeField] private AudioMixer mixer;
    private PostProcessVolume volume;
    private ChromaticAberration aberration;
    private float fixedEffectTime;
    public static event EventHandler<int> OnStartTimeChaneges, OnStopTimeChanges;

    public override void Start()
    {
        base.Start();
        fixedEffectTime = _effectTime;
        mixer = EffectsManager.instance.GetMixer();
        OnStartTimeChaneges?.Invoke(this,15); //in % of time range
    }

    public override Effect GetEffectType() => this;

    public override string ToString() => "TicTac";

    public override void Update()
    {

        Time.timeScale = _time_func.Evaluate(_effectTime / 100);
     //   aberration.intensity.value = effect_func.Evaluate(Time.timeScale);
        mixer.SetFloat("Pitch", Time.timeScale);
        base.Update();
    }   

    public override void EffectStopped()
    {
        Debug.Log("Effect tic-tac stopped!");
        Time.timeScale = 1f;
        OnStopTimeChanges?.Invoke(this, 1); //2 - slows down spawn rate by two 
        base.EffectStopped();
    }

    public override void EffectStart()
    {
       // volume = FindObjectOfType<PostProcessVolume>();
      //  volume.profile.TryGetSettings(out aberration);
        base.EffectStart();
    }
}
