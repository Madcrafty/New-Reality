using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InteractDetection : MonoBehaviour
{
    public Mesh mesh;
    public float _grabBaseRange = 1f;
    public float _grabRangeScale = 1f;
    public Vector3 _grabBoxDimensions;
    //public bool GrabDisabled;
    //public GameObject highlightPrefab;
    private RaycastHit _info;
    //private GameObject _highlightLight;
    //private UIManager _UIManager;
    private Vector3 _trueBoxDimensions;
    private Vector3 StartPoint;
    private float _trueGrabRange;
    private Vector3 Endpoint;
    private void OnValidate()
    {
        SetUpGrab();
    }
    public void Start()
    {
        SetUpGrab();
        //_highlightLight = Instantiate(highlightPrefab);
        //_highlightLight.SetActive(false);
        //_highlightLight.transform.parent = gameObject.transform;
    }
    void SetUpGrab()
    {
        StartPoint = transform.position;
        _trueGrabRange = _grabBaseRange * transform.localScale.magnitude * _grabRangeScale;
        Endpoint = StartPoint + transform.forward * _trueGrabRange;
        _trueBoxDimensions = new Vector3(_grabBoxDimensions.x * transform.localScale.x, _grabBoxDimensions.y * transform.localScale.y, _grabBoxDimensions.z * transform.localScale.z);
    }
    public RaycastHit Detection(LayerMask layerMask)
    {
        _trueGrabRange = _grabBaseRange * transform.localScale.magnitude * _grabRangeScale;
        Physics.BoxCast(StartPoint, _trueBoxDimensions, transform.forward, out _info, transform.rotation, _trueGrabRange, layerMask);
        //if (_info.collider != null && _info.collider.name != name)
        //{
        //    _UIManager = _info.transform.GetComponent<UIManager>();
        //}
        return _info;
    }
    private void Update()
    {
        SetUpGrab();
    }
    public RaycastHit GetHitInfo()
    {
        return _info;
    }
    public void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + transform.forward * _grabRangeScale * transform.localScale.z);
        Gizmos.DrawLine(StartPoint, Endpoint);
        //Gizmos.DrawCube(Endpoint, _grabBoxDimensions);
        Gizmos.DrawWireMesh(mesh, Endpoint, transform.rotation, _trueBoxDimensions * 2f);
    }
}
