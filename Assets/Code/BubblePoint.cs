using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;
public class BubblePoint : WorldClickable
{
    [SerializeField] private BubbleType _bubbleRequired;
    [SerializeField] private Plant _plant;
    [SerializeField] private Transform _parent;
    [SerializeField] private UnityEvent _onBubbleSetted;

    [SerializeField] private float _showByDistance = -1;

    private Bubble _currentBubble;
    private Tween _scaleTween;
    private Vector3 _defaultLiftScale;

    private bool _isShowing;

    public Transform Parent { get => _parent; }
    public bool IsShowing { get => _isShowing; set => _isShowing = value; }

    private void Awake()
    {
        _plant = GetComponentInParent<Plant>();

        _defaultLiftScale = transform.localScale;
        transform.localScale = Vector3.zero;

    }

    public override void Update()
    {
        base.Update();
        if (_showByDistance >= 0)
        {
            if (Vector2.Distance(Player.Current.transform.position, transform.position) < _showByDistance)
            {
                Show();
                // if (!UIInventoryModule.Current.IsActive) UIInventoryModule.Current.Show();
            }
            else Hide();
        }
    }

    public override void OnDetected()
    {
        if (_currentBubble != null) return;

        var selectedItem = UIInventoryModule.Current.SelectedItem;
        if (selectedItem)
        {
            if (selectedItem is Bubble)
            {
                var bubble = (Bubble)selectedItem;
                if (_bubbleRequired == bubble.BubbleType)
                {
                    if (_plant)
                    {
                        _plant.SetBubble(selectedItem as Bubble, this);
                    }
                    else
                    {
                        SetBubble(selectedItem as Bubble);
                        Destroy(gameObject, 0.5f);
                    }
                    return;
                }
            }
        }
    }
    Timer timerToClose;
    public void Show(float closeAutomaticallyOn = 0.5f)
    {
        if (IsShowing) return;
        _scaleTween = transform.DOScale(_defaultLiftScale, 0.2f)
            .SetEase(Ease.OutBack);

        IsShowing = true;
    }

    public void Hide()
    {
        if (!IsShowing) return;

        _scaleTween = transform.DOScale(Vector3.zero, 0.2f)
            .SetEase(Ease.OutBack);

        IsShowing = false;
    }

    private void OnDestroy()
    {
        _scaleTween?.Kill();
    }

    public bool SetBubble(Bubble bubble)
    {
        if (_bubbleRequired != BubbleType.None)
        {
            _currentBubble = bubble;
            _currentBubble.IsUsed = true;
            Inventory.Current.UseItem(bubble);

            _currentBubble.gameObject.SetActive(true);
            _currentBubble.transform.position = transform.position;
            _currentBubble.transform.SetParent(transform.parent);
            _onBubbleSetted?.Invoke();
            Hide();
            return true;
        }

        Debug.Log($"The Selected bubble ({bubble}) is not accepted for current bubble point");
        return false;
    }
}
