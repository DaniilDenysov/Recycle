using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private string ID;
    [SerializeField] private GameObject particles; 
    private void OnMouseDown()
    {
        EffectsManager manager = FindObjectOfType<EffectsManager>();
        // manager.GetComponent<Animator>().GetAn.Stop();
        // GetComponentInChildren<ParticleSystem>().Play();
        
        Destroy(Instantiate(particles,transform.position,Quaternion.identity),1.5f);
        manager.GetComponent<Animator>().Play(ID);
        Destroy(gameObject);
        //GetComponent<AudioSource>().Play(); //сделать анимацию подбирания 
    }
}
