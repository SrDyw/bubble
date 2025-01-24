using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _starPrefabs;
    [SerializeField] private float _minCameraDistance = 50;
    [SerializeField] private float _maxCameraDistance = 200;
    [SerializeField] private int amount = 30;

    private SpriteRenderer _renderer;
    private GameObject _starWContainer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _starWContainer = new GameObject("Star Container");

        _starWContainer.transform.SetParent(this.transform);
        Generate(amount);
    }

    public void Generate(int quantity)
    {
        var bounds = _renderer.bounds;

        for (int i = 0; i < quantity; i++)
        {
            var x = Random.Range(bounds.min.x, bounds.max.x);
            var y = Random.Range(bounds.min.y, bounds.max.y);
            var z = Random.Range(_minCameraDistance, _maxCameraDistance);

            var position = new Vector3(x, y, z);
            var s = Instantiate(_starPrefabs, position, Quaternion.identity);
            s.transform.parent = this.transform;
        }
    }
}
