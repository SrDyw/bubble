using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DywFunctions.MathExtras;

public class GravityAtraction : MonoBehaviour
{
    [SerializeField] private string _gravityAreaTag = "Gravity Area";
    private Rigidbody2D _rb;

    private GravityCenter _center;
    private GameObject _gravityArea;
    private Planet _currentPlanet;

    private float _defaulGravityScale = 1;

    public Planet CurrentPlanet { get => _currentPlanet; set => _currentPlanet = value; }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _defaulGravityScale = _rb.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var gravityDirection = Physics2D.gravity;

        if (_center)
        {
            gravityDirection = (_center.transform.position - transform.position).normalized;

            _rb.AddForce(gravityDirection * _center.GravityAtraction, ForceMode2D.Force);
        }
        RotateTowardGravityDirection(gravityDirection);
    }

    private void RotateTowardGravityDirection(Vector2 gravityDirection)
    {
        transform.rotation = Quaternion.FromToRotation(transform.up, -gravityDirection) * transform.rotation;
    }

    public void EnterInGravityArea(GameObject gravityArea)
    {
        _gravityArea = gravityArea;
        var gravityCenter = _gravityArea.GetComponentInParent<GravityCenter>();
        if (gravityCenter)
        {
            _center = gravityCenter;
            _currentPlanet = _center.GetComponent<Planet>();
            _rb.gravityScale = 0;

        }
        else
        {
            Debug.Log($"The current gravity area ({gravityArea.name}) doesnt have a gravity center attached");
        }
    }

    public void LeaveGravityArea()
    {
        _gravityArea = null;
        _center = null;
        _rb.gravityScale = _defaulGravityScale;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_gravityAreaTag))
        {
            EnterInGravityArea(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == _gravityArea)
        {
            LeaveGravityArea();
        }
    }
}
