using System;
using UnityEngine;

public class Garbage : Dragable
{
    [SerializeField] private Transform point;
    [SerializeField] private GameObject texture;
    [SerializeField] private GameObject particles;
    [SerializeField] private AudioClip ObjectTaken, ObjectDropped;
    [SerializeField] private LayerMask collisionIgnore;
    private Animator animator;
    private Camera camera;
    private HingeJoint2D hingeJoint2D;
    private BoxCollider2D boxCollider2D;
    private CircleCollider2D circleCollider2D;

    protected override void Start()
    {
        base.Start();
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
       // _particles.GetComponent<ParticleSystem>().startColor = MapManager.instance.ground[MapManager.instance.Map];
        Destroy(_particles,1);
    }

    public void changeState()
    {
        if (texture != null) transform.parent.gameObject.SetActive(false);
        else gameObject.SetActive(false);
    }


    public void Destroy ()
    {
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

    protected override void OnMouseDown()
    {
        SoundManager.instance.PlaySound(ObjectTaken);
        animator.Play("Taken");
        base.OnMouseDown();
    }

    public void OnMouseOver()
    {
        if (_isTaken)
        {
            if (!PauseManager.instance.Paused && !DefeatManager.instance.Lost)
            {

                transform.position = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3));
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(point.transform.position.x, point.transform.position.y, -1), Vector2.down);
                if (hit.collider)
                    if (hit.collider.TryGetComponent<Help>(out Help help))
                       help.Check(gameObject.layer);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    protected override void OnMouseExit()
    {
        animator.Play("Dropped");
        base.OnMouseExit();
    }

    protected override void OnMouseUp()
    {
        animator.Play("Dropped");
        base.OnMouseUp();
    }
}
