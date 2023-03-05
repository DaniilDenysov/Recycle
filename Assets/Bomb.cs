using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Range(1,10f)]
    [SerializeField] private float explosionForce,explosionRadius;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Explode(Physics2D.OverlapCircleAll(transform.position, explosionRadius));
            ExplosionPhysics(Physics2D.OverlapCircleAll(transform.position, explosionRadius));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
   
    }
    private void Explode (Collider2D [] objects)
    {
        foreach (Collider2D obj in objects)
        {
            if (obj.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                damagable.Damage(101);
            }
        }
    }

    private void ExplosionPhysics (Collider2D [] objects)
    {
        foreach (Collider2D obj in objects)
        {
            if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody))
            {
                Debug.Log("rb");
                rigidbody.AddForce(rigidbody.gameObject.transform.position - transform.position * explosionForce * Time.deltaTime   ,ForceMode2D.Impulse);
            }
        }
    }

}
