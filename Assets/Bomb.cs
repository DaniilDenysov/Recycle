using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bomb : MonoBehaviour,IWarning
{
    [Range(1,100f)]
    [SerializeField] private float _explosionForce,_explosionRadius,_damage;
    [Range(1,60f)]
    [SerializeField] private float _bombTimer = 10f;
    [SerializeField] private ParticleSystem _particleSystem;
    private bool _exploded = false;
    private const string _dropEffect = "Prefabs\\Effects\\VFX\\DropEffect";
    private Camera _camera;
    [SerializeField] private GameObject warningPref;
    private GameObject warning;
  //  private CinemachineImpulseSource _cinemachineImpulseSource;

    private void Start()
    {
        warning = Instantiate(warningPref, transform.position, Quaternion.identity);  
        _camera = Camera.main;
     //   _cinemachineImpulseSource = FindObjectOfType<CinemachineImpulseSource>(); 
        Invoke(nameof(Explode),_bombTimer);
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

    private void Explode ()
    {
        if (_exploded) return;
        List<Collider2D> list = new List<Collider2D>();
        ScreenShakeManager.instance.Shake(50f);
        list.AddRange(Physics2D.OverlapCircleAll(transform.position, _explosionRadius));
        ExplodeDamage(list);
        ExplosionPhysics(Physics2D.OverlapCircleAll(transform.position, _explosionRadius));
        VisualizeExplosion();
        _exploded = true;
        DestroyBomb();
    }

    public void DestroyBomb ()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Shakable") return;
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody)) ScreenShakeManager.instance.VelocityShake(rigidbody.velocity);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        if (hit) Instantiate(Resources.Load<GameObject>(_dropEffect),new Vector3(hit.point.x, hit.point.y, 1), Quaternion.identity);
    }

    public void Deffuse ()
    {
        GetComponent<AudioSource>().Play();
        Destroy(GetComponent<Rigidbody2D>());
        _exploded = true;
        Destroy(warning);
        GetComponent<Animator>().Play("Deffuse");
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
             
                damagable.Damage(_damage);
            }
        }
    }

    private void ExplosionPhysics (Collider2D [] objects)
    {
        foreach (Collider2D obj in objects)
        {
            if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody))
            {
                rigidbody.AddForce(rigidbody.gameObject.transform.position - transform.position * _explosionForce * Time.deltaTime,ForceMode2D.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,_explosionRadius);
    }
    public void Follow()
    {
       if (warning != null) warning.transform.position = transform.position;
    }
}
