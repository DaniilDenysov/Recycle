using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoFade : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;
    private Color _color;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;
    }

    private void LateUpdate()
    {
        _color.a -= 0.001f;
        _spriteRenderer.color = _color;
    }
}
