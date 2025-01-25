using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDialogModule : UIModule
{
    [SerializeField] private GameObject dialog;
    [SerializeField] private float maxDistance;
    [SerializeField] private Transform characterPos;
    public override void Show()
    {
        var canTalk = Vector3.Distance(characterPos.position, Player.Current.transform.position) < maxDistance;
        if (canTalk)
            
            base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
