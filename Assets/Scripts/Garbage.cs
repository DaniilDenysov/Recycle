using UnityEngine;

public class Garbage : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private GameObject texture;
    [SerializeField] private GameObject particles;
    [SerializeField] private AudioClip ObjectTaken, ObjectDropped;
    [SerializeField] private LayerMask collisionIgnore;
    private Animator animator;
    public PauseManager pauseManager;
    public DefeatManager defeatManager;
    public ScoreManager scoreManager;
    private AudioSource audioSource;
    private Rigidbody2D rigidbody;
    private HingeJoint2D hingeJoint2D;
    private BoxCollider2D boxCollider2D;
    private CircleCollider2D circleCollider2D;
    bool isTaken;

    private void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        defeatManager = FindObjectOfType<DefeatManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
        hingeJoint2D = GetComponent<HingeJoint2D>();
    /*   if (GetComponent<BoxCollider2D>()) boxCollider2D = GetComponent<BoxCollider2D>();
       else circleCollider2D = GetComponent<CircleCollider2D>();*/
        if (texture != null) animator = texture.GetComponent<Animator>();
        else animator = GetComponent<Animator>();
       
    }


    /*private void OnTriggerEnter2D(Collider2D collision)
      {
          if (!pauseManager.Paused && !defeatManager.Lost)
          {
              if (gameObject.layer == collision.gameObject.layer && collision.gameObject.GetComponent<Bin>())
              {
                  scoreManager.AddScore(1);
                  collision.gameObject.GetComponent<AudioSource>().Play();
                  collision.gameObject.GetComponent<Bin>().Particles();
                  Destroy();
              }
              else if (collision.gameObject.layer == 15)
              {
                  SpawnParticles();
                  scoreManager.AddScore(-6);
                  Destroy();
              }
              else if (collision.gameObject.layer != gameObject.layer && collision.gameObject.GetComponent<Bin>())
              {
                  scoreManager.AddScore(-3);
                  collision.gameObject.GetComponent<AudioSource>().Play();
                  collision.gameObject.GetComponent<Bin>().Particles();
                  Destroy();
              }
          }
          else
          {
              Destroy(gameObject);
          }

      }*/
    public void detectColider(Collision2D collision)
    {
        if (collision != null)
        {
            if (!pauseManager.Paused && !defeatManager.Lost)
            {
                if (gameObject.layer == collision.gameObject.layer && collision.gameObject.GetComponent<Bin>())
                {
                    scoreManager.AddScore(1);
                    collision.gameObject.GetComponent<AudioSource>().Play();
                    collision.gameObject.GetComponent<Bin>().Particles();
                    Destroy();
                }
                else if (collision.gameObject.layer == 15)
                {
                    SpawnParticles();
                    scoreManager.AddScore(-6);
                    Destroy();
                }
                else if (collision.gameObject.layer != gameObject.layer && collision.gameObject.GetComponent<Bin>())
                {
                    scoreManager.AddScore(-3);
                    collision.gameObject.GetComponent<AudioSource>().Play();
                    collision.gameObject.GetComponent<Bin>().Particles();
                    Destroy();
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!pauseManager.Paused && !defeatManager.Lost)
        {
            if (gameObject.layer == collision.gameObject.layer && collision.gameObject.GetComponent<Bin>())
            {
                scoreManager.AddScore(1);
                collision.gameObject.GetComponent<AudioSource>().Play();
                collision.gameObject.GetComponent<Bin>().Particles();
                Destroy();
            }
            else if (collision.gameObject.layer == 15)
            {
                SpawnParticles();
                scoreManager.AddScore(-6);
                Destroy();
            }
            else if (collision.gameObject.layer != gameObject.layer && collision.gameObject.GetComponent<Bin>())
            {
                scoreManager.AddScore(-3);
                collision.gameObject.GetComponent<AudioSource>().Play();
                collision.gameObject.GetComponent<Bin>().Particles();
                Destroy();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SpawnParticles ()
    {
        GameObject _particles = Instantiate(particles,transform.position,Quaternion.identity);
        MapManager mapManager = FindObjectOfType<MapManager>();
        _particles.GetComponent<ParticleSystem>().startColor = mapManager.ground[mapManager.Map];
        Destroy(_particles,1);
    }

    public void changeState()
    {
        if (texture != null) transform.parent.gameObject.SetActive(false);
        else gameObject.SetActive(false);
    }

    

    public void Destroy ()
    {
       /*& if (texture != null) transform.parent.gameObject.SetActive(false);
        else gameObject.SetActive(false);
        // Destroy(GetComponent<CircleCollider2D>());
        if (hingeJoint2D) hingeJoint2D.enabled = false;
        rigidbody.sleepMode = RigidbodySleepMode2D.StartAsleep;
        if (texture != null) texture.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.StartAsleep;
        if (boxCollider2D) boxCollider2D.enabled = false;
        else circleCollider2D.enabled = false;
        animator.Play("Destroy");
        Invoke(nameof(changeState), 1);*/
         if (texture != null)
         {

             Destroy(GetComponent<HingeJoint2D>());
             Destroy(GetComponent<Rigidbody2D>());
             Destroy(texture.GetComponent<Rigidbody2D>());
             texture.GetComponent<Animator>().Play("Destroy");
             Destroy(gameObject.transform.parent.gameObject, 1);
             Destroy(this);
         }
         else
         {
             Destroy(GetComponent<Rigidbody2D>());
             GetComponent<Animator>().Play("Destroy");
             Destroy(gameObject, 1);
             Destroy(this);
         }     
    }
    void LateUpdate()
    {       
        /**/
    }
    public void OnMouseDown()
    {
   
            isTaken = true;
            audioSource.PlayOneShot(ObjectTaken);
            animator.Play("Taken");
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            rigidbody.Sleep();
        
    }

    public void OnMouseOver()
    {
        if (isTaken)
        {
            if (!pauseManager.Paused && !defeatManager.Lost)
            {
                Debug.Log("Ray");
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3));
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(point.transform.position.x, point.transform.position.y, -1), Vector2.down);
                //  Debug.DrawLine(transform.position, Vector2.down, Color.red);
                if (hit.collider)
                {
                    Debug.Log("Hitted:" + hit.collider.name);
                    if (hit.collider.GetComponent<Bin>())
                    {
                        Debug.Log("Ok");
                        hit.collider.GetComponent<Bin>().Check(gameObject.layer);
                    }
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        // GetComponent<Animator>().Play("Enter");
    }

    public void OnMouseExit()
    {
        isTaken = false;
        animator.Play("Dropped");
        rigidbody.constraints = RigidbodyConstraints2D.None;
        rigidbody.WakeUp();
    }

    public void OnMouseUp()
    {    
        isTaken = false;
        animator.Play("Dropped");
        rigidbody.constraints = RigidbodyConstraints2D.None;
        rigidbody.WakeUp();      
    }
}
