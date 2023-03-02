using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;


public class TicTac : Effect
{
    [Range(1,100)]
    [SerializeField] private float timeBeforeSlowingDown = 10f;
    [SerializeField] private AudioMixer mixer;
    private bool timeStop = true;
    private PostProcessVolume volume;
    private ChromaticAberration aberration;
    private float fixedEffectTime;

    public override void Start()
    {
        base.Start();
        fixedEffectTime = effectTime;
        mixer = EffectsManager.instance.GetMixer();
    }

    public override Effect GetEffectType() => this;

    public override string ToString()
    {
        return "TicTac";
    }

    public override void Update()
    {
        Time.timeScale += (1f / fixedEffectTime) * Time.unscaledDeltaTime;
        mixer.SetFloat("Pitch", Time.timeScale);    
        aberration.intensity.value -= (1f / fixedEffectTime) * Time.unscaledDeltaTime;
        base.Update();
    }   

    public override void EffectStopped()
    {
        Debug.Log("Effect tic-tac stopped!");
        Time.timeScale = 1f;
        base.EffectStopped();
    }

    public override void EffectStart()
    {
        Time.timeScale = 0.01f;
        volume = FindObjectOfType<PostProcessVolume>();
        volume.profile.TryGetSettings(out aberration);
        aberration.intensity.value = 1f;
        base.EffectStart();
    }
}
