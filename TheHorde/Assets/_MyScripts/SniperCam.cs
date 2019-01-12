// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.

using UnityEngine;
using System.Collections;

public class SniperCam : MonoBehaviour
{
    //
    // VARIABLES
    //

    public float turnSpeed = 4.0f;      // Speed of camera turning when mouse moves in along an axis
    public float panSpeed = 100.0f;       // Speed of the camera when being panned
    public float zoomSpeed = 4.0f;      // Speed of the camera going back and forth

    private Vector3 mouseOrigin;    // Position of cursor when mouse dragging starts
    private bool isRotating = true;    // Is the camera being rotated?
    private bool isZooming;     // Is the camera zooming?
    public Camera cam;

    //
    // UPDATE
    //
    private void Start()
    {
        //cam.eventMask = ~cam.cullingMask;
    }

    void Update()
    {
        // Get the left mouse button
            mouseOrigin = Input.mousePosition;

        // Get the middle mouse button
        if (Input.GetMouseButtonDown(2))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isZooming = true;
        }

        // Disable movements on button release
        if (!Input.GetMouseButton(2)) isZooming = false;

        // Rotate camera along X and Y axis
        /* if (isRotating)
         {
             Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

             transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
             transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
         }
         */
        var c = gameObject.transform;
        c.Rotate(0, Input.GetAxis("Mouse X") * turnSpeed, 0);
        c.Rotate(-Input.GetAxis("Mouse Y") * turnSpeed, 0, 0);
        c.Rotate(0, 0, -Input.GetAxis("QandE") * 90 * Time.deltaTime);
        // Moving camera across XZ
        /*if (Input.mousePosition.y >= Screen.height * 0.95 || Input.mousePosition.x >= Screen.width * 0.95
            || Input.mousePosition.y <= Screen.height * 0.05 || Input.mousePosition.x <= Screen.width * 0.05)
        {
            Vector3 pos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move2 = new Vector3(pos1.x * panSpeed, 0, pos1.y * panSpeed);
            transform.Translate(move2, Space.World);
        }*/

        Vector3 posit = transform.position;
        transform.position = posit;
        // Move the camera linearly along Z axis
        if (isZooming)
        {
            Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = pos.y * zoomSpeed * transform.forward;
            transform.Translate(move, Space.World);
        }

        Vector3 move1 = Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed * transform.forward;
        transform.Translate(move1, Space.World);
    }
}