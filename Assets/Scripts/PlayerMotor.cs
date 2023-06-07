using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerMotor : MonoBehaviour
{
    private Vector3 rawInputMovement;
    private Rigidbody rb;
    //private Collider col;
    private Transform camTranz;
    public Action<GameObject, InputAction.CallbackContext> InteractAction;
    //private List<Action<GameObject>> ContinuousHoldUpdate = new List<Action<GameObject>>();
    public float lookSensitivity;
    public MovementType curMovement;
    public PlayerInput memes;
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //col = GetComponent<Collider>();
        camTranz = transform.GetChild(0);
        curMovement.Setup(rb);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public Transform GetPlayerCameraTransform()
    {
        return camTranz;
    }
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        //transform.LookAt(transform.position + rawInputMovement);
        //float speedmod = Mathf.Sqrt(Mathf.Pow(inputMovement.x, 2) + Mathf.Pow(inputMovement.y, 2));
        //anim.SetFloat("Speed", _effectiveSpeed * speedmod);
    }
    public void OnLook(InputAction.CallbackContext value)
    {

        Vector2 inputLook = value.ReadValue<Vector2>();
        transform.Rotate(0, inputLook.x/1000.0f * lookSensitivity, 0);
        camTranz.Rotate(-inputLook.y / 1000.0f * lookSensitivity, 0, 0);
    }
    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.performed == true)
        {
            curMovement.Jump(rb);
        }
        if (value.canceled == true)
        {
            curMovement.JumpCancel(rb);
        }
    }
    public void OnSneak(InputAction.CallbackContext value)
    {
        if (value.performed == true)
        {
            curMovement.Sneak(rb);
        }
    }
    public void OnInteraction(InputAction.CallbackContext value)
    {
        if (InteractAction != null)
        {
            //InteractAction.Invoke(gameObject);
            InteractAction.Invoke(gameObject, value);
            Debug.Log("interaction call");
        }
        //if (value.canceled == true)
        //    if (InteractAction != null)
        //        ContinuousHoldUpdate.Clear(); Debug.Log("interaction let go");

    }
    public void AssignInteraction(Action<GameObject, InputAction.CallbackContext> func)
    {
        InteractAction += func;
    }
    public void RemoveInteraction(Action<GameObject, InputAction.CallbackContext> func)
    {
        InteractAction -= func;
        // Calling this function here still allows things to work but may bring up an error
        //ContinuousHoldUpdate.Clear();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        curMovement.Movement(rawInputMovement, rb);
    }
    // ledge detection

}
