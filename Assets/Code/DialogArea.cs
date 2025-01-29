using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogArea : DialogueInteractor
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Movement>().IsGrounded)
            {
                StartDialogue();
            }

        }
    }

    void StartDialogue()
    {
        ShowDialog();
        gameObject.SetActive(false);
    }
}
