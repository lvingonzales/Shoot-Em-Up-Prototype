using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;

    private Vector2 gizmoPosition;

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmoPosition = Mathf.Pow(1 - t, 3) * points[0].position + 3 * Mathf.Pow(1 - t, 2) * t * points[1].position
                + 3 * (1 - t) * Mathf.Pow(t, 2) * points[2].position + Mathf.Pow(t, 3) * points[3].position;

            Gizmos.DrawSphere(gizmoPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector2(points[0].position.x, points[0].position.y), new Vector2(points[1].position.x,
            points[1].position.y));

        Gizmos.DrawLine(new Vector2(points[2].position.x, points[2].position.y), new Vector2(points[3].position.x,
            points[3].position.y));
    }
}
