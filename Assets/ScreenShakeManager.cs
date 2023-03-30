using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class ScreenShakeManager : MonoBehaviour
{
    public static ScreenShakeManager instance;

    private CinemachineImpulseSource _cinemachineImpulseSource;

    private void Awake()
    {
        instance = this;        
    }

    private void Start()
    {
        _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void VelocityShake(Vector3 _velocity)
    {
        _cinemachineImpulseSource.GenerateImpulseWithVelocity(_velocity);
    }

    public void Shake(float _shakeForce)
    {
        _cinemachineImpulseSource.GenerateImpulseWithForce(_shakeForce);
    }
}
