using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Limit : MonoBehaviour
{
    [SerializeField] private int Limit;

    void Start()
    {
        Application.targetFrameRate = Limit;
    }
}
