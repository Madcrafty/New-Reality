using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rbDragModMat : MonoBehaviour
{
    [Min(0)]
    public float dragOverride = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Rigidbody>() != null)
        {
            rbDragModEffect dme = collision.gameObject.AddComponent<rbDragModEffect>();
            dme.ContactCondition = true;
            dme.zoneObject = gameObject;
        }
    }
}
