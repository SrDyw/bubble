using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootPiece : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent != transform.parent)
        {
            GameManager.Current.GameOver();
        }
    }
}
