using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashController : MonoBehaviour
{

    public bool block, help;
    public UniversalSceneController USC;
    [SerializeField] private Sprite[] TrashSprite;
    [SerializeField] private int [] TrashLayer; 
    public string[] Name, BucketName;
     public GameObject []BuckName;
    public PanelController Panel;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float[] TrashRadius;
    AudioSource sou;
    public PanelController TouchScreen;
    public int LayerNumber;
    public AudioClip clip;
    public bool PlayedClip, isTaken;
    Rigidbody2D rb;
    GameObject CheckedBucket;
    void Start()
    {
        if (PlayerPrefs.GetInt("Help") == 0) help = true;
        else if (PlayerPrefs.GetInt("Help") == 1) help = false;
        
        TouchScreen = USC.PanelCont.GetComponent<PanelController>();
        if (Panel.Trash == null) Debug.Log("Null");
        RandomTrash();
        sou = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        if (USC.isPaused == true) Destroy(this.gameObject);
    }
    private void FixedUpdate()
    {
        /**  if (Input.GetAxisRaw("Fire1") == 0)
          {
              isTaken = false;
              GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
              GetComponent<Rigidbody2D>().WakeUp();
          }*/
        if (help == true)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector3(rayPoint.transform.position.x, rayPoint.transform.position.y, -1), -transform.up, this.gameObject.layer);
            if (hit.collider && TouchScreen.Trash == this.gameObject)
            {

                if (hit.collider.GetComponent<Bucket>())
                {

                    if (CheckedBucket != hit.collider.gameObject)
                    {
                        CheckedBucket = hit.collider.gameObject;
                        hit.collider.GetComponent<Bucket>().OnTick(this.gameObject.layer);
                    } else
                    {
                        hit.collider.GetComponent<Bucket>().OnTick(this.gameObject.layer);
                    }
                    Debug.Log(gameObject.name + "Raycast hit:" + hit.collider.name);
                }
                else
                {
                    if (CheckedBucket) CheckedBucket.GetComponent<Bucket>().Info.GetComponent<SpriteRenderer>().sprite = null;
                    Debug.Log(gameObject.name + "Raycast hit:" + hit.collider.name);
                }

            }
        }
    }
    public void RandomTrash ()
    {
        LayerNumber = Random.Range(0, TrashSprite.Length - (PlayerPrefs.GetInt("Difficulty") * 10));   
        while (Name[LayerNumber] == PlayerPrefs.GetString("LastTrashNumber"))
        {
            LayerNumber = Random.Range(0, TrashSprite.Length - PlayerPrefs.GetInt("Difficulty") * 10);
        }
        GetComponent<SpriteRenderer>().sprite = TrashSprite[LayerNumber];
        gameObject.layer = TrashLayer[LayerNumber];
        this.gameObject.name = Name[LayerNumber];
        if (TrashRadius[LayerNumber] != null)  GetComponent<CircleCollider2D>().radius = TrashRadius[LayerNumber];
        PlayerPrefs.SetString("LastTrashNumber", Name[LayerNumber]);
        PlayerPrefs.Save();
        Debug.Log(TrashLayer[LayerNumber]);
    }
   

    public void OnMouseDown()
    {
        if (Panel.Trash == null || Panel.Trash.GetComponent<TrashController>().isTaken == false)
        {

            Debug.Log("Off");
            Panel.Trash = this.gameObject;
            isTaken = true;
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    public void OnMouseOver()
    {
        if (Panel.Trash == this.gameObject)
        {
            
            isTaken = true;
            GetComponent<CircleCollider2D>().isTrigger = true;
            
        }
    }


    public void OnMouseExit()
    {    
            Debug.Log("On");
            GetComponent<CircleCollider2D>().isTrigger = false;
            Panel.Trash = null;
            isTaken = false;
        if (CheckedBucket) CheckedBucket.GetComponent<Bucket>().Info.GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().WakeUp();
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 15)
        {
            GameObject.Find("Sun").GetComponent<UniversalSceneController>().Defeat();
            PlayedClip = true;
            block = true;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10 || collision.gameObject.layer == 11 || collision.gameObject.layer == 13 && USC.isPaused == false)
        {
            if (isTaken == false)
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            else
            {
                Panel.Trash = this.gameObject;
                isTaken = true;
                GetComponent<CircleCollider2D>().isTrigger = true;
            }
            Destroy(this.gameObject);
        }
        else
        {
            if (collision.gameObject.layer == 15 ) Destroy(this.gameObject);
        }
        /* if (collision.gameObject.layer == 9|| collision.gameObject.layer == 10 || collision.gameObject.layer == 11 || collision.gameObject.layer == 13 && isTaken == true)
         {
             Panel.Trash = this.gameObject;
         }*/

        if (collision.gameObject.layer == 8 )
        {
            PlayedClip = true;
            block = true;          
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 15)
        {
            GameObject.Find("Sun").GetComponent<UniversalSceneController>().Defeat();
            PlayedClip = true;
            block = true;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Graund" && gameObject.layer == 10)
        {      
            block = true;            
            //Buck[1].GetComponent<Bucket>().Bottles[0] -= 10;
            gameObject.tag = "Graund";         
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            block = true;
        }
       

    }
   
}
