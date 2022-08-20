using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSpawn : MonoBehaviour
{

    public float Force;


    void Start()
    {       
        GetComponent<Rigidbody2D>().angularVelocity = Force;
    }

    private void OnMouseDown()
    {
        FindObjectOfType<PauseManager>().Pause();
    }

}
