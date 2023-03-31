using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class ScreenShakeManager : MonoBehaviour
{
    public static ScreenShakeManager instance;

    private CinemachineImpulseSource _cinemachineImpulseSource;

    private bool _gameStopped = false;

    private void Awake()
    {
        instance = this;
    }

    private void GameBrakeManager_OnBrake(object sender, bool e)
    {
        _gameStopped = e;
    }

    private void Start()
    {
        _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        GameBrakeManager.OnBrake += GameBrakeManager_OnBrake;
    }

    public void VelocityShake(Vector3 _velocity)
    {
        if (_gameStopped) return;
        _cinemachineImpulseSource.GenerateImpulseWithVelocity(_velocity);
    }

    public void Shake(float _shakeForce)
    {
        if (_gameStopped) return;
        _cinemachineImpulseSource.GenerateImpulseWithForce(_shakeForce);
    }
}
