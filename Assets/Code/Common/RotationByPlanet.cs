using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationByPlanet : MonoBehaviour
{
    [SerializeField] private GameObject _planet;
    [SerializeField] private bool _invert;
    [SerializeField] private Axis _axisToBlock;
    // Start is called before the first frame update
    void Start()
    {
        var defaultRotation = transform.rotation;
        var targetPosition = _planet.transform.position;

        targetPosition.z = transform.position.z;

        var direction = (targetPosition - transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(transform.up, -direction * (_invert ? -1 : 1)) * transform.rotation;

        // transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
    }
}
