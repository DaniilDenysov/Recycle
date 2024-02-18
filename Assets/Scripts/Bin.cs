using UnityEngine;
using UnityEngine.UI;

public class Bin : MonoBehaviour,IDamagable
{
    [SerializeField] private AudioClip audioClip;
    private float _health = 100f;

    public void Damage(float damage)
    {
        _health -= damage;
        if (_health <= 0) EventManager.FireEvent(EventManager.OnDefeat);
    }
    public void PlaySound ()
    {
        EventManager.FireEvent(EventManager.OnSoundPlayed,audioClip);
    }
}
