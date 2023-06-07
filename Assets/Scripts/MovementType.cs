using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "movement", menuName = "ScriptableObjects/movement")]
public class MovementType : ScriptableObject
{
    public int midairJumps;
    public float speed;
    public float jumpForce;
    public Vector3 normalScaleGrabBoxSize;
    public Vector3 lastjumpScaleGrabBoxSize;
    private int curJumps;
    private Vector3 movedir;
    private GroundDetection gd;
    private GrabLedgeDetection gld;
    private Rigidbody _rb;
    private bool Grabbing = false;
    private Transform camTranz;

    public void Setup(Rigidbody rb)
    {
        Grabbing = false;
        _rb = rb;
        if (rb.GetComponent<GroundDetection>() == null)
        {
            gd = rb.gameObject.AddComponent<GroundDetection>();
        }
        else
        {
            gd = rb.GetComponent<GroundDetection>();
        }
        if (rb.GetComponent<GrabLedgeDetection>() == null)
        {
            gld = rb.gameObject.AddComponent<GrabLedgeDetection>();
        }
        else
        {
            gld = rb.GetComponent<GrabLedgeDetection>();
        }
        camTranz = rb.transform.Find("Main Camera");
    }
    public void Movement(Vector3 dir, Rigidbody rb)
    {
        movedir = dir;
        rb.AddRelativeForce(dir * speed);
        if (!gld.ledgeGrab)
        {
            rb.AddForce(-rb.transform.up * 10);
        }
        if (gd.grounded || gld.ledgeGrab)
        {
            restoreJumps();
        }
        if (gld.ledgeGrab && gld.ledgeGrab != Grabbing)
        {
            GrabLedge();
        }
        if (!gld.ledgeGrab)
        {
            Grabbing = false;
        }
        if (gd.grounded)
        {
            gld.EndableDetection(false);
        }
        else if (!gd.grounded && curJumps != 0)
        {
            float tmp = camTranz.transform.localEulerAngles.x;
            if (tmp > 180)
            {
                tmp -= 360;
            }
            if (Grabbing)
            {
                tmp = 30;
            }
            gld.EndableDetection(true);
            gld.BoxScaleUpdate(normalScaleGrabBoxSize, (float)Mathf.Clamp(-tmp/ 90, -1, 1));
            //gld.BoxScaleUpdate(normalScaleGrabBoxSize, (float)Mathf.Clamp(camTranz.transform.localEulerAngles.x / 90, -1, 1));
        }
        else
        {
            float tmp = camTranz.transform.localEulerAngles.x;
            if (tmp > 180)
            {
                tmp -= 360;
            }
            if (Grabbing)
            {
                tmp = 30;
            }
            gld.EndableDetection(true);
            gld.BoxScaleUpdate(lastjumpScaleGrabBoxSize, (float)Mathf.Clamp(-tmp / 90, -1, 1));
            //gld.BoxScaleUpdate(lastjumpScaleGrabBoxSize, (float)Mathf.Clamp(camTranz.transform.rotation.eulerAngles.x / 90, -1, 1));
        }
    }
    public void Jump(Rigidbody rb)
    {
        if (gd.grounded || gld.ledgeGrab || curJumps > 0)
        {
            if (gld.ledgeGrab)
            {
                LedgeDrop();
            }
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            curJumps--;

        }
        //else if (curJumps > 0)
        //{
        //    rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        //    curJumps--;
        //}
    }
    public void Sneak(Rigidbody rb)
    {
        if (gld.ledgeGrab)
        {
            LedgeDrop();
        }
        //else if (curJumps > 0)
        //{
        //    rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        //    curJumps--;
        //}
    }
    public void JumpCancel(Rigidbody rb)
    {
        if (rb.velocity.y>0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y/4.0f, rb.velocity.z);
        }
    }
    public void restoreJumps()
    {
        curJumps = midairJumps;
    }
    public void restoreJumps(int number)
    {
        if (curJumps + number > midairJumps)
        {
            curJumps = midairJumps;
        }
        else
        {
            curJumps += number;
        }
    }
    public void GrabLedge()
    {
        _rb.useGravity = false;
        _rb.position = new Vector3(_rb.position.x, gld.GrabPoint.y, _rb.position.z);
        _rb.velocity = new Vector3(0,0,0);
         Grabbing = true;
    }
    public void LedgeDrop()
    {
        gld.LedgeDrop();
        _rb.useGravity = true;
        Grabbing = false;
    }
    public void LedgeGetup()
    {

    }
}
