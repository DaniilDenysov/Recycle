using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartMover : MonoBehaviour
{
    public GameObject Spawn;
    public Sprite [] Types;
    void Start()
    {
     
        Spawn = GameObject.Find("Spawn");
        GetComponent<SpriteRenderer>().sprite = Types[Spawn.GetComponent<Spawn>().Map];
        Destroy(gameObject,15f);
    }

  
    void FixedUpdate()
    {
        transform.Rotate(Random.Range(0, 145) * Time.deltaTime, Random.Range(0, 145) * Time.deltaTime, Random.Range(0, 145) * Time.deltaTime);
        transform.Translate(0,-0.01f,0);
    }
}
