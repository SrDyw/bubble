using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject _model;
    private Animator _animator;
    private Movement _movement;


    private readonly int RANDOM_PARAMETER = Animator.StringToHash("random");
    private readonly int MOVE_PARAMETER = Animator.StringToHash("moving");
    private readonly int GROUND_PARAMETER = Animator.StringToHash("grounded");
    private WaitForSeconds _randomUpdaterWaiter;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _randomUpdaterWaiter = new WaitForSeconds(0.5f);
        _movement = GetComponentInParent<Movement>();

        StartCoroutine(UpdateRandomValue());
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool(MOVE_PARAMETER, _movement.enabled == true && _movement.IsMoving);
        _animator.SetBool(GROUND_PARAMETER, _movement.IsGrounded);

        _model.transform.localScale = new Vector3(Player.Current.Direction, 1, 1);
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
