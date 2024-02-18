using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsHud : MonoBehaviour
{

    [SerializeField] private string _rescourceLink;

    void Start()
    {
        EventManager.OnEffectStarted += EventManager_OnEffectStarted;
        EventManager.OnEffectStopped += EventManager_OnEffectStopped;
    }

    private void EventManager_OnEffectStarted(Effect obj)
    {
        GameObject _temp = Instantiate(Resources.Load<GameObject>(_rescourceLink + obj.ToString()), transform);
        obj.SetProgress(_temp.GetComponent<Image>());
    }

    private void EventManager_OnEffectStopped(Effect obj)
    {

    }

    private void OnDestroy()
    {
        EventManager.OnEffectStarted -= EventManager_OnEffectStarted;
        EventManager.OnEffectStopped += EventManager_OnEffectStopped;
    }
}
 