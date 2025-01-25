using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreCorruption : MonoBehaviour
{
    private int counterCorruption = 0;
    public int rootAmount = 2;
    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Root")) {
            counterCorruption++;
            Destroy(coll.gameObject);
        }
        if (counterCorruption >= rootAmount) {
            GameManager.Current.GameOver();
        }
    }
}
