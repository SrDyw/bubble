using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreCorruption : MonoBehaviour
{
    private int counterCorruption = 0;
    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Root")) {
            counterCorruption++;
            Destroy(coll.gameObject);
        }
        if (counterCorruption > 1) {
            GameManager.Current.GameOver();
        }
    }
}
