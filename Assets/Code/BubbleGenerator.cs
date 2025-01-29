using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _bubblePref;
    [SerializeField] private float amount;

    [SerializeField] private float _radius = 30;
    [SerializeField] private float angle = 0;
    [SerializeField] private float speed = 3f;

    private void Start()
    {
        Generate();
    }


    public void Generate()
    {
        for (int i = 0; i < 360f / amount; i++)
        {
            var x = Mathf.Cos(i) * _radius;
            var y = Mathf.Sin(i) * _radius;
            var v = new Vector3(x, y) * _radius;

            var gameObject = Instantiate(_bubblePref, transform.position + v, Quaternion.identity);
            var bubble = gameObject.GetComponent<Bubble>();

            bubble.Direction = v * Random.Range(speed, speed * Random.Range(0.85f, 1f));
            bubble.FollowPlayer = false;
            bubble.ActiveTime = Random.Range(1, 2.5f);
        }
        Invoke(nameof(Generate), Random.Range(5, 20f));
    }
}
