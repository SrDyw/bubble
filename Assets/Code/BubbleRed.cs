using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleRed : Bubble
{
    [SerializeField] private float timeToExplose = 2;
    [SerializeField] private BubblePoint _targetPoint;

    public override void BlockPointLogic(BubblePoint point)
    {
        StartExplosion();
        _targetPoint = point;
    }
    public void StartExplosion()
    {
        StartCoroutine(ExplosionProcess());
    }

    private IEnumerator ExplosionProcess()
    {
        yield return new WaitForSeconds(timeToExplose);
        var g = Resources.Load<GameObject>("ExplosionEffect");
        var explosion = Instantiate(g, transform.position, Quaternion.identity);

        if (_targetPoint.Parent)
        {
            _targetPoint.Parent.gameObject.SetActive(false);
        }
        else
        {
            _targetPoint.gameObject.SetActive(false);
        }

        _targetPoint.GetComponentInParent<Plant>()?.BlockPoints.Remove(_targetPoint);
        gameObject.SetActive(false);
    }
}
