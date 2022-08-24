using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private string ID;
    private void OnMouseDown()
    {
        EffectsManager manager = FindObjectOfType<EffectsManager>();
       // manager.GetComponent<Animator>().GetAn.Stop();
        manager.GetComponent<Animator>().Play(ID);
        Destroy(gameObject);
        //GetComponent<AudioSource>().Play(); //сделать анимацию подбирания 
    }
}
