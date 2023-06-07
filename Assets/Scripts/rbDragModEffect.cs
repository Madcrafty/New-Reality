using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rbDragModEffect : MonoBehaviour
{
    public float dragOverride;
    public GameObject zoneObject;
    private float oldDragVar;
    public bool hasDuration;
    public bool ContactCondition;
    //public bool AirialCondition;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        oldDragVar = GetComponent<Rigidbody>().drag;
        GetComponent<Rigidbody>().drag = dragOverride;        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasDuration)
        {
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                Destroy(this);
            }
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (ContactCondition && collision.gameObject == zoneObject)
        {
            Destroy(this);
        }
    }
    private void OnDestroy()
    {
        GetComponent<Rigidbody>().drag = oldDragVar;
    }
}
