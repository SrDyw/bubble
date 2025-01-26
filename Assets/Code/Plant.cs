using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Plant : MonoBehaviour
{
    [SerializeField] private Transform _baseBone;
    [SerializeField] private List<BubblePoint> _liftPoints;
    [SerializeField] private List<BubblePoint> _blockPoints;
    [SerializeField] private Ease _floatEase;
    [SerializeField] private Ease _growEase;
    [SerializeField] private float _floatAnimationTime = 3f;
    [SerializeField] private float _initialGrowDelay = 0.2f;
    [SerializeField] private float _growAnimationTime = 0.5f;
    [SerializeField] private GameObject _bubbleScan;
    [SerializeField] private UnityEvent _onDone;
    [SerializeField] private bool _autoInitRoots = true;

    private Tween _floatTween;
    private Tween _scaleTween;
    private Vector2 _defaultScale;
    private GrowRoot _roots;

    private bool _isActive = false;

    public List<BubblePoint> BlockPoints { get => _blockPoints; set => _blockPoints = value; }

    private void Awake()
    {
        _defaultScale = _baseBone.localScale;
        _baseBone.localScale = Vector3.zero;
        _isActive = true;
    }

    private void Start()
    {
        _roots = GetComponentInChildren<GrowRoot>();
        _scaleTween = _baseBone.DOScale(_defaultScale, _growAnimationTime)
            .SetEase(_growEase)
            .SetDelay(_initialGrowDelay)
            .OnComplete(() =>
            {
                if (_autoInitRoots) _roots.StartGenerateRoots();
            });

    }
    public void StartsRoots()
    {
        _roots.StartGenerateRoots();
    }

    private void Update()
    {
        if (_isActive && _liftPoints.Count == 0 && BlockPoints.Count == 0)
        {
            Float();
            _isActive = false;
        }

        // Debug.DrawRay(transform.position, transform.up * 30);
    }


    public void SetBubble(Bubble bubble, BubblePoint point)
    {
        bool TrySetBubbleToSetPoint(List<BubblePoint> points, bool automaticallyRemovePoint = true)
        {
            var index = points.IndexOf(point);
            if (index != -1 && points[index].SetBubble(bubble))
            {
                if (automaticallyRemovePoint == true)
                {
                    points.Remove(point);
                }
                return true;
            }
            return false;
        }

        if (TrySetBubbleToSetPoint(BlockPoints, false))
        {
            bubble.BlockPointLogic(point);
        } // Try release block points
        else TrySetBubbleToSetPoint(_liftPoints);



    }

    public void Float()
    {
        // _evilRoot.StopRoots();
        _bubbleScan.gameObject.SetActive(false);
        _floatTween = transform.DOMove(transform.position + transform.up * 30, _floatAnimationTime)
            .SetEase(_floatEase)
            .OnComplete(() => gameObject.SetActive(false));

        _roots.StopRoots();
        GameManager.Current.OnReleasePlant?.Invoke(this);
        _onDone?.Invoke();
    }

    public void ShowBubblePoint()
    {
        foreach (var point in _liftPoints)
        {
            point.Show();
        }

        foreach (var point in BlockPoints)
        {
            point.Show();
        }
    }

    void ClearTweens(List<Tween> tweens)
    {
        foreach (var t in tweens) t?.Kill();
    }

    public void HideBubblePoints()
    {
        foreach (var point in _liftPoints)
        {
            point.Hide();
        }
        foreach (var point in BlockPoints)
        {
            point.Hide();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // UIInventoryModule.Current.Show();
            ShowBubblePoint();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIInventoryModule.Current.Hide();
            HideBubblePoints();
        }
    }
}
