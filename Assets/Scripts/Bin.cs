using UnityEngine;


public class Bin : MonoBehaviour
{
    public int Acceptable;
    [SerializeField] private Color Green, Red;
    [SerializeField] private Animator tick_anim;
    [SerializeField] private Sprite[] BinSprite;
    [SerializeField] private ParticleSystem [] particle;
    [SerializeField] private ParticleSystem Tick_Particle;
    [SerializeField] private AudioClip audioClip;
    bool Help;

    void Start()
    {
        gameObject.layer = Acceptable;
        if (PlayerPrefs.HasKey("Help"))
        {
           // Debug.Log("Help: " + PlayerPrefs.GetInt("Help"));
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
            if (Garbage_ID == Acceptable) tick_anim.Play("Accepted");
            else tick_anim.Play("NotAccepted");
            //TickParticles(Green);

            // TickParticles(Red);
        }
    }
    public void ChangeColor ()
    {
        GetComponent<SpriteRenderer>().sprite = BinSprite[Acceptable];        
    }
    public void PlaySound ()
    {
        SoundManager.instance.PlaySound(audioClip);
    }
}
