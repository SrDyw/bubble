using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
public class BubblePoint : WorldClickable
{
    [SerializeField] private BubbleType _bubbleRequired;
    [SerializeField] private Plant _plant;

    private Bubble _currentBubble;

    private void Awake() {
        _plant = GetComponentInParent<Plant>();
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
                    _plant.SetBubble(selectedItem as Bubble, this);
                    return;
                }
            }
        }
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
            _currentBubble.transform.SetParent(transform);
            return true;
        }

        Debug.Log($"The Selected bubble ({bubble}) is not accepted for current bubble point");
        return false;
    }
}
