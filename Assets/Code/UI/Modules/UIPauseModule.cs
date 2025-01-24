using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseModule : UIModule
{
    private void Start()
    {
        GameManager.Current.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.Current.OnGameStateChanged -= OnGameStateChanged;
    }

    public void OnGameStateChanged(bool value)
    {
        if (value == false)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
