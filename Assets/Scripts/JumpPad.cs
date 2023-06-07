using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 15;
    public bool addForce = false;
    // I cant find a way to refference public variables on a modular script so i am doing this hard coding bs for now
    private TextMeshProUGUI textmesh;
    private void Start()
    {
        if (textmesh == null)
        {
            textmesh = transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        }
    }
    private void Update()
    {
        if (textmesh.text != jumpForce.ToString())
        {
            textmesh.text = jumpForce.ToString();
        }
    }
    private void OnDrawGizmos()
    {
        if (textmesh == null)
        {
            textmesh = transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        }
        if (textmesh.text != jumpForce.ToString())
        {
            textmesh.text = jumpForce.ToString();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            if (!addForce)
            {
                other.GetComponent<Rigidbody>().velocity = transform.up * jumpForce;
            }
            else
            {
                other.GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Rigidbody>() != null)
        {
            if (!addForce)
            {
                collision.transform.GetComponent<Rigidbody>().velocity = transform.up * jumpForce;
            }
            else
            {
                collision.transform.GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);
            }
        }
        // if boomerang mode is active and makes contact with player
        //  check if player is holding item
        //  if so: disable lerp, apply normal drop functions like add / enabling rigidbody
        //  if not: disable lerp, apply normal grab functions
    }
}
