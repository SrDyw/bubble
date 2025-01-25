using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bubble : Item
{
    [SerializeField] private BubbleType _type;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _initalForce = 5;
    [SerializeField] private float _speed;
    [SerializeField] private float _activeTime;
    [SerializeField] private float _playerAtractionDistance = 1;

    private bool _isWild = true;
    private bool _isUsed = false;
    private Vector2 direction;
    private Rigidbody2D _rb;

    private Vector2 _defaultScale;
    private Tween _scaleTween;

    public BubbleType BubbleType { get => _type; set => _type = value; }
    public Vector2 Direction { get => direction; set => direction = value; }
    public bool IsWild { get => _isWild; set => _isWild = value; }
    public bool IsUsed { get => _isUsed; set => _isUsed = value; }


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(Direction * _initalForce, ForceMode2D.Impulse);

        _defaultScale = transform.localScale;

        StartCoroutine(DesapearProcess());
    }

    public override void Pick()
    {
        if (IsUsed) return;
        
        base.Pick();
        _scaleTween?.Kill();

        transform.localScale = _defaultScale;
        Direction = Vector2.zero;
        _isWild = false;
    }

    private void OnDestroy()
    {
        _scaleTween?.Kill();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isWild)
        {
            var targetDirection = Direction;
            if (Vector3.Distance(Player.Current.transform.position, transform.position) < _playerAtractionDistance)
            {
                targetDirection = (Player.Current.transform.position - transform.position).normalized * 10;
            }
                _rb.AddForce(targetDirection * _acceleration, ForceMode2D.Force);

                _rb.velocity = new Vector2(
                    Mathf.Clamp(_rb.velocity.x, -_speed, _speed),
                    Mathf.Clamp(_rb.velocity.y, -_speed, _speed)
                );
        }
    }



    private IEnumerator DesapearProcess()
    {
        yield return new WaitForSeconds(_activeTime);
        _scaleTween = transform.DOScale(Vector3.zero, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(() => Destroy(gameObject));
    }

    public override string ToString()
    {
        return $"@Bubble {_type.ToString()} ID {IdInInventory}";
    }
}
