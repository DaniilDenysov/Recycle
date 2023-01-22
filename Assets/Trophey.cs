using UnityEngine;

public class Trophey : MonoBehaviour
{
    [SerializeField] private int ID;
    [SerializeField] private AudioClip Trophey_Taken;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        FindObjectOfType<DataManager>().saveData(ID);
        audioSource.PlayOneShot(Trophey_Taken);
        animator.Play("Trophey_taken");
        Destroy(rigidbody);
        Destroy(this.gameObject, 0.4f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Garbage")
        {
            animator.Play("Trophey_destroyed");
            Destroy(rigidbody);
            Destroy(this.gameObject, 0.4f);
        }
    }
}
