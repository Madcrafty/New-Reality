using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class BoomerangAbility : MonoBehaviour
{
    public float throwRange = 5;
    public float throwTime = 1;
    public float holdTime = 3;
    private PlayerMotor curPlayer;
    private Collider col;
    private bool isThrown;
    private InputAction.CallbackContext input;
    private bool inputCanceled;
    //private bool boomerangMode;
    private Vector3 StartPoint;
    private Vector3 ThrowPoint;
    private int stage = 0;
    private float t = 0;
    // Start is called before the first frame update
    private void Start()
    {
        col = GetComponent<Collider>();
    }
    public void Assign(GameObject thing)
    {
        thing.GetComponent<PlayerMotor>().AssignInteraction(Boomerang);
    }
    public void Remove(GameObject thing)
    {
        thing.GetComponent<PlayerMotor>().RemoveInteraction(Boomerang);
    }
    void Boomerang(GameObject thing, InputAction.CallbackContext value)
    {
        Debug.Log("Bomerange call & isThrown = " + isThrown);
        input = value;
        inputCanceled = value.canceled;
        if (value.started)
        {
            if (!isThrown)
            {
                isThrown = true;
                Pickup PlayerPickup = thing.GetComponent<Pickup>();
                PlayerPickup.RemoveHeldObject();
                col.enabled = true;
                stage = 1;
                curPlayer = thing.GetComponent<PlayerMotor>();
                StartPoint = transform.position;
                ThrowPoint = transform.position + curPlayer.GetPlayerCameraTransform().forward * throwRange;
            }
        }
    }
    private void Update()
    {
        //if boomerang mode is on
        switch (stage)
        {
            case 1:
                t += Time.deltaTime;
                if (t > throwTime)
                {
                    t = throwTime;
                }
                transform.position = Vector3.Lerp(StartPoint, ThrowPoint, t / throwTime);
                if (t / throwTime == 1)
                {
                    t = 0;
                    if (input.interaction is HoldInteraction)
                    {
                        stage = 2;                      
                    }
                    else
                    {
                        stage = 3;
                    }
                }
                break;
            case 2:
                t += Time.deltaTime;
                if (inputCanceled || t >= holdTime)
                {
                    t = 0;
                    stage = 3;
                }
                break;
            case 3:
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(ThrowPoint, curPlayer.transform.position, t / throwTime);
                break;
            default:
                break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<PlayerMotor>() != null)
        {
            if (isThrown)
            {
                if (collision.transform.GetComponent<Pickup>().GetHeldObject() == null)
                {
                    stage = 0;
                    t = 0;
                    isThrown = false;
                    ReGrabLogic(collision.transform.GetComponent<Pickup>());
                }
                else
                {
                    stage = 0;
                    t = 0;
                    isThrown = false;
                    Drop(collision.gameObject);
                }
            }
        }
    }
    public void Drop(GameObject player)
    {
        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
        else if (GetComponent<Rigidbody>().isKinematic == true)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
        // Collider Checks
        if (GetComponent<Collider>().enabled == false)
        {
            GetComponent<Collider>().enabled = true;
        }
        if (GetComponent<OnPickup>() != null)
        {
            GetComponent<OnPickup>().OnDropEvent.Invoke(player);
        }
    }
    public void ReGrabLogic(Pickup pickup)
    {
        // Rigidbody Checks
        if (GetComponent<Rigidbody>() != null && GetComponent<Rigidbody>().isKinematic == false)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        // Collider Checks
        if (GetComponent<Collider>() != null && GetComponent<Collider>().enabled == true)
        {
            GetComponent<Collider>().enabled = false;
        }
        pickup.AddHeldObject(gameObject);
    }
}