using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public bool dir;
    public Sprite[] Skins;
    public float Speed;

    void Start()
    {    
        //transform.Rotate(Random.Range(5, -5), 0, 0);
        GetComponent<SpriteRenderer>().sprite = Skins[Random.Range(0, Skins.Length - 1)];
        if (transform.position.x == -15)
            dir = false;
        if (transform.position.x == 15)
            dir = true;

        Destroy(gameObject,60);
    } 
    void FixedUpdate()
    {
        if (dir == true)
            transform.Translate(-Speed * Time.deltaTime, 0, 0);
        else
            transform.Translate(Speed * Time.deltaTime, 0, 0);
    }
}
