using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class ScreenShakeManager : MonoBehaviour
{
    private CinemachineImpulseSource _cinemachineImpulseSource;

    private bool _gameStopped = false;

    private void Start()
    {
        _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        EventManager.OnGameStateChanged += EventManager_OnGameStateChanged;
        EventManager.OnVelocityShake += EventManager_OnVelocityShake;
        EventManager.OnShake += EventManager_OnShake;
    }
    private void EventManager_OnShake(float obj)
    {
        if (_gameStopped) return;
        Shake(obj);
    }
    private void EventManager_OnVelocityShake(Vector3 obj)
    {
        if (_gameStopped) return;
        VelocityShake(obj);
    }

    private void EventManager_OnGameStateChanged()
    {
        _gameStopped = !_gameStopped;
    }

    public void Impulse (Vector3 _posiotion, Vector3 _velocity)
    {
        _cinemachineImpulseSource.GenerateImpulseAt(_posiotion,_velocity);
    }

    public void VelocityShake(Vector3 _velocity)
    {
        _cinemachineImpulseSource.GenerateImpulseWithVelocity(_velocity);
    }


    public void VelocityShake(Vector3 _velocity,bool des)
    {
        _cinemachineImpulseSource.GenerateImpulseWithVelocity(_velocity);
    }

    public void Shake(float _shakeForce)
    {
        _cinemachineImpulseSource.GenerateImpulseWithForce(_shakeForce);
    }


    private void OnDestroy()
    {
        EventManager.OnGameStateChanged -= EventManager_OnGameStateChanged;
        EventManager.OnVelocityShake -= EventManager_OnVelocityShake;
        EventManager.OnShake -= EventManager_OnShake;
    }
}
