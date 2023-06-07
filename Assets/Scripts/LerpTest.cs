using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    private Vector3 StartPoint;
    private Vector3 ThrowPoint;
    // Start is called before the first frame update
    void Start()
    {
        StartPoint = transform.position;
        ThrowPoint = transform.position + transform.up;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
