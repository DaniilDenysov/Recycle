using UnityEngine;

public class ColiderTrigger : MonoBehaviour
{
    [SerializeField] private Garbage garbage;

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (!PauseManager.instance.Paused && !DefeatManager.instance.Lost)
        {
            if (garbage.gameObject.layer == collision.gameObject.layer && collision.gameObject.GetComponent<Bin>())
            {
                ScoreManager.instance.AddScore(1);
                collision.gameObject.GetComponent<AudioSource>().Play();
                collision.gameObject.GetComponent<Bin>().Particles();
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
                collision.gameObject.GetComponent<AudioSource>().Play();
                collision.gameObject.GetComponent<Bin>().Particles();
                garbage.Destroy();
            }
        }
        else
        {
            garbage.Destroy();
        }
    }
}
