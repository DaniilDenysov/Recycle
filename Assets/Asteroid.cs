using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Range(0,100)]
    [SerializeField] private float _forceMax,_forceMin;
    [SerializeField] private GameObject _effect;
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private float _force;
    Vector2 pos;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _force = CalculateForce();
        GenerateDirection();
        pos = transform.position;
        Launch(_direction, ForceMode2D.Impulse);
    }
    public float CalculateForce () => Random.Range(_forceMin, _forceMax);
    public void GenerateDirection ()
    {
        _direction = ((Vector2.right * -1) + (Vector2.up * -1) * Random.Range(1,0)) - (Vector2)transform.position;
    }
    private void Update()
    {
        Debug.DrawLine(pos, _direction, Color.red);
    }
    private void OnDrawGizmos()
    {
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        if (hit) Instantiate(_effect, new Vector3(hit.point.x, hit.point.y, 1), Quaternion.identity);
    }

    public void Launch (Vector2 _dir,ForceMode2D forceMode2D)
    {
        _rigidbody.AddForce(transform.right * -1 * _force, forceMode2D);
    }
}
