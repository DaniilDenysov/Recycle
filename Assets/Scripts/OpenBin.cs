using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Animator>().Play("OpenAnimation"); 
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GetComponent<Animator>().Play("CloseAnimation");
    }
}
