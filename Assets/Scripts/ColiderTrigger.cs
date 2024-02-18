using System;
using UnityEngine;

public class ColiderTrigger : MonoBehaviour
{
    [SerializeField] private Garbage garbage;
    [SerializeField] private GameObject Echo;
    [SerializeField] private static LayerMask _dropEffectLayerMask;
    private float time = 0.2f, timeBetweenSpawn = 0, timePassed;
    private const string _dropEffect = "Prefabs\\Effects\\VFX\\DropEffect";

    private bool _gameStopped = false;

    private void Start()
    {
        EventManager.OnGameStateChanged += EventManager_OnGameStateChanged;
    }

    private void EventManager_OnGameStateChanged()
    {
        _gameStopped = !_gameStopped;
    }

    private void Update()
    {
        if (Time.timeScale > 0.7f) return;
        timeBetweenSpawn -= Time.deltaTime;
        if (timeBetweenSpawn <= 0)
        {
            Destroy(Instantiate(Echo, transform.position, transform.rotation), 0.5f);
            timeBetweenSpawn = time;
        }
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (_gameStopped==false)
        {
            if (collision.gameObject.tag == "Shakable")
            {
                if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody)) EventManager.FireEvent(EventManager.OnVelocityShake,rigidbody.velocity);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _dropEffectLayerMask);
                if (hit) Instantiate(Resources.Load<GameObject>(_dropEffect), new Vector3(hit.point.x, hit.point.y, 1), Quaternion.identity);
            }
            if (garbage.gameObject.layer == collision.gameObject.layer && collision.gameObject.GetComponent<Bin>())
            {
                EventManager.FireEvent(EventManager.OnScoreChanges, 1);
                collision.gameObject.GetComponent<Bin>().PlaySound();
                collision.gameObject.GetComponent<BinVFX>().Particles();
                Debug.Log("Correct");
                garbage.Destroy();
            }
            else if (collision.gameObject.layer == 15)
            {
                garbage.SpawnParticles();
                EventManager.FireEvent(EventManager.OnScoreChanges,-6);
                Debug.Log("Ground");
                garbage.Destroy();
            }
            else if (collision.gameObject.layer != garbage.gameObject.layer && collision.gameObject.GetComponent<Bin>())
            {
                EventManager.FireEvent(EventManager.OnScoreChanges, -3);
                Bin bin = collision.gameObject.GetComponent<Bin>();
                collision.gameObject.GetComponent<Bin>().PlaySound();
                collision.gameObject.GetComponent<BinVFX>().Particles();
                Debug.Log("Wrong");
                garbage.Destroy();
            }
        }
        else
        {
            garbage.Destroy();
        }
    }
}
