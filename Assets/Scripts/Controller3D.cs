using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Controller3D : MonoBehaviour
{
    const float SKIN_WIDTH = 0.02f;

    [SerializeField]
    LayerMask collisionMask;
    new CapsuleCollider collider;

    public bool IsGrounded { get; private set; }
    public bool IsCeiled { get; private set; }

    void Awake()
    {
        collider = GetComponent<CapsuleCollider>();
    }

    public void Move(Vector3 velocity)
    {
        IsGrounded = false;
        IsCeiled = false;

        // Calculate collisions
        //if (velocity.y != 0)
        //    VerticalCasting(ref velocity);
        //if (velocity.x != 0 || velocity.z != 0)
        //    RadialCasting(ref velocity);
        if (velocity != Vector3.zero)
            DetectCollisions(ref velocity);

        // Handle collisions
        transform.Translate(velocity, Space.World);
    }

    void DetectCollisions(ref Vector3 velocity)
    {
        Vector3 point1 = transform.position + collider.center + Vector3.down * (collider.height / 2 - collider.radius);
        Vector3 point2 = transform.position + collider.center + Vector3.up * (collider.height / 2 - collider.radius);

        Vector3 dir = velocity.normalized;
        float dist = velocity.magnitude + SKIN_WIDTH;

        RaycastHit hit;
        if (Physics.CapsuleCast(point1, point2, collider.radius - SKIN_WIDTH, dir, out hit, dist, collisionMask))
        {
            float adjustedSkinWidth = SKIN_WIDTH / Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(-hit.normal, dir));
            Vector3 displacement = dir * (hit.distance - adjustedSkinWidth);
            velocity = displacement;

            IsGrounded = Mathf.Sign(dir.y) == -1;
            IsCeiled = !IsGrounded;

            // Restrict hit normal to span of Vector3.up & direction
            Vector3 planeNormal = Vector3.Cross(dir, Vector3.up);
            float hitPlaneNormalAngle = Vector3.Angle(planeNormal, hit.normal);
            if (hitPlaneNormalAngle > 90)
            {
                planeNormal *= -1; // Makes sure planeNormal is pointing toward hitNormal end point
                hitPlaneNormalAngle = Vector3.Angle(planeNormal, hit.normal);
            }
            float distToPointInPlane = 1 /*hit.normal.magnitude*/ * Mathf.Sin((90 - hitPlaneNormalAngle) * Mathf.Deg2Rad);
            Vector3 restrictedHitNormal = hit.normal - planeNormal * distToPointInPlane;

            // TODO: use slope angle to move along slopes of max steepness
            float slopeAngle = Vector3.Angle(Vector3.up, hit.normal - planeNormal * distToPointInPlane);
            Debug.Log(slopeAngle);
        }
    }

    /*
    void VerticalCasting(ref Vector3 velocity)
    {
        Vector3 point1 = transform.position + collider.center + Vector3.down * (collider.height / 2 - collider.radius);
        Vector3 point2 = transform.position + collider.center + Vector3.up * (collider.height / 2 - collider.radius);
        
        Vector3 direction = Vector3.up * Mathf.Sign(velocity.y);
        float distance = Mathf.Abs(velocity.y) + SKIN_WIDTH;

        RaycastHit hit;
        if (Physics.CapsuleCast(point1, point2, collider.radius - SKIN_WIDTH, direction, out hit, distance, collisionMask))
        {
            float adjustedSkinWidth = SKIN_WIDTH / Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(-hit.normal, direction));
            Vector3 displacement = direction * (hit.distance - adjustedSkinWidth);
            velocity.y = displacement.y;
            // TODO: add collisionListener

            IsGrounded = direction == Vector3.down;
            IsCeilinged = direction == Vector3.up;
        }
    }

    void RadialCasting(ref Vector3 velocity)
    {
        Vector3 point1 = transform.position + collider.center + Vector3.down * (collider.height / 2 - collider.radius);
        Vector3 point2 = transform.position + collider.center + Vector3.up * (collider.height / 2 - collider.radius);

        Vector3 direction = new Vector3(velocity.x, 0, velocity.z);
        float distance = direction.magnitude + SKIN_WIDTH;
        direction.Normalize();

        RaycastHit hit;
        if (Physics.CapsuleCast(point1, point2, collider.radius - SKIN_WIDTH, direction, out hit, distance, collisionMask))
        {
            float adjustedSkinWidth = SKIN_WIDTH / Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(-hit.normal, direction));
            Vector3 displacement = direction * (hit.distance - adjustedSkinWidth);
            velocity.x = displacement.x;
            velocity.z = displacement.z;
            // TODO: add collisionListener

            // Maybe not TODO: print distance between displaced position and wall
            //Vector3 fromPoint = transform.position + collider.center + collider.radius * direction + displacement;
        }
    }
    */

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
