using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private GameObject particles;
    [SerializeField] private AudioClip ObjectTaken, ObjectDropped;
    public PauseManager pauseManager;
    public DefeatManager defeatManager;
    public ScoreManager scoreManager;
    bool isTaken;

    private void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        defeatManager = FindObjectOfType<DefeatManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        Debug.Log(gameObject.name);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!pauseManager.Paused && !defeatManager.Lost)
        {
            if (gameObject.layer == collision.gameObject.layer && collision.gameObject.GetComponent<Bin>())
            {
                scoreManager.AddScore(1);
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
    void Destroy ()
    {
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
        GetComponent<Animator>().Play("Destroy");
        Destroy(gameObject, 1);
    }
    void FixedUpdate()
    {       
        if (isTaken)
        {
            if (!pauseManager.Paused && !defeatManager.Lost)
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3));
                RaycastHit2D hit = Physics2D.Raycast(new Vector3(point.transform.position.x, point.transform.position.y, -1), Vector2.down);
              //  Debug.DrawLine(transform.position, Vector2.down, Color.red);
                if (hit.collider)
                {
                    Debug.Log("Hitted:" + hit.collider.name);
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
    public void OnMouseDown()
    {
        if (!isTaken)
        {
            isTaken = true;
            GetComponent<AudioSource>().PlayOneShot(ObjectTaken);
            GetComponent<CircleCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<Rigidbody2D>().Sleep();
        }
    }

    public void OnMouseUp()
    {
        isTaken = false;
        GetComponent<AudioSource>().PlayOneShot(ObjectDropped);
        GetComponent<CircleCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().WakeUp();
    }
}
