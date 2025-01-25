using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform point0;
    public Transform point1;
    public Transform point2;
    public Transform point3;

    public int segmentCount = 20;
    private List<Vector3> bezierPoints;

    void Awake()
    {
        bezierPoints = new List<Vector3>();
        CalculateBezierPoints();
    }

    void OnDrawGizmos()
    {
        if (point0 == null || point1 == null || point2 == null || point3 == null)
            return;

        var bezierPointsLocal = new List<Vector3>();
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector3 point = CalculateBezierPoint(t, point0.position, point1.position, point2.position, point3.position);
            bezierPointsLocal.Add(point);
        }

        for (int i = 0; i < bezierPointsLocal.Count - 1; i++)
        {
            Gizmos.DrawLine(bezierPointsLocal[i], bezierPointsLocal[i + 1]);
        }
    }

    void CalculateBezierPoints()
    {
        bezierPoints.Clear();
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector3 point = CalculateBezierPoint(t, point0.position, point1.position, point2.position, point3.position);
            bezierPoints.Add(point);
        }
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * p0;
        point += 3 * uu * t * p1;
        point += 3 * u * tt * p2;
        point += ttt * p3;

        return point;
    }

    public List<Vector3> GetBezierPoints()
    {
        return bezierPoints;
    }
}
