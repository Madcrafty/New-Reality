using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabLedgeDetection : MonoBehaviour
{
    public LayerMask layerMask;
    public Vector3 GrabPoint;
    public bool ledgeGrab;
    public GameObject hitObject;
    private Rigidbody rb;
    private Collider col;
    private LedgeDetection ld;
    private bool change;
    public bool lastLedgeGrab;
    //gizmos
    private Vector3 rayOrigin;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        layerMask = LayerMask.GetMask("Default", "Ground");
        GameObject triggerbox = new GameObject();
        triggerbox.name = "Ledge Trigger";
        //BoxCollider newbox = new BoxCollider();
        //newbox.size.Set(1, 1, 1);
        //newbox.center.Set(0, 1, 1);
        ld = triggerbox.AddComponent<LedgeDetection>();
        BoxCollider tmp = triggerbox.AddComponent<BoxCollider>();
        tmp.isTrigger = true;
        tmp.size = new Vector3(col.bounds.extents.x, col.bounds.extents.y * 2.1f, col.bounds.extents.z);
        triggerbox.transform.SetParent(transform);
        triggerbox.transform.localPosition = new Vector3(0, col.bounds.extents.y, col.bounds.extents.z * 2);
        triggerbox.transform.localRotation = new Quaternion();
        triggerbox.layer = 2;
    }
    public void BoxScaleUpdate(Vector3 ScaleBounds, float y_offsetPercent)
    {
        ld.GetComponent<BoxCollider>().size = new Vector3(col.bounds.extents.x * ScaleBounds.x, col.bounds.extents.y * ScaleBounds.y, col.bounds.extents.z * ScaleBounds.z);
        ld.transform.localPosition = new Vector3(0, col.bounds.extents.y * (ScaleBounds.y * 0.5f) + y_offsetPercent * transform.localScale.y, col.bounds.extents.z * (ScaleBounds.z * 2f));
    }
    public void LedgeDrop()
    {
        ld.LedgeDrop();
    }
    public void EndableDetection(bool val)
    {
        ld.GetComponent<BoxCollider>().enabled = val;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //if (other.gameObject.layer == layerMask) other != transform.parent && 
    //    if (layerMask == (layerMask | (1 << other.gameObject.layer)))
    //    {
    //        Vector3 tmp = other.ClosestPoint(transform.position);
    //        Vector2 tmpHoriz = new Vector2(tmp.x, tmp.z);
    //        Vector2 positionHoriz = new Vector2(transform.position.x, transform.position.z);
    //        float horizontalDistance = Vector2.Distance(tmpHoriz, positionHoriz);
    //        float grabHeight = transform.position.y + col.bounds.extents.y * 2 - (Mathf.Pow(horizontalDistance, 2) * 0.4f);
    //        //float grabHeight = col.bounds.center.y + col.bounds.extents.y * 2;
    //        rayOrigin = new Vector3(tmp.x, grabHeight, tmp.z);
    //        Physics.Raycast(rayOrigin, -transform.up, out info, col.bounds.extents.y * 2);
    //        if (info.collider != null)
    //        {
    //            hitObject = info.collider.gameObject;
    //            ledgeGrab = true;
    //        }
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (ledgeGrab && other == info.collider)
    //    {
    //        hitObject = null;
    //        ledgeGrab = false;
    //    }
    //}
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(rayOrigin, 0.1f);
    //    Gizmos.DrawLine(rayOrigin, info.point);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(info.point, 0.1f);
    //}
}
