using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SmoothZoom : MonoBehaviour {
    public Camera cam;
    // Use this for initialization
    public float zoomSensitivity = 15.0f;
    public float zoomSpeed = 5.0f;
    public float zoomMin = 5.0f;
    public float zoomMax = 80.0f;
    private float zoom;
    public FirstPersonController fpscontr;

    void Start()
    {
        zoom = cam.fieldOfView;
    }

    void Update()
    {

        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
    }

    void LateUpdate()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoom, Time.deltaTime * zoomSpeed);
    }
}
