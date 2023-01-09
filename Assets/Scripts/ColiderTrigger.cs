using UnityEngine;

public class ColiderTrigger : MonoBehaviour
{
    [SerializeField] private Garbage garbage;

    private PauseManager pauseManager;
    private DefeatManager defeatManager;
    private ScoreManager scoreManager;

    private void Start()
    {
        pauseManager = garbage.pauseManager;
        defeatManager = garbage.defeatManager;
        scoreManager = garbage.scoreManager;
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (!pauseManager.Paused && !defeatManager.Lost)
        {
            if (garbage.gameObject.layer == collision.gameObject.layer && collision.gameObject.GetComponent<Bin>())
            {
                scoreManager.AddScore(1);
                collision.gameObject.GetComponent<AudioSource>().Play();
                collision.gameObject.GetComponent<Bin>().Particles();
                garbage.Destroy();
            }
            else if (collision.gameObject.layer == 15)
            {
                garbage.SpawnParticles();
                scoreManager.AddScore(-6);
                garbage.Destroy();
            }
            else if (collision.gameObject.layer != garbage.gameObject.layer && collision.gameObject.GetComponent<Bin>())
            {
                scoreManager.AddScore(-3);
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
