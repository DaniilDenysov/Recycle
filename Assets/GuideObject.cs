using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuideObject : MonoBehaviour
{
    public static event EventHandler<Callback> OnCallback;
    [SerializeField] private Callback _callback;

    public enum Callback
    {
        OnCollision,
        OnClick
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (_callback != Callback.OnCollision) return;
        Debug.Log("Callback");
        OnCallback?.Invoke(this, Callback.OnCollision);
    }

    public virtual void OnMouseDown()
    {
        if (_callback != Callback.OnClick) return;
        OnCallback?.Invoke(this,Callback.OnClick);
    }

}
