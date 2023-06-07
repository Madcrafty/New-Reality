using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnPickup : MonoBehaviour
{
    public UnityEvent<GameObject> OnPickupEvent = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> OnDropEvent = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> OnThrowEvent = new UnityEvent<GameObject>();
}
