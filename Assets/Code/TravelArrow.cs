using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelArrow : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private float _animationSpeed;
    [SerializeField] private float _strength = 1;
    [SerializeField] private BezierCurve _jumpPath;

    private Vector3 _defaultPosition;
    private float _angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        _defaultPosition = _model.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _angle += Time.deltaTime * _animationSpeed;
        var d = Mathf.Sin(_angle) * _strength;

        _model.transform.position = _defaultPosition + transform.right * d;
    }

    public void Travel()
    {
        StartCoroutine(Player.Current.StartJumpProcess(_jumpPath));
    }
}
