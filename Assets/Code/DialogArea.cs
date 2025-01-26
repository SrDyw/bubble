using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogArea : DialogueInteractor
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowDialog();
            gameObject.SetActive(false);
        }
    }
}
