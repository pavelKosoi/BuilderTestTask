using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleAndRadiusHandler : MonoBehaviour
{
    [SerializeField] float attacklAngle;
    [SerializeField] float attackRadius;
    [SerializeField] int arcSegments = 20;
    public float AttackAngle => attacklAngle;
    public float AttackRadius => attackRadius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3[] endPoints = CalcEndPoints(attacklAngle, attackRadius);      
        Gizmos.DrawLine(transform.position, endPoints[0]);
        Gizmos.DrawLine(transform.position, endPoints[1]);

        List<Vector3> arcPoints = GetArcPoints(attacklAngle, attackRadius, endPoints[0], endPoints[1]);
        for (int i = 0; i < arcPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(arcPoints[i], arcPoints[i + 1]);
        }
    }

    Vector3[] CalcEndPoints(float angle, float radius)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, transform.up);
        Quaternion negativeRotation = Quaternion.AngleAxis(-angle, transform.up);

        Vector3 direction = rotation * transform.forward;
        Vector3 negativeDirection = negativeRotation * transform.forward;       

        Vector3 endPoint1 = transform.position + direction * radius;
        Vector3 endPoint2 = transform.position + negativeDirection * radius;

        return new Vector3[] {endPoint1, endPoint2};
    }

    List<Vector3> GetArcPoints(float angle, float radius, Vector3 endPoint0, Vector3 endPoint1)
    {
        List<Vector3> arcPoints = new List<Vector3>();
        Vector3 dir = endPoint0 - endPoint1;
        Vector3 center = transform.position + transform.forward * radius;
        center -= Vector3.Cross(dir, transform.up).normalized * radius;

        for (int i = 0; i <= arcSegments; i++)
        {
            float t = i / (float)arcSegments;
            Vector3 point = Vector3.Slerp(endPoint1 - center, endPoint0 - center, t);
            point += center;
            arcPoints.Add(point);
        }
        return arcPoints;
    }
}
