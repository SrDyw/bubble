using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Quest : MonoBehaviour
{
    [SerializeField] private int questAmount;
    [SerializeField] private UnityEvent _onAllQuestSolved;
    public int questsSolved = 0;


    public void TaskClear()
    {
        questsSolved++;
        if (questsSolved >= questAmount)
        {
            _onAllQuestSolved?.Invoke();
            GameManager.Current.OnQuestComplete?.Invoke();
            enabled = false;
        }
    }
}
