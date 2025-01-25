using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionScan : MonoBehaviour
{
    public Rigidbody2D bubbleLevel;
    // Start is called before the first frame update
    public Transform target;
    public float speed = .2f;
    public Vector2 minMaxDelay;
    void Start()
    {
        StartCoroutine("shootBubbleLevel");
    }

    IEnumerator shootBubbleLevel() {
        yield return new WaitForSeconds(Random.Range(minMaxDelay.x, minMaxDelay.y));
        bubbleLevel.position = transform.position;
        bubbleLevel.velocity = (target.position - transform.position).normalized * speed;
    }
}
