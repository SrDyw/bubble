using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreCorruption : MonoBehaviour
{
    private int counterCorruption = 0;
    public int rootAmount = 2;


    private void Update()
    {
        if (GameManager.Current.GameState != GameState.GameOver && counterCorruption >= rootAmount)
        {
            print(counterCorruption);
            GameManager.Current.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Root"))
        {
            counterCorruption++;
            // coll.gameObject.SetActive(false);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Root"))
        {
            counterCorruption--;
        }
    }
}
