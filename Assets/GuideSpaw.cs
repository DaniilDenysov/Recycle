using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSpaw : GuideEvent
{

    [SerializeField] private GameObject _testObject;
    private GameObject _temp;
    private bool _initiated;

    public override void StartEvent()
    {
        base.StartEvent();
        GuideObject.OnCallback += GuideObject_OnCallback;
        _initiated = true;
    }

    private void GuideObject_OnCallback(object sender, GuideObject.Callback e)
    {
        EndEvent();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_temp != null) return;
        if (!_initiated) return;
        _temp = Instantiate(_testObject, transform.position, Quaternion.identity);
    }
}
