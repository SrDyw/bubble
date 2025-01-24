using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationByPlanet : MonoBehaviour
{
    [SerializeField] private GameObject _planet;
    [SerializeField] private bool _invert;
    // Start is called before the first frame update
    void Start()
    {
        var direction = (_planet.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(transform.up, -direction * (_invert ? -1 : 1)) * transform.rotation;
    }
}
