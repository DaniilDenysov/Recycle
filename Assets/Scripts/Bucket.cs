using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bucket : MonoBehaviour
{
    public GameObject Dialog;
    [SerializeField] private Text[] Colors;
    public Sprite[] Ticks;
   public GameObject Info;
    [SerializeField] private int MyNumber;
    [SerializeField] private Vector3 [] DifficultyPosition;
    [SerializeField] private ParticleSystem TrashParticles, Tick;
   public bool [] isActiveNow;
   public int [] Bottles;
    AudioSource sou;    
    int Reputation;
    public GameObject USC;
    public AudioClip Sound;
    public Text PraiceText;
    public string[] PriceStringText;
    public GameObject [] HappinessController;
    public PanelController PC;
    int TagNum, LNum;
    [SerializeField] private LayerMask AcceptableForRecycle;
    GameObject CheckTick;
    void Start()
    {
        this.gameObject.SetActive(isActiveNow[PlayerPrefs.GetInt("Difficulty")]);
        this.gameObject.transform.position = new Vector3 (DifficultyPosition[PlayerPrefs.GetInt("Difficulty")].x, DifficultyPosition[PlayerPrefs.GetInt("Difficulty")].y, DifficultyPosition[PlayerPrefs.GetInt("Difficulty")].z);
        Dialog.GetComponent<Animator>().Play("StaticDialog");
        sou = GetComponent<AudioSource>();
    
    }
    void FixedUpdate()
    {

    }   
    private void OnMouseDown()
    {
       Dialog.GetComponent<Animator>().Play("StaticDialog");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (USC.GetComponent<UniversalSceneController>().isPaused == false)
        {
            Debug.Log(collision.gameObject.layer);
            if (collision.gameObject.layer == gameObject.layer && collision.gameObject.tag != "Graund" && USC.GetComponent<UniversalSceneController>().isPaused == false)
            {
                TrashParticles.Play();
                Bottles[collision.gameObject.layer] += 1;
                USC.GetComponent<UniversalSceneController>().NowRab += 1;
                USC.GetComponent<UniversalSceneController>().PointBar(1);
                // sou.PlayOneShot(Sound);
                Destroy(collision.gameObject);
              
            }
            else if (collision.gameObject.layer != gameObject.layer && collision.gameObject.tag != "Graund" && USC.GetComponent<UniversalSceneController>().isPaused == false)
            {
                TrashParticles.Play();
                Bottles[collision.gameObject.layer] -= 3;
                USC.GetComponent<UniversalSceneController>().NowRab -= 3;
                USC.GetComponent<UniversalSceneController>().PointBar(-3);
                //sou.PlayOneShot(Sound);
                Destroy(collision.gameObject);
            }
            USC.GetComponent<UniversalSceneController>().Check();
        }
        else if (USC.GetComponent<UniversalSceneController>().isPaused == true && collision.gameObject.tag != "Graund")
        {
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (USC.GetComponent<UniversalSceneController>().isPaused == false)
        {
            Debug.Log(collision.gameObject.layer);
            if (collision.gameObject.layer == gameObject.layer && collision.gameObject.tag != "Graund" && USC.GetComponent<UniversalSceneController>().isPaused == false)
            {
                TrashParticles.Play();
                Bottles[collision.gameObject.layer] += 1;
                USC.GetComponent<UniversalSceneController>().NowRab += 1;
                USC.GetComponent<UniversalSceneController>().PointBar(1);
                // sou.PlayOneShot(Sound);
                Destroy(collision.gameObject);

            }
            else if (collision.gameObject.layer != gameObject.layer && collision.gameObject.tag != "Graund" && USC.GetComponent<UniversalSceneController>().isPaused == false)
            {
                TrashParticles.Play();
                Bottles[collision.gameObject.layer] -= 3;
                USC.GetComponent<UniversalSceneController>().NowRab -= 3;
                USC.GetComponent<UniversalSceneController>().PointBar(-3);
                //sou.PlayOneShot(Sound);
                Destroy(collision.gameObject);
            }
            USC.GetComponent<UniversalSceneController>().Check();
          //  Info.GetComponent<SpriteRenderer>().sprite = null;
        }
        else if (USC.GetComponent<UniversalSceneController>().isPaused == true && collision.gameObject.tag != "Graund")
        {
            Destroy(collision.gameObject);
           // Info.GetComponent<SpriteRenderer>().sprite = null;
        }
     
    }
    
    public void OnTick (int lay)
    {
        if (lay == this.gameObject.layer) Info.GetComponent<SpriteRenderer>().sprite = Ticks[0];
        else Info.GetComponent<SpriteRenderer>().sprite = Ticks[1];
    }

    void Praice()
    {
        PraiceText.text = PriceStringText[Reputation];
        while (PraiceText.fontSize < 60)
        {
            PraiceText.fontSize += 1;
        }

        IEnumerator TextSize()
        {
            yield return new WaitForSeconds(0.1f);
            while (PraiceText.fontSize > 30)
            {
                PraiceText.fontSize -= 1;
            }
        }
        PraiceText.GetComponent<Animator>().Play("Praice");
    }
}
