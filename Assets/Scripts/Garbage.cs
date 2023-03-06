using UnityEngine;

public class Garbage : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private GameObject texture;
    [SerializeField] private GameObject particles;
    [SerializeField] private AudioClip ObjectTaken, ObjectDropped;
    [SerializeField] private LayerMask collisionIgnore;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private Camera camera;
    private HingeJoint2D hingeJoint2D;
    private BoxCollider2D boxCollider2D;
    private CircleCollider2D circleCollider2D;
    private bool isTaken;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        hingeJoint2D = GetComponent<HingeJoint2D>();
        camera = Camera.main;
    /*   if (GetComponent<BoxCollider2D>()) boxCollider2D = GetComponent<BoxCollider2D>();
       else circleCollider2D = GetComponent<CircleCollider2D>();*/
        if (texture != null) animator = texture.GetComponent<Animator>();
        else animator = GetComponent<Animator>();
       
    }
 
    public void SpawnParticles ()
    {
        GameObject _particles = Instantiate(particles,transform.position,Quaternion.identity);
        _particles.GetComponent<ParticleSystem>().startColor = MapManager.instance.ground[MapManager.instance.Map];
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
             texture.GetComponent<Animator>().Play("Destroy");
             Destroy(texture.GetComponent<ColiderTrigger>());
             Destroy(gameObject.transform.parent.gameObject, 1);
            // Destroy(this);
         }
         else
         {
             Destroy(GetComponent<Rigidbody2D>());
             GetComponent<Animator>().Play("Destroy");
            Destroy(gameObject, 1);
            // Destroy(this);
         }     
    }
    void LateUpdate()
    {
       /* if (isTaken)
        {
            if (!PauseManager.instance.Paused && !DefeatManager.instance.Lost)
            {
            
                transform.position = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3));
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(point.transform.position.x, point.transform.position.y, -1), Vector2.down);
                if (hit.collider)
                {
                    if (hit.collider.GetComponent<Bin>())
                    {
                        hit.collider.GetComponent<Bin>().Check(gameObject.layer);
                    }
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }*/
    }
    public void OnMouseDown()
    {
        if (rigidbody)
        {
            isTaken = true;
            SoundManager.instance.PlaySound(ObjectTaken);
            animator.Play("Taken");
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            rigidbody.Sleep();
        }
        
    }

    public void OnMouseOver()
    {
        if (isTaken)
        {
            if (!PauseManager.instance.Paused && !DefeatManager.instance.Lost)
            {

                transform.position = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3));
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(point.transform.position.x, point.transform.position.y, -1), Vector2.down);
                if (hit.collider)
                {
                    if (hit.collider.GetComponent<Bin>())
                    {
                        hit.collider.GetComponent<Bin>().Check(gameObject.layer);
                    }
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnMouseExit()
    {
        if (rigidbody)
        {
            isTaken = false;
            animator.Play("Dropped");
            rigidbody.constraints = RigidbodyConstraints2D.None;
            rigidbody.WakeUp();
        }
    }

    public void OnMouseUp()
    {
        if (rigidbody)
        {
            isTaken = false;
            animator.Play("Dropped");
            rigidbody.constraints = RigidbodyConstraints2D.None;
            rigidbody.WakeUp();
        }
    }
}
