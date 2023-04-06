using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float _forceMax, _forceMin;
    [Range(-360, 360)]
    [SerializeField] private float _angleRange, _length, _angleGap;

    [SerializeField] private Color _angleLimitColor, _angleRangeColor;
    private const string _dropEffect = "Prefabs\\Effects\\VFX\\AsteroidEffect", _asteroidParts = "Prefabs\\Effects\\VFX\\AsteroidParts";
    private Rigidbody2D _rigidbody;
    private Vector3 _direction;
    private float _force;

    Vector3 _limitA, _limitB, _source;

    private Animator _animator;
    private bool pressed;
    Vector2 pos;
    Vector3 _angle;
    private bool stop;
    void Start()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        _force = CalculateForce();
        _animator = GetComponent<Animator>();
        GenerateDirection();
        _source = ((Vector2.right * -1) + (Vector2.up * -1)) - (Vector2)transform.position;
        ChangeAngle(Random.Range(_angleRange - _angleGap / 2, _angleRange + _angleGap / 2));
        pos = transform.position;
        SwipeDetection.instance.swipePerformed += Move;
        //transform.rotation = Quaternion.Euler(_direction);
    }


    private void OnMouseDown()
    {
        pressed = true;
    }

    private void OnMouseUp()
    {
        pressed = false;
    }

    public void Move (Vector2 dir)
    {
        if (!pressed) return;
        _rigidbody.gravityScale = 1;
        _rigidbody.constraints = RigidbodyConstraints2D.None;
        _rigidbody.AddForce(_direction * - 1 * 10,ForceMode2D.Impulse);
    }

    public float CalculateForce() => Random.Range(_forceMin, _forceMax);
    public void GenerateDirection()
    {
        _direction = ((Vector2.right * -1) + (Vector2.up * -1)) - (Vector2)transform.position;
    }
    private void Update()
    {
        if (stop) return;
        Launch(_direction, ForceMode2D.Force);
        Debug.DrawRay(pos, _direction, Color.red);
    }
    private void OnDrawGizmos()
    {

        _source = ((Vector2.right * -1) + (Vector2.up * -1)) - (Vector2)transform.position;
        _limitA = Quaternion.AngleAxis(_angleRange - _angleGap / 2, Vector3.forward) * _source;
        _limitB = Quaternion.AngleAxis(_angleRange + _angleGap / 2, Vector3.forward) * _source;


        // ChangeAngle(Random.Range(_angleRange, -_angleRange));
        //  ChangeAngle(Random.Range(_angleRange - _angleGap / 2, _angleRange + _angleGap / 2));
        //Gizmos.color = _angleRangeColor;
        //Gizmos.DrawRay(transform.position, _direction * _length);

        Gizmos.color = _angleLimitColor;
        Gizmos.DrawRay(transform.position, _limitA * _length);
        Gizmos.DrawRay(transform.position, _limitB * _length);

    }

    private void ChangeAngle(float angle)
    {
        _direction = Quaternion.AngleAxis(angle, Vector3.forward) * _source;
        transform.rotation = Quaternion.Euler(0,0,Vector2.Angle(transform.right * -1, _direction));
        Debug.Log("Dir:" + _direction);
    }

    public void DestroyAsteroid() => Destroy(gameObject);

    public void OnDestroy()
    {
        //Instantiate(Resources.Load<GameObject>(_asteroidParts), transform.position, Quaternion.identity);
       // Instantiate(Resources.Load<GameObject>(_dropEffect), new Vector3(transform.position.x, transform.position.y - (GetComponent<CircleCollider2D>().radius / 2), 2), Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (stop) return;
        stop = true;
        ScreenShakeManager.instance.Shake(200);
        //  _animator.Play("Asteroid_Destroy");
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        //Destroy(GetComponentInChildren<ParticleSystem>().gameObject);
    }

    public void SetAngle(float _newAngle) => _angleRange = _newAngle;

    public void Launch(Vector2 _dir, ForceMode2D forceMode2D)
    {
        _rigidbody.AddForce(_dir * _force, forceMode2D);
    }
}
