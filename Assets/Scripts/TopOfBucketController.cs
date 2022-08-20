using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOfBucketController : MonoBehaviour
{

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        if (hit.collider)
        {
            if (hit.collider.gameObject.GetComponent<Bucket>())
            {
                hit.collider.gameObject.GetComponent<Bucket>().OnTick(this.gameObject.layer);
            }          
            Debug.Log(gameObject.name + "Raycast hit:" + hit.collider.name);
        }
    }
}
