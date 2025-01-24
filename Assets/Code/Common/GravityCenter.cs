using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCenter : MonoBehaviour
{
    [SerializeField] private float _gravityAtraction = 10;

    public float GravityAtraction { get => _gravityAtraction; set => _gravityAtraction = value; }
}
