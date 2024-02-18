using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinVFX : MonoBehaviour
{

    [SerializeField] private ParticleSystem _particleSystem;

    public void Particles()
    {
        _particleSystem.Play();                                                                                                     
    }
}
