using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System;


using UnityEngine.UI;
using UnityEngine.EventSystems;

public class sCameraControlOrbit : MonoBehaviour
{
    public Transform target;
    public float Distance = 10.0f;
    public float ScrollSensitivity = 200f;

    public float XSpeed = 250.0f;
    public float YSpeed = 120.0f;

    public float YMinLimit = -20f;
    public float YMaxLimit = 80f;

    public float DistanceMin = 3f;
    public float DistanceMax = 15f;



    public static sCameraControlOrbit Instance;

    private float x = 0.0f;
    private float y = 0.0f;
    private Vector3 position;

    private float _targetDistance;
    private bool _interpolate = false;
    private Vector2? _targetAngels = null;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

        UpdatePosition(true);

        Time.timeScale = 1f;
    }

    void Update()
    {
        //if (_interpolate)
        //{
        //    Distance = Mathf.Lerp(Distance, _targetDistance, _t);

        //    if (_t <= 1f)
        //    {
        //        _t += TransformDuration * Time.smoothDeltaTime;
        //    }
        //    else
        //    {
        //        _targetDistance = Distance;
        //        _t = 0;
        //        _interpolate = false;
        //    }
        //}

        if (_targetAngels != null)
        {
            x = Mathf.LerpAngle(x, ((Vector2)_targetAngels).x, 0.1f);
            y = Mathf.LerpAngle(y, ((Vector2)_targetAngels).y, 0.1f);

            transform.rotation = Quaternion.Euler(y, x, 0);
            transform.position = transform.rotation * new Vector3(0.0f, 0.0f, -Distance) + target.position;

            if (Input.GetMouseButton(1) || Input.GetAxis("Mouse ScrollWheel") != 0)
                _targetAngels = null;
        }
        else
        {
            UpdatePosition();
        }
    }

    /*public void OnGUI()
    {
        GUI.Label(new Rect(5, Screen.height - 30, 200, 25), "Camera zoom: " + Distance);
    }*/

    public void SetTargetDistance(float targetDistance)
    {
        Distance = targetDistance;
        UpdatePosition(true);
    }

    public void SetTargetAngels(Vector2 angels)
    {
        _targetAngels = angels;
    }

    public void UpdatePosition(bool forceRecalculate = false)
    {
        if (target == null)
            return;

        if ((Input.GetMouseButton(1) || Input.GetAxis("Mouse ScrollWheel") != 0 || forceRecalculate || _interpolate))//  && !EventSystem.current.IsPointerOverGameObject()
        {
            if (Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * XSpeed * 0.035f;
                y -= Input.GetAxis("Mouse Y") * YSpeed * 0.045f;
                y = ClampAngle(y, YMinLimit, YMaxLimit);
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
                Distance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity, DistanceMin, DistanceMax);

            transform.rotation = Quaternion.Euler(y, x, 0);
            transform.position = transform.rotation * new Vector3(0.0f, 0.0f, -Distance) + target.position;

            //Debug.Log("x: " + x + " y:" + y);
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }

    public void ToggleEnabled()
    {
        enabled = !enabled;
    }
}