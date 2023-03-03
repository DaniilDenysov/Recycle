using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class InitializeEffect : MonoBehaviour
{
    [SerializeField] private string effect = "TicTac";
    [SerializeField] private AnimationCurve func;

    private void OnMouseDown()
    {
        var vari = effect;
        EffectsManager.instance.gameObject.AddComponent(System.Type.GetType(effect));
        EffectsManager.instance.gameObject.GetComponent<Effect>().SetFunc(func);
        DestroyObject();
    }

    private void DestroyObject ()
    {
        Destroy(this.gameObject);
    }
}

