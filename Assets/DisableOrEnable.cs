using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOrEnable : MonoBehaviour
{
    private void OnDisable()
    {
        GetComponent<Animator>().Play("OnDisable"); 
    }
    private void OnEnable()
    {
        GetComponent<Animator>().Play("OnEnable");
    }
}
