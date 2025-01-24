using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOverModule : UIModule
{
    private void Start()
    {
        GameManager.Current.OnGameOver += Show;
    }

    private void OnDestroy()
    {
        GameManager.Current.OnGameOver -= Show;
    }

    public override void Show()
    {
        base.Show();
        GameManager.Current.Freeze();
    }

    public void OnRestartButton()
    {
        GameManager.Current.Unfreeze();
        GameManager.Current.RestartGame();
    }
}
