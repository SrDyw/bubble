using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DywFunctions;

public class Player : MonoBehaviour
{
    private Plant _targetPlant;
   private Movement _movement;
    private GravityAtraction _atraction;
    [SerializeField] private BezierCurve _targetJump;
    [SerializeField] private float _jumpSpeed;

    private bool _traveling = false;
    public bool Traveling { get => _traveling; set => _traveling = value; }

    bool Interact => Input.GetKeyDown(KeyCode.E);
    private bool _allowInput = true;

    private Rigidbody2D _rb;
    public static Player Current;

    private void Awake()
    {
        Current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<Movement>();
        _atraction = GetComponent<GravityAtraction>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollower.instance.Focus = _atraction.CurrentPlanet.transform;
        if (Interact)
        {
            if (_targetPlant) ProcessPlant(_targetPlant);
        }

        if (Traveling)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
    }

    public IEnumerator StartJumpProcess(BezierCurve targetJumpPath)
    {
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.velocity = Vector2.zero;
        _traveling = true;
        // _movement.enabled = false;

        int currentPointIndex = 0;
        var bezierPoints = targetJumpPath.GetBezierPoints();

        bezierPoints.Show();

        while (true)
        {
            if (currentPointIndex < bezierPoints.Count)
            {
                Vector3 targetPosition = bezierPoints[currentPointIndex];
                targetPosition.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _jumpSpeed * Time.deltaTime);

                if (transform.position == targetPosition)
                {
                    currentPointIndex++;
                }
            }
            else
            {
                _rb.bodyType = RigidbodyType2D.Dynamic;
                _traveling = false;
                yield break; // Finaliza la corutina cuando se alcanzan todos los puntos
            }

            yield return null;
        }
    }

    public void SetInputState(bool value)
    {
        _allowInput = value;
        _movement.enabled = value;
    }

    private void ProcessPlant(Plant plant)
    {
        plant.Float();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_allowInput) return;
        if (Interact)
        {
            print("Try To interact with something");
        }
        if (other.gameObject.CompareTag("Bubble"))
        {

            var bubble = other.GetComponent<Bubble>();
            bubble.Pick();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plant"))
        {
            var plant = other.GetComponent<Plant>();
            _targetPlant = plant;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Plant") && other.gameObject == _targetPlant.gameObject)
        {
            _targetPlant = null;
        }
    }
}
