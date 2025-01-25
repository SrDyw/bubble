using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    private Tween _floatTween;
    private Tween _scaleTween;
    private Vector2 _defaultScale;
    private GrowRoot _roots;

    private void Awake()
    {
        _defaultScale = _baseBone.localScale;
        _baseBone.localScale = Vector3.zero;
    }

    private void Start()
    {
        _roots = GetComponentInChildren<GrowRoot>();
        _scaleTween = _baseBone.DOScale(_defaultScale, _growAnimationTime)
            .SetEase(_growEase)
            .SetDelay(_initialGrowDelay)
            .OnComplete(() => _roots.StartGenerateRoots());

    }


    public void SetBubble(Bubble bubble, BubblePoint point)
    {
        bool TrySetBubbleToSetPoint(List<BubblePoint> points)
        {
            var index = points.IndexOf(point);
            if (index != -1 && points[index].SetBubble(bubble))
            {
                points.Remove(point);
                return true;
            }
            return false;
        }

        if (TrySetBubbleToSetPoint(_blockPoints)) { } // Try release block points
        else TrySetBubbleToSetPoint(_liftPoints);

        if (_liftPoints.Count == 0 && _blockPoints.Count == 0)
        {
            Float();
        }

    }

    public void Float()
    {
        // _evilRoot.StopRoots();

        _floatTween = transform.DOMove(transform.up * 30, _floatAnimationTime)
            .SetEase(_floatEase)
            .OnComplete(() => gameObject.SetActive(false));

        _roots.StopRoots();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIInventoryModule.Current.Show();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIInventoryModule.Current.Hide();
        }
    }
}
