using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public LayerMask layerMask;
    private RaycastHit info;
    public bool grounded;
    public GameObject hitObject;
    private Rigidbody rb;
    private Collider col;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        layerMask = LayerMask.GetMask("Default", "Ground");
    }
    public void FixedUpdate()
    {
        grounded = isGrounded();
    }
    public bool isGrounded()
    {
        float radius = 0;
        if (col is SphereCollider)
        {
            SphereCollider sphereCol = (SphereCollider)col;
            radius = sphereCol.radius;
        }
        else if (col is CapsuleCollider)
        {
            CapsuleCollider capCol = (CapsuleCollider)col;
            radius = capCol.radius;
        }
        else if (col is BoxCollider)
        {
            BoxCollider boxCol = (BoxCollider)col;
            radius = boxCol.size.magnitude;
        }
        Physics.SphereCast(rb.position, radius, -rb.transform.up, out info, col.bounds.center.y - col.bounds.min.y, layerMask);
        if (info.collider != null)
        {
            hitObject = info.collider.gameObject;
            return true;
        }
        else
        {
            hitObject = null;
            return false;
        }
    }
}
