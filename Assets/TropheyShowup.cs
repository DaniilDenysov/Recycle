using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TropheyShowup : MonoBehaviour
{
    [SerializeField] private ParticleSystem trophey;

    public void tropheyShowUP()
    {
        trophey.Play();
    }
}
