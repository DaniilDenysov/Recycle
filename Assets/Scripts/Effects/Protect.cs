using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Protect : Effect
{
    private PostProcessVolume volume;
    private ChromaticAberration vignette;

    public override object GetEffect() => true;
    public override Effect GetEffectType() => this;

    public override void Start()
    {
        base.Start();
    }

    public override string ToString()
    {
        return "Protect";
    }

    public override void Update()
    {
        base.Update();
     //   vignette.intensity.value = effect_func.Evaluate(effectTime/100);
        //vignette.intensity.value -= (0.2f / fixedEffectTime) * Time.unscaledDeltaTime;
    }

    public override void EffectStopped()
    {
        base.EffectStopped();
    }

    public override void EffectStart()
    {
        Debug.Log("Protect");
        //volume = FindObjectOfType<PostProcessVolume>();
       // volume.profile.TryGetSettings(out vignette);
        base.EffectStart();
    }
}
