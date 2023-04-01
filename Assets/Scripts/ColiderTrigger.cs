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
        GameBrakeManager.OnBrake += GameBrakeManager_OnBrake;
    }

    private void GameBrakeManager_OnBrake(object sender, bool e)
    {
        _gameStopped = e;
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

        if (!_gameStopped)
        {
            ScreenShakeManager.instance.VelocityShake(GetComponent<Rigidbody2D>().velocity);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,_dropEffectLayerMask);
            if (hit) Instantiate(Resources.Load<GameObject>(_dropEffect), new Vector3(hit.point.x, hit.point.y, 1), Quaternion.identity);
            Debug.Log("Normalized:" + GetComponent<Rigidbody2D>().velocity.normalized);
            if (garbage.gameObject.layer == collision.gameObject.layer && collision.gameObject.GetComponent<Bin>())
            {
                ScoreManager.instance.AddScore(1);
                collision.gameObject.GetComponent<Bin>().PlaySound();
                collision.gameObject.GetComponent<BinVFX>().Particles();
                garbage.Destroy();
            }
            else if (collision.gameObject.layer == 15)
            {
                garbage.SpawnParticles();
                ScoreManager.instance.AddScore(-6);
                garbage.Destroy();
            }
            else if (collision.gameObject.layer != garbage.gameObject.layer && collision.gameObject.GetComponent<Bin>())
            {
                ScoreManager.instance.AddScore(-3);
                Bin bin = collision.gameObject.GetComponent<Bin>();
                collision.gameObject.GetComponent<Bin>().PlaySound();
                collision.gameObject.GetComponent<BinVFX>().Particles();
                garbage.Destroy();
            }
        }
        else
        {
            garbage.Destroy();
        }
    }
}
