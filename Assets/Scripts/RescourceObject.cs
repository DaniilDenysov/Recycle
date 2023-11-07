using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescourceObject : MonoBehaviour
{
    [Range(0.001f, 100)]
    [SerializeField] private float _probobility = 1;
    [SerializeField] private GameObject _prefab;

    public RescourceObject(GameObject _prefab)
    {
        this._prefab = _prefab;
    }
    public RescourceObject(string _prefab)
    {
        this._prefab = Resources.Load<GameObject>(_prefab);
    }
    public GameObject GetPrefab() { return _prefab; }
}
