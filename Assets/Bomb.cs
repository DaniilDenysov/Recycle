using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bomb : MonoBehaviour,IWarning
{
    [Range(1,100f)]
    [SerializeField] private float explosionForce,explosionRadius,Damage;
    [Range(1,60f)]
    [SerializeField] private float bombTimer = 10f;
    [SerializeField] private ParticleSystem _particleSystem;
    private Rigidbody2D _rigidbody;
    private bool _exploded = false,_isTaken = false;
    private Camera _camera;
    [SerializeField] private GameObject warningPref;
    private GameObject warning;
    private CinemachineImpulseSource _cinemachineImpulseSource;

    private void Start()
    {
        warning = Instantiate(warningPref, transform.position, Quaternion.identity);
        _rigidbody = GetComponent<Rigidbody2D>();       
        _camera = Camera.main;
        _cinemachineImpulseSource = FindObjectOfType<CinemachineImpulseSource>(); 
        Invoke(nameof(Explode),bombTimer);
    }
    void Update()
    {
        Follow();
      //  bombTimer -= Time.deltaTime;
       // if (bombTimer <= 0) Explode();
    }
    private void OnDestroy()
    {
        Destroy(warning);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
    //   Explode();        
    }
    private void Explode ()
    {
        if (_exploded) return;
        List<Collider2D> list = new List<Collider2D>();
        _cinemachineImpulseSource.GenerateImpulse(100f);
        list.AddRange(Physics2D.OverlapCircleAll(transform.position, explosionRadius));
        ExplodeDamage(list);
        ExplosionPhysics(Physics2D.OverlapCircleAll(transform.position, explosionRadius));
        VisualizeExplosion();
        _exploded = true;
        Destroy(gameObject);
    }
    public void Deffuse ()
    {
        Destroy(gameObject);
    }
    private void VisualizeExplosion ()
    {      
        _particleSystem.Play();
        _particleSystem.gameObject.transform.parent.SetParent(null);
    }
    private void ExplodeDamage (List<Collider2D> objects)
    {
        foreach (Collider2D obj in objects)
        {
            if (obj.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                Debug.Log(obj.gameObject.name);
                damagable.Damage(Damage);
            }
        }
    }
    private void ExplosionPhysics (Collider2D [] objects)
    {
        foreach (Collider2D obj in objects)
        {
            if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody))
            {
                rigidbody.AddForce(rigidbody.gameObject.transform.position - transform.position * explosionForce * Time.deltaTime   ,ForceMode2D.Impulse);
            }
        }
    }
    public void OnMouseOver()
    {
       if (_isTaken == true) transform.position = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3));
    }
    public void OnMouseDown()
    {
        if (_rigidbody)
        {
            _isTaken = true;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            _rigidbody.Sleep();
        }

    }
    public void OnMouseExit()
    {
        if (_rigidbody)
        {
            _isTaken = false;            
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            _rigidbody.WakeUp();
        }
    }
    public void OnMouseUp()
    {
        if (_rigidbody)
        {
            _isTaken = false;
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            _rigidbody.WakeUp();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
    public void Follow()
    {
        warning.transform.position = transform.position;
    }
}
