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
    private Vector2 _defaultInventoryPosition;

    private RectTransform _cursor;
    private int _selectedSlot = -1;
    private Tween _cursorMoveTween;
    private Tween _mainModuleTween;

    public int SelectedSlot => _selectedSlot;

    public Item SelectedItem { get => SelectedSlot != -1 ? _slots[SelectedSlot]?.CurrentItem : null; }

    public static UIInventoryModule Current;



    private void Awake()
    {
        for (int i = 0; i < _slotAmount; i++)
        {
            var s = Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity);
            s.transform.SetParent(_slotsWrapper.transform, false);
            var slot = s.GetComponent<UIItemSlot>();
            slot.UiInventoryModule = this;
            slot.SlotIndex = i;

            _slots.Add(slot);
        }

        Current = this;
    }
    private void Start()
    {
        Inventory.Current.OnItemAdd += AssignItemToSlot;
        GameManager.Current.OnGameOver += OnGameOver;
        Inventory.Current.OnItemUse += OnItemUse;

        var c = Instantiate(_cursorPrefab, Vector3.zero, Quaternion.identity);
        _cursor = c.GetComponent<RectTransform>();
        _cursor.transform.SetParent(Wrapper, false);
        _cursor.transform.localScale *= 1.3f;

        _defaultInventoryPosition = Wrapper.localPosition;
        Wrapper.localPosition += Vector3.left * 100;

        Wrapper.gameObject.SetActive(false);

    }

    private void Update()
    {
        var activeBubblePoint = FindObjectsOfType<BubblePoint>().Where(x => x.IsShowing).ToList(); 
        if (activeBubblePoint.Count != 0) Show();
        else Hide();

        // _slots.Show();

        // if (IsActive)
        // {
        //     if (Input.anyKeyDown)
        //     {
        //         var direction = Input.GetAxisRaw("Horizontal");
        //         if (direction != 0)
        //         {
        //             SelectSlotAndUpdateCursor((int)Mathf.Clamp(SelectedSlot + direction, 0, _slots.Count - 1));
        //         }
        //         if (Input.GetKeyDown(KeyCode.E))
        //         {
        //             if (_slots[_selectedSlot].CurrentItem)
        //             {
        //                 Inventory.Current.UseItem(_slots[_selectedSlot].CurrentItem.Scheme.Name);
        //                 UpdateModule();
        //             }
        //         }
        //     }
        // }
    }

    private void OnDestroy()
    {
        Inventory.Current.OnItemAdd -= AssignItemToSlot;
        GameManager.Current.OnGameOver -= OnGameOver;
        Inventory.Current.OnItemUse -= OnItemUse;

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

    float animationTime = 0.25f;
    
    public override void Show()
    {
        if (IsActive) return;
        Wrapper.gameObject.SetActive(true);
        IsActive = true;

        _mainModuleTween = Wrapper.DOLocalMoveX(_defaultInventoryPosition.x, animationTime)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                SelectSlotAndUpdateCursor(SelectedSlot != -1 ? SelectedSlot : 0);
            });
    }

    public override void Hide()
    {
        if (!IsActive) return;

        IsActive = false;
        _mainModuleTween = Wrapper.DOLocalMoveX(_defaultInventoryPosition.x - 100, animationTime)
            .SetEase(Ease.OutBack)
            .OnComplete(() => Wrapper.gameObject.SetActive(false));

        _cursor.gameObject.SetActive(false);
    }

    void OnItemUse(Item item)
    {
        var lastCommonItem = Inventory.Current.Items.FirstOrDefault(x => x.Scheme.Name == item.Scheme.Name);

        if (lastCommonItem)
        {
            _slots[SelectedSlot].CurrentItem = lastCommonItem;
        }
        else
        {
            _slots[SelectedSlot].CurrentItem = null;
        }
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
        _cursor.gameObject.SetActive(true);

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
