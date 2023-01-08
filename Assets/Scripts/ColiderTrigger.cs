using UnityEngine;

public class ColiderTrigger : MonoBehaviour
{
    [SerializeField] private Garbage garbage;
    

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (!garbage.pauseManager.Paused && !garbage.defeatManager.Lost)
        {
            if (garbage.gameObject.layer == collision.gameObject.layer && collision.gameObject.GetComponent<Bin>())
            {
                garbage.scoreManager.AddScore(1);
                collision.gameObject.GetComponent<AudioSource>().Play();
                collision.gameObject.GetComponent<Bin>().Particles();
                garbage.Destroy();
            }
            else if (collision.gameObject.layer == 15)
            {
                garbage.SpawnParticles();
                garbage.scoreManager.AddScore(-6);
                garbage.Destroy();
            }
            else if (collision.gameObject.layer != garbage.gameObject.layer && collision.gameObject.GetComponent<Bin>())
            {
                garbage.scoreManager.AddScore(-3);
                collision.gameObject.GetComponent<AudioSource>().Play();
                collision.gameObject.GetComponent<Bin>().Particles();
                garbage.Destroy();
            }
        }
        else
        {
            Destroy(gameObject);
        }
        /*if (!garbage.pauseManager.Paused && !garbage.defeatManager.Lost)
        {
            if (gameObject.layer == collision.gameObject.layer && collision.gameObject.GetComponent<Bin>())
            {
                garbage.scoreManager.AddScore(1);
                collision.gameObject.GetComponent<AudioSource>().Play();
                collision.gameObject.GetComponent<Bin>().Particles();
                garbage.Destroy();
            }
            else if (collision.gameObject.layer == 15)
            {
                garbage.SpawnParticles();
                garbage.scoreManager.AddScore(-6);
                garbage.Destroy();
            }
            else if (collision.gameObject.layer != gameObject.layer && collision.gameObject.GetComponent<Bin>())
            {
                garbage.scoreManager.AddScore(-3);
                collision.gameObject.GetComponent<AudioSource>().Play();
                collision.gameObject.GetComponent<Bin>().Particles();
                garbage.Destroy();
            }
        }
        else
        {
            Destroy(gameObject);
        }*/
       // garbage.detectColider(collision);    
    }
}
