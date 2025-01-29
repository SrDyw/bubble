using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bubble : Item
{
    [SerializeField] protected BubbleType _type;
    [SerializeField] protected float _acceleration;
    [SerializeField] protected float _initalForce = 5;
    [SerializeField] protected float speed;
    [SerializeField] private float activeTime;
    [SerializeField] private bool _permanent = false;
    [SerializeField] protected float _playerAtractionDistance = 1;

    protected SpriteRenderer _renderer;

    protected bool _isWild = true;
    protected bool _isUsed = false;
    protected Vector2 direction;
    protected Rigidbody2D _rb;

    protected Vector2 _defaultScale;
    protected Tween _scaleTween;
    public bool FollowPlayer = true;

    public BubbleType BubbleType { get => _type; set => _type = value; }
    public Vector2 Direction { get => direction; set => direction = value; }
    public bool IsWild { get => _isWild; set => _isWild = value; }
    public bool IsUsed { get => _isUsed; set => _isUsed = value; }
    public float Speed { get => speed; set => speed = value; }
    public float ActiveTime { get => activeTime; set => activeTime = value; }


    // Start is called before the first frame update
    void Start()
    {
        // print(Direction);
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(Direction * _initalForce, ForceMode2D.Impulse);

        _defaultScale = transform.localScale;

        if (_permanent == false) StartCoroutine(DesapearProcess());
        _renderer = GetComponent<SpriteRenderer>();

        _renderer.color = Scheme.Color;
        _renderer.sprite = Scheme.Thumb;

        if (Vector2.Distance(Player.Current.transform.position, transform.position) < 20)
            AudioManager.instance.PlaySFX("BubbleAppear");
    }

    public override void Pick()
    {
        if (IsUsed) return;

        base.Pick();
        _scaleTween?.Kill();
        AudioManager.instance.PlaySFX("Bubble");


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
            if (Vector3.Distance(Player.Current.transform.position, transform.position) < _playerAtractionDistance && FollowPlayer)
            {
                targetDirection = (Player.Current.transform.position - transform.position).normalized * 10;
            }
            _rb.AddForce(targetDirection * _acceleration, ForceMode2D.Force);

            _rb.velocity = new Vector2(
                Mathf.Clamp(_rb.velocity.x, -Speed, Speed),
                Mathf.Clamp(_rb.velocity.y, -Speed, Speed)
            );
        }
    }


    public virtual void BlockPointLogic(BubblePoint point)
    {

    }

    public void Dissapear()
    {
        _scaleTween = transform.DOScale(Vector3.zero, 0.5f)
                    .SetEase(Ease.InBack)
                    .OnComplete(() => Destroy(gameObject));
    }


    private IEnumerator DesapearProcess()
    {
        yield return new WaitForSeconds(ActiveTime);
        Dissapear();
    }

    public override string ToString()
    {
        return $"@Bubble {_type.ToString()} ID {IdInInventory}";
    }
}
