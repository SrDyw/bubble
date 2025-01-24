using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BubblePoint : MonoBehaviour
{
    [SerializeField] private BubbleType[] _bubblesRequired;

    private Bubble _currentBubble;


    public bool SetBubble(Bubble bubble)
    {
        if (_bubblesRequired.FirstOrDefault(b => b == bubble.BubbleType) != BubbleType.None)
        {
            _currentBubble = bubble;
            return true;
        }

        Debug.Log($"The Selected bubble ({bubble}) is not accepted for current bubble point");
        return false;
    }
}
