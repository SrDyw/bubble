using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpFoce = 20;

    [Space]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _foot;

    private Rigidbody2D _rb;
    private GravityAtraction _gravityAtraction;
    private bool _isGrounded;
    private float _jumpThreshold = 0;

    private Timer _stepTimer;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gravityAtraction = GetComponent<GravityAtraction>();
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapPoint(_foot.position, _groundLayer);
        _jumpThreshold = Mathf.Clamp(_jumpThreshold - Time.deltaTime, 0, _jumpThreshold);


        if (Input.GetKeyDown(KeyCode.Space) && _jumpThreshold == 0 && _isGrounded)
        {
            _rb.AddForce(transform.up * _jumpFoce, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hInput = Input.GetAxisRaw("Horizontal");
        if (hInput != 0)
        {
            var direction = transform.right.normalized * Mathf.Sign(hInput);
            var translationVector = direction * _speed * Time.fixedDeltaTime;
            transform.position += translationVector;

            if (_stepTimer == null || _stepTimer.Active == false)
            {
                if (_isGrounded)
                {
                    _gravityAtraction?.CurrentPlanet.SurfaceStep(transform.up);
                    Timer.CreateOrRestartTimer(ref _stepTimer, 0.3f, gameObject, () =>
                    {
                        _gravityAtraction?.CurrentPlanet.SurfaceStep(transform.up);
                    });
                }
            }
        }
        else
        {
            if (_stepTimer != null && _stepTimer.Active) _stepTimer.Stop();
        }
    }
}
