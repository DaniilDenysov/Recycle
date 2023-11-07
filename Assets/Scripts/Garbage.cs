using System;
using UnityEngine;

public class Garbage : Dragable
{
    [SerializeField] private GameObject texture;
    [SerializeField] private GameObject particles;
    [SerializeField] private AudioClip ObjectTaken, ObjectDropped;
    private CircleCollider2D _circleCollider2D;
    private Animator _animator;
    private HingeJoint2D _hingeJoint2D;
    private ColiderTrigger _coliderTrigger;

    protected override void Start()
    {
        base.Start();
        _hingeJoint2D = GetComponent<HingeJoint2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _coliderTrigger = texture.GetComponent<ColiderTrigger>();
        _animator = texture.GetComponent<Animator>();     
    }

    public void SpawnParticles ()
    {
        Instantiate(particles,transform.position,Quaternion.identity);
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

    protected override void FixedUpdate()
    {
        if (_gameStopped) return;
        if (!_isTaken) return;
        base.FixedUpdate();
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(_circleCollider2D.bounds.center.x, _circleCollider2D.bounds.center.y - _circleCollider2D.radius - 0.1f, -1), Vector2.down);
        if (hit.collider)
            if (hit.collider.TryGetComponent<Help>(out Help help))
                help.Check(gameObject.layer);
    }

    protected override void OnMouseUp()
    {
        _animator.Play("Dropped");
        base.OnMouseUp();
    }
}
