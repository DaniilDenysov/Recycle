using UnityEngine;
using UnityEngine.UI;

public class Bin : MonoBehaviour,IDamagable
{
     private int Acceptable;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Image hp_bar_UI;
    [SerializeField] private Animator hp_bar;
    private float _health = 100f;

    public void Damage(float damage)
    {
        _health -= damage;
        Debug.Log("HP: " + _health);
        if (_health <= 0) DefeatManager.instance.Defeat();
    }
    public void PlaySound ()
    {
        SoundManager.instance.PlaySound(audioClip);
    }
}
