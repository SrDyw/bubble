using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class WorldHover : WorldClickable
{
    [SerializeField] private UnityEvent _onHover;
    [SerializeField] private UnityEvent _onUnHover;


    [SerializeField] private bool _hover;
    [SerializeField] private float _sizeOnUnhover = 0.75f;
    [SerializeField] private float _minDistanceToPlayer = 2;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Ease _hoverEase = Ease.InOutBack;
    [SerializeField] private Color _colorOnHover, _baseColor;

    private Vector2 _defaultScale;
    private Timer _tick;
    private Tween _hoverTween;

    private bool PlayerInRange => Vector3.Distance(Player.Current.transform.position, transform.position) < _minDistanceToPlayer;

    private void Awake()
    {
        _defaultScale = transform.localScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale *= _sizeOnUnhover;
        _renderer.color = _baseColor;
        _tick = Timer.CreateNewTimer(Random.Range(0.1f, 0.2f), gameObject, () =>
        {
            if (PlayerInRange && TryDetect())
            {
                if (_hover == false) OnHover();
                _hover = true;
            }
            else
            {
                if (_hover == true) OnUnhover();
                _hover = false;
            }
            _tick.RestartTimer();

        });
    }

    public override void OnDetected()
    {
        base.OnDetected();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (_hover && Input.GetMouseButtonDown(0))
        {
            OnClick1?.Invoke();
        }
    }

    void OnHover()
    {
        _renderer.color = _colorOnHover;
        _onHover?.Invoke();

        _hoverTween?.Kill();
        _hoverTween = transform.DOScale(_defaultScale, 0.3f)
            .SetEase(_hoverEase);
    }

    void OnUnhover()
    {
        _renderer.color = _baseColor;
        _onUnHover?.Invoke();


        _hoverTween?.Kill();
        _hoverTween = transform.DOScale(_defaultScale * _sizeOnUnhover, 0.3f)
            .SetEase(_hoverEase);
    }
}
