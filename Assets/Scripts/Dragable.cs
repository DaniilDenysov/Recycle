using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected bool _isTaken = false;

    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnMouseDown()
    {
     if (_rigidbody == null) return;
     _isTaken = true;
     _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
     _rigidbody.Sleep();
    }
    protected virtual void OnMouseExit()
    {
      if (_rigidbody == null) return;
     _isTaken = false;
     _rigidbody.constraints = RigidbodyConstraints2D.None;
     _rigidbody.WakeUp();
    }
    protected virtual void OnMouseUp()
    {
     if (_rigidbody == null) return;
     _isTaken = false;
     _rigidbody.constraints = RigidbodyConstraints2D.None;
     _rigidbody.WakeUp();
    }

    /*
      public void OnMouseOver()
    {
       if (_isTaken == true) transform.position = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3));
    }
     */
}
