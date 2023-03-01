using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case : MonoBehaviour
{
    [SerializeField] private ParticleSystem confety;
    [SerializeField] private Animator trophey_animator,case_animator;

    public void PlayConfety ()
    {
        confety.Play();
    }

    public void openCase ()
    {
        case_animator.SetTrigger("Open");
    }

    public void takeTrophey ()
    {
        trophey_animator.SetTrigger("Take");
        case_animator.SetTrigger("Take");
    }

    public void PlayTrophey (string name)
    {
        trophey_animator.Play(name);
    }
}
