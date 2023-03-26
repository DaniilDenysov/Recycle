using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinVFX : MonoBehaviour
{
    [SerializeField] private string _path;
    [SerializeField] private string _name;

    private ParticleSystem _particleSystem;

    private void Start()
    {
        Debug.Log(_path + _name + gameObject.layer);
        _particleSystem = Instantiate(Resources.Load<GameObject>(_path + _name + gameObject.layer)).GetComponent<ParticleSystem>();
        _particleSystem.transform.SetParent(gameObject.transform);
        _particleSystem.transform.localPosition = new Vector2(0, 1.4f);
        _particleSystem.transform.localScale = new Vector3(1, 1, 1);
    }

    public void Particles()
    {
        _particleSystem.Play();
    }
}
