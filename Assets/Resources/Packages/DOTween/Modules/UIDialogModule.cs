using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDialogModule : UIModule
{
    [SerializeField] private GameObject dialog;
    public override void Show()
    {
        base.Show();
        // Disable movement of player
    }

    public override void Hide()
    {
        base.Hide();
    }
}
