using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;


    private readonly int RANDOM_PARAMETER = Animator.StringToHash("random");
    private WaitForSeconds _randomUpdaterWaiter;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _randomUpdaterWaiter = new WaitForSeconds(0.5f);

        StartCoroutine(UpdateRandomValue());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator UpdateRandomValue()
    {
        while (true)
        {
            yield return _randomUpdaterWaiter;
            _animator.SetFloat(RANDOM_PARAMETER, Random.Range(0, 1f));
        }
    }
}
