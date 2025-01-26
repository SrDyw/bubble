using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulkan : MonoBehaviour
{
    [SerializeField] private float _initialDelay = 10f;
    [SerializeField] private float _minTimeToGenerate = 5;
    [SerializeField] private float _maxTimeToGenerate = 10;
    [SerializeField] private float _shootDelay = 0.2f;

    [SerializeField] private GameObject _bubblePrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Animator _animator;

    private Coroutine _bubbleGenerator;

    public virtual void Start()
    {
        _bubbleGenerator = StartCoroutine(BubbleGEenerator());
    }

    private void OnDisable()
    {
        if (_bubbleGenerator != null) StopCoroutine(_bubbleGenerator);
    }

    private IEnumerator BubbleGEenerator()
    {
        yield return new WaitForSeconds(_initialDelay);

        while (true)
        {
            _animator.SetTrigger("Blop");
            Pawn();
            yield return new WaitForSeconds(_shootDelay);
            var b = Instantiate(_bubblePrefab, _spawnPoint.position, Quaternion.identity);
            var bubble = b.GetComponent<Bubble>();

            bubble.Direction = transform.up;
            yield return new WaitForSeconds(Random.Range(_minTimeToGenerate, _maxTimeToGenerate));
        }
    }

    public virtual void Pawn(){}
}
