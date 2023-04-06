using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected bool _isTaken = false;
    protected bool _gameStopped = false;
    protected Camera _camera;

    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        GameBrakeManager.OnBrake += GameBrakeManager_OnBrake;
    }

    protected virtual void GameBrakeManager_OnBrake(object sender, bool e)
    {
        _gameStopped = e;
    }

    protected virtual void OnMouseDown()
    {
     if (_rigidbody == null) return;
     _isTaken = true;
     _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
     _rigidbody.Sleep();
    }


    protected virtual void FixedUpdate()
    {
        if (_gameStopped) return;
        if (!_isTaken) return;
        transform.position = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2));
    }

    protected virtual void OnMouseUp()
    {
     if (_rigidbody == null) return;
     _isTaken = false;
     _rigidbody.constraints = RigidbodyConstraints2D.None;
     _rigidbody.WakeUp();
    }

}
