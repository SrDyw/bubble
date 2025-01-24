using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DywFunctions;
using DG.Tweening;
using System.Linq;
public class UIInventoryModule : UIModule
{
    [SerializeField] private int _slotAmount = 4;
    [SerializeField] private GameObject _slotsWrapper;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _cursorPrefab;
    [SerializeField] private Ease _cursorMoveEase = Ease.OutBack;
    [SerializeField] private float _cursorMoveTime = 0.4f;

    private List<UIItemSlot> _slots = new();

    private RectTransform _cursor;
    private int _selectedSlot = 0;
    private Tween _cursorMoveTween;

    public int SelectedSlot => _selectedSlot;

    private void Awake()
    {
        for (int i = 0; i < _slotAmount; i++)
        {
            var s = Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity);
            s.transform.SetParent(_slotsWrapper.transform, false);

            _slots.Add(s.GetComponent<UIItemSlot>());
        }
    }
    private void Start()
    {
        Inventory.Current.OnItemAdd += AssignItemToSlot;
        GameManager.Current.OnGameOver += OnGameOver;

        var c = Instantiate(_cursorPrefab, Vector3.zero, Quaternion.identity);
        _cursor = c.GetComponent<RectTransform>();
        _cursor.transform.SetParent(Wrapper, false);
        _cursor.transform.localScale *= 1.3f;
    }

    private void Update()
    {
        // _slots.Show();

        if (IsActive)
        {
            if (Input.anyKeyDown)
            {
                var direction = Input.GetAxisRaw("Horizontal");
                if (direction != 0)
                {
                    SelectSlotAndUpdateCursor((int)Mathf.Clamp(SelectedSlot + direction, 0, _slots.Count - 1));
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (_slots[_selectedSlot].CurrentItem)
                    {
                        Inventory.Current.UseItem(_slots[_selectedSlot].CurrentItem.Scheme.Name);
                        UpdateModule();
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        Inventory.Current.OnItemAdd -= AssignItemToSlot;
        GameManager.Current.OnGameOver -= OnGameOver;

        _cursorMoveTween?.Kill();
    }

    public void AssignItemToSlot(Item item)
    {
        var commonSlot = _slots.FirstOrDefault(slot => slot.CurrentItem?.Scheme.Name == item.Scheme.Name);
        var targetSlot = commonSlot ?? _slots.FirstOrDefault(slot => slot.CurrentItem == null);

        targetSlot?.Assign(item);
        UpdateModule();
    }

    public void OnGameOver()
    {
        Hide();
    }


    public override void Show()
    {
        base.Show();
        Player.Current.SetInputState(false);
        StartCoroutine(DelayedUpdateCursor());
    }
    private IEnumerator DelayedUpdateCursor()
    {
        yield return null;
        SelectSlotAndUpdateCursor(SelectedSlot);
    }


    public override void Hide()
    {
        base.Hide();
        Player.Current.SetInputState(true);
    }

    public void UpdateModule()
    {
        foreach (var _slot in _slots)
        {
            _slot.UpdateSlot();
        }
    }

    public void SelectSlotAndUpdateCursor(int index)
    {
        _selectedSlot = index;
        UpdateCursor();
    }


    public void UpdateCursor()
    {
        _cursorMoveTween?.Kill();
        _cursorMoveTween = _cursor.transform.DOMove(_slots[SelectedSlot].transform.position, _cursorMoveTime)
            .SetEase(_cursorMoveEase);
    }
}
