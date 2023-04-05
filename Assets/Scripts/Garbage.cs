using System;
using UnityEngine;

public class Garbage : Dragable
{
    [SerializeField] private Transform point;
    [SerializeField] private GameObject texture;
    [SerializeField] private GameObject particles;
    [SerializeField] private AudioClip ObjectTaken, ObjectDropped;
    [SerializeField] private LayerMask collisionIgnore;
    private Animator _animator;
    private Camera _camera;
    private HingeJoint2D _hingeJoint2D;
    private BoxCollider2D _boxCollider2D;
    private CircleCollider2D _circleCollider2D;
    private ColiderTrigger _coliderTrigger;
    private bool _gameStopped = false;

    float fixedRadius;
    Vector2 def;

    protected override void Start()
    {
        base.Start();
        GameBrakeManager.OnBrake += GameBrakeManager_OnBrake;
        _hingeJoint2D = GetComponent<HingeJoint2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _camera = Camera.main;
        fixedRadius = _circleCollider2D.radius;
           /*   if (GetComponent<BoxCollider2D>()) boxCollider2D = GetComponent<BoxCollider2D>();
              else circleCollider2D = GetComponent<CircleCollider2D>();*/
           _coliderTrigger = texture.GetComponent<ColiderTrigger>();
            _animator = texture.GetComponent<Animator>();     
    }

    private void GameBrakeManager_OnBrake(object sender, bool e)
    {
        _gameStopped = e;
    }

    public void SpawnParticles ()
    {
        Instantiate(particles,transform.position,Quaternion.identity);
    }

    private void ChangeDragPointRadius ()
    {
        def = _circleCollider2D.offset;
        _circleCollider2D.offset = new Vector2(0,0);
        _circleCollider2D.radius = fixedRadius * 3;
    }

    private void ChangeDragPointRadiusToDef()
    {
        _circleCollider2D.offset = def;
        _circleCollider2D.radius = fixedRadius;
    }


    public void Destroy()
    {
        Destroy(_hingeJoint2D);
        Destroy(_rigidbody);
        texture.GetComponent<Animator>().Play("Destroy");
        Destroy(texture.GetComponent<ColiderTrigger>());
        Destroy(gameObject.transform.parent.gameObject, 1);
    }

    protected override void OnMouseDown()
    {
        SoundManager.instance.PlaySound(ObjectTaken);
        _animator.Play("Taken");
        base.OnMouseDown();
    }

    public void OnMouseOver()
    {
        if (_gameStopped) return;
        if (!_isTaken) return;
        transform.position = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,2));
        ChangeDragPointRadius();
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - _circleCollider2D.radius, -1), Vector2.down);
        if (hit.collider)
           if (hit.collider.TryGetComponent<Help>(out Help help))
                  help.Check(gameObject.layer);
    }

    protected override void OnMouseExit()
    {
        _animator.Play("Dropped");
        ChangeDragPointRadiusToDef();
        base.OnMouseExit();
    }

    protected override void OnMouseUp()
    {
        _animator.Play("Dropped");
        ChangeDragPointRadiusToDef();
        base.OnMouseUp();
    }
}
