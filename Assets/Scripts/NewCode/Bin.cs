using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    public int Acceptable;
    [SerializeField] private Color Green, Red;
    [SerializeField] private Animator tick_anim;
    [SerializeField] private Sprite[] BinSprite;
    [SerializeField] private ParticleSystem [] particle;
    [SerializeField] private ParticleSystem Tick_Particle;
    bool Help;

    void Start()
    {
        gameObject.layer = Acceptable;
        if (PlayerPrefs.HasKey("Help"))
        {
            if (PlayerPrefs.GetInt("Help") == 0) Help = true;
            else Help = false;
        }
    }
    public void TickParticles(Color color)
    {
       // if (!Tick_Particle.isPlaying)
       // {
            Tick_Particle.startColor = color;
            Tick_Particle.Play();
       // }
    }
    public void Particles ()
    {
        particle[Acceptable].Play();
    }
    public void Check (int Garbage_ID)
    {
        if (Help)
        {
            if (Garbage_ID == Acceptable)
            {
                //TickParticles(Green);
                tick_anim.Play("Accepted");
            }
            else
            {
               // TickParticles(Red);
                tick_anim.Play("NotAccepted");
            }
        }
    }
    public void ChangeColor ()
    {
        GetComponent<SpriteRenderer>().sprite = BinSprite[Acceptable];        
    }
    public void PlaySound (AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
