using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Plant : MonoBehaviour
{
    [SerializeField] private EvilRoot _evilRoot;
    [SerializeField] private List<BubblePoint> _liftPoints;
    [SerializeField] private List<BubblePoint> _blockPoints;
    [SerializeField] private Ease _floatEase;
    [SerializeField] private float _floatAnimationTime = 3f;

    private Tween _floatTween;


    public void SetBubble(Bubble bubble)
    {
        bool TrySetBubbleToSetPoint(List<BubblePoint> points)
        {
            for (int i = points.Count - 1; i >= 0; i--)
            {
                var point = points[i];
                if (point.SetBubble(bubble))
                {
                    points.Remove(point);
                    return true;
                }
            }
            return false;
        }

        if (TrySetBubbleToSetPoint(_blockPoints)) {} // Try release block points
        else TrySetBubbleToSetPoint(_liftPoints); 

        if (_liftPoints.Count == 0 && _blockPoints.Count == 0)
        {
            Float();
        }

    }

    public void Float()
    {
        _evilRoot.StopRoots();

        _floatTween = transform.DOMove(transform.up * 20, _floatAnimationTime)
            .SetEase(_floatEase)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
