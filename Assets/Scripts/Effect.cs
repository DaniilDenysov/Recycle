using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private string ID;
    [SerializeField] private GameObject particles; 
    private void OnMouseDown()
    {
        // manager.GetComponent<Animator>().GetAn.Stop();
        // GetComponentInChildren<ParticleSystem>().Play();
        
        Destroy(Instantiate(particles,transform.position,Quaternion.identity),1.5f);
        FindObjectOfType<EffectsManager>().GetComponent<Animator>().Play(ID);
      //  GetComponent<Animator>().Play("Destroy");
        Destroy(gameObject,0.05f);
       
    }
}
