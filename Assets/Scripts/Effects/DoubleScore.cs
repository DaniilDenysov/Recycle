using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;

public class DoubleScore : Effect
{
    [Range(1, 100)]
    [SerializeField] private int Amount = 2;

    private PostProcessVolume volume;
    private ChromaticAberration lensDistortion;
  //  private float fixedEffectTime;
    public override object GetEffect() => Amount;

    public override Effect GetEffectType() => this;

    public override void Start()
    {
        base.Start();
    //    fixedEffectTime = effectTime;
    }

    public override string ToString()
    {
        return "DoubleScore";
    }

    public override void Update()
    {
        base.Update();
       // lensDistortion.intensity.value = effect_func.Evaluate(effectTime / 100);
    }

    public override void EffectStopped()
    {
      //  lensDistortion.intensity.value = effect_func.Evaluate(effectTime / 100);
        base.EffectStopped();
    }

    public override void EffectStart()
    {
        Debug.Log("Double");
      //  volume = FindObjectOfType<PostProcessVolume>();
    //    volume.profile.TryGetSettings(out lensDistortion);
        base.EffectStart();
    }
}
