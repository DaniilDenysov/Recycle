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
        Effect.onEffectStarted += Effect_onEffectStarted;
        //Effect.onEffectStopped += Effect_onEffectStopped;
    }

    private void Effect_onEffectStopped(object sender, GameObject e)
    {

    }

    private void Effect_onEffectStarted(object sender, Effect e)
    {
            Debug.Log("Effect: " + _rescourceLink + e.ToString());
            GameObject _temp = Instantiate(Resources.Load<GameObject>(_rescourceLink + e.ToString()), transform);
            e.SetProgress(_temp.GetComponent<Image>());
    }
}
