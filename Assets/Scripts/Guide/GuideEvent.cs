using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuideEvent : MonoBehaviour
{
    public static event EventHandler OnEventStarted, OnEventEnded;

    public virtual void StartEvent ()
    {
        OnEventStarted?.Invoke(this,EventArgs.Empty);
    }

    public virtual void EndEvent()
    {
        OnEventEnded?.Invoke(this, EventArgs.Empty);
    }
}
