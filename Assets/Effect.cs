using System;
using UnityEngine.UI;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [Range(0, 600)]
    [SerializeField] protected float _effectTime = 60f;
    protected float _fixedEffectTime;

    [SerializeField] protected Image _progress;
    [SerializeField] protected AnimationCurve _effect_func, _time_func;
    private float timeSpend = 0f;
    public virtual void Start()
    {
        EffectStart();
        _fixedEffectTime = _effectTime;
    }

    public virtual void SetEffectTime(float time) => _effectTime = time;

    public virtual void SetTimeFunc(AnimationCurve newFunc) => _time_func = newFunc;
    public virtual void SetEffectFunc(AnimationCurve newFunc) => _effect_func = newFunc;

    public virtual Effect GetEffectType() => this;

    public virtual float GetFixedEffectTime() => _fixedEffectTime;

    public virtual object GetEffect() => null;

    public override string ToString()
    {
        return "Effect";
    }

    public virtual void SetProgress(Image _progress) { this._progress = _progress; Debug.Log("Setted"); }

    public virtual void Update()
    {
        _effectTime -= Time.unscaledDeltaTime;
        Debug.Log("Prog:" + _effectTime * (1 / 100));
        _progress.fillAmount = _effectTime * (1 / _fixedEffectTime);
        if (_effectTime > 0) return;
        EffectStopped();
    }


    public virtual void EffectStart ()
    {
        EventManager.FireEvent(EventManager.OnEffectStarted, this);
    }

    public virtual void EffectStopped ()
    {
        EventManager.FireEvent(EventManager.OnEffectStopped, this);
        Destroy(_progress.gameObject);
        Destroy(this);
    }
}
