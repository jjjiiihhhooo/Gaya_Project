using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private Vector3 frontBoxSize;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool drawGizmo;

    private void OnDrawGizmos()
    {
        if (!drawGizmo) return;

        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);

        //Gizmos.color = Color.red;
        //Gizmos.DrawCube(transform.position + transform.forward * maxDistance, frontBoxSize);
    }

    public bool IsGrounded()
    {
        return Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, groundLayer);
    }

    //public bool IsWall()
    //{
    //    return Physics.BoxCast(transform.position, frontBoxSize, transform.forward, transform.rotation, maxDistance, groundLayer);
    //}
}
