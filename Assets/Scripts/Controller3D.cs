using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Controller3D : MonoBehaviour
{
    const float SKIN_WIDTH = 0.01f;

    new CapsuleCollider collider;

    void Awake()
    {
        collider = GetComponent<CapsuleCollider>();
    }

    public void Move(Vector3 velocity)
    {
        // Calculate collisions
        if (velocity.y != 0)
            VerticalCasting(ref velocity);
        if (velocity.x != 0 || velocity.z != 0)
            RadialCasting(ref velocity);


    }

    void VerticalCasting(ref Vector3 velocity)
    {

    }

    void RadialCasting(ref Vector3 velocity)
    {
        
    }

    /*
    Beginning of cylinder casting

    [SerializeField] [Range(3, 32)]
    uint radialSegments = 3;
    [SerializeField] [Range(1, 32)]
    uint verticalSegments = 1;
    [SerializeField]
    float radius = 0.5f, height = 2;

    void OnDrawGizmos()
    {
        float anglePerSegment = 360 / radialSegments;
        float heightPerSegment = height / verticalSegments;

        Gizmos.color = Color.blue;
        for (uint idx = 0; idx < radialSegments; idx++)
        {
            uint nextIdx = idx + 1;
            if (nextIdx == radialSegments)
                nextIdx = 0;

            float angle = idx * anglePerSegment;
            float nextAngle = nextIdx * anglePerSegment;

            Vector3 point = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad), 
                0, 
                Mathf.Sin(angle * Mathf.Deg2Rad)
            ) * radius;

            Vector3 nextPoint = new Vector3(
                Mathf.Cos(nextAngle * Mathf.Deg2Rad), 
                0, 
                Mathf.Sin(nextAngle * Mathf.Deg2Rad)
            ) * radius;

            for (uint heightIdx = 0; heightIdx <= verticalSegments; heightIdx++)
            {
                Gizmos.DrawLine(point + Vector3.up * heightPerSegment * heightIdx, nextPoint + Vector3.up * heightPerSegment * heightIdx);

                if (heightIdx == 0 || heightIdx == verticalSegments)
                {
                    Gizmos.DrawLine(point + Vector3.up * heightPerSegment * heightIdx, Vector3.up * heightPerSegment * heightIdx);
                }
                
                if (heightIdx != verticalSegments)
                {
                    Gizmos.DrawLine(point + Vector3.up * heightPerSegment * heightIdx, point + Vector3.up * heightPerSegment * (heightIdx + 1));
                }
            }
        }
    }
    */
}
