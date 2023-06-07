using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillboardCanvas : MonoBehaviour
{
    //[Tooltip("Time (in seconds) the canvas is active after being updated")]
    //public float activeTime;
    //public bool startOn = false;

    //public Slider slider;
    private Canvas canvas;
    private Transform camTransform;
    private float curTime;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        camTransform = Camera.main.transform;
        //if (!startOn)
        //{
        //    curTime = activeTime;
        //}   
    }

    // Update is called once per frame
    void Update()
    {
        //camTransform = Camera.main.transform;
        //if (curTime >= activeTime)
        //{
        //    canvas.enabled = false;
        //}
        //else
        //{
        //    curTime += Time.deltaTime;
        //}
        transform.LookAt(camTransform);
    }
    //public void UpdateCanvasSlider(float value)
    //{
    //    //slider.value = value;
    //    image.fillAmount = value;
    //    curTime = 0;
    //    canvas.enabled = true;
    //}
    public void DisbleBilboard()
    {
        canvas.enabled = false;
    }
}
