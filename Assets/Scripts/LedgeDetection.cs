using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    public LayerMask layerMask;
    private RaycastHit ledgeInfo;
    private RaycastHit playerInfo;
    public bool ledge;
    public GameObject hitObject;
    private Rigidbody rb;
    private Collider col;
    private GrabLedgeDetection gld;
    //gizmos
    private Vector3 ledgeRayOrigin;
    private Vector3 ledgeRayEnd;
    private Vector3 playerRayOrigin;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody>();
        col = transform.parent.GetComponent<Collider>();
        gld = transform.parent.GetComponent<GrabLedgeDetection>();
        layerMask = LayerMask.GetMask("Default", "Ground");
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer == layerMask) other != transform.parent && 
        if (layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
            //gld.lastLedgeGrab = ledge;
            // The purpose of this function is to find the closest and highest point to the user
            // find the closest point
            Vector3 tmp = other.ClosestPoint(transform.position);
            // convert positions to 2D
            Vector2 tmpHoriz = new Vector2(tmp.x, tmp.z);
            Vector2 positionHoriz = new Vector2(transform.position.x, transform.position.z);
            // Get distances
            float horizontalDistance = Vector2.Distance(tmpHoriz, positionHoriz);
            float grabHeight = transform.position.y + col.bounds.extents.y * 2 - (Mathf.Pow(horizontalDistance, 2) * 0.4f);
            //float grabHeight = col.bounds.center.y + col.bounds.extents.y * 2;
            ledgeRayOrigin = new Vector3(tmp.x, grabHeight, tmp.z);
            ledgeRayEnd = ledgeRayOrigin - (transform.up * col.bounds.extents.y * 3);
            //Physics.Raycast(ledgeRayOrigin, -transform.up, out ledgeInfo, col.bounds.extents.y * 3);
            // Sphere cast to not miss closest point
            Physics.SphereCast(ledgeRayOrigin, 0.05f, -transform.up, out ledgeInfo, col.bounds.extents.y * 3);
            playerRayOrigin = new Vector3(transform.parent.transform.position.x, grabHeight, transform.parent.transform.position.z);
            Physics.Raycast(playerRayOrigin, -transform.up, out playerInfo, col.bounds.extents.y * 3);
            // if a ledge is detected and there is nothing above the players head
            if (ledgeInfo.collider != null && playerInfo.transform == transform.parent)
            {
                hitObject = ledgeInfo.collider.gameObject;
                gld.hitObject = ledgeInfo.collider.gameObject;
                gld.GrabPoint = ledgeInfo.point;
                // test for colisions at grab point
                Vector3 CapsuleTop = ledgeInfo.point + (transform.up * col.bounds.extents.y * 3); // add the horizontal offset to ledge
                Vector3 CapsuleBottom = ledgeInfo.point - (transform.up * col.bounds.extents.y * 3);
                Physics.CapsuleCast(CapsuleTop, CapsuleBottom, col.bounds.extents.x * 2, -transform.up, 0);
                ledge = true;
                gld.ledgeGrab = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (ledge && other == ledgeInfo.collider)
        {
            LedgeDrop();
        }
    }
    public void LedgeDrop()
    {
        hitObject = null;
        gld.hitObject = null;
        //gld.GrabPoint = null;
        ledge = false;
        gld.ledgeGrab = false;
    }
    public bool isLedge()
    {
        return ledge;
    }
    private void OnDrawGizmos()
    {
        // Ledge cast
        Gizmos.DrawSphere(ledgeRayOrigin, 0.1f);
        Gizmos.DrawLine(ledgeRayOrigin, ledgeRayEnd);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(ledgeRayEnd, 0.1f);
        // Point found
        Gizmos.color = Color.white;
        Gizmos.DrawLine(ledgeRayOrigin, ledgeInfo.point);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(ledgeInfo.point, 0.1f);
        // Player roof casts
        Gizmos.color = Color.grey;
        Gizmos.DrawSphere(playerRayOrigin, 0.1f);
        Gizmos.DrawLine(playerRayOrigin, playerInfo.point);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(playerInfo.point, 0.1f);
        Gizmos.color = Color.grey;
    }

}
