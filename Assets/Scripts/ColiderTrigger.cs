using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderTrigger : MonoBehaviour
{
    [SerializeField] private Garbage garbage;
    
    private void OnCollisionEnter2D (Collision2D collision)
    {
        garbage.detectColider(collision);    
    }
}
