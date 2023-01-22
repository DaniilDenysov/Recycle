using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] private ParticleSystem confety;

    public void PlayConfety ()
    {
        confety.Play();
    }
}
