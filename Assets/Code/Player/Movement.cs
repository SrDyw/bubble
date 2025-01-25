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
    private Vector2 _direction;
    private bool _isMoving = false;
    private float _horizontalInput;
    private Timer _stepTimer;
   
    public bool IsMoving { get => _isMoving; }
    public float JumpThreshold { get => _jumpThreshold; set => _jumpThreshold = value; }
    public Vector2 Direction { get => _direction; set => _direction = value; }
    public float HorizontalInput { get => _horizontalInput; set => _horizontalInput = value; }
    public bool IsGrounded { get => _isGrounded; set => _isGrounded = value; }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gravityAtraction = GetComponent<GravityAtraction>();
    }

    private void Update()
    {
        IsGrounded = Physics2D.OverlapPoint(_foot.position, _groundLayer);
        JumpThreshold = Mathf.Clamp(JumpThreshold - Time.deltaTime, 0, JumpThreshold);

        // if (Input.GetKeyDown(KeyCode.Space) && JumpThreshold == 0 && IsGrounded)
        // {
        //     _rb.AddForce(transform.up * _jumpFoce, ForceMode2D.Impulse);
        // }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        HorizontalInput = Input.GetAxisRaw("Horizontal");
        if (HorizontalInput != 0)
        {
            Direction = transform.right.normalized * HorizontalInput;
            var translationVector = Direction * _speed * Time.fixedDeltaTime;
            transform.position += (Vector3)translationVector;

            if (_stepTimer == null || _stepTimer.Active == false)
            {
                if (IsGrounded)
                {
                    _isMoving = true;
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
            _isMoving = false;
        }
    }
}
