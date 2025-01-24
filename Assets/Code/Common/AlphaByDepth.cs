using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaByDepth : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float maxDistance = 200;
    // Start is called before the first frame update
    void Start()
    {
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 1 - (transform.position.z/maxDistance));
    }
}
