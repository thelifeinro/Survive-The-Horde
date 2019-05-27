// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.

using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
    //
    // VARIABLES
    //

    public float turnSpeed = 4.0f;      // Speed of camera turning when mouse moves in along an axis
    public float panSpeed = 100.0f;       // Speed of the camera when being panned
    public float zoomSpeed = 4.0f;      // Speed of the camera going back and forth

    public float minHeight = 5;
    public float maxHeight = 200;
    public float maxFront;
    public float maxBack;
    public float maxRight;
    public float maxLeft;

    private Vector3 mouseOrigin;    // Position of cursor when mouse dragging starts
    private bool isPanning;     // Is the camera being panned?
    private bool isRotating;    // Is the camera being rotated?
    private bool isZooming;     // Is the camera zooming?

    private float lastFrameTime;


    float shake_decay;
    float shake_intensity;
    Vector3 originPosition;
    Quaternion originRotation;

    private void Start()
    {
        lastFrameTime = Time.realtimeSinceStartup;
    }
    //
    // UPDATE
    //
    public void Shake()
    {
        originPosition = Camera.main.transform.position;
        originRotation = Camera.main.transform.rotation;
        shake_intensity = 0.16f;
        shake_decay = 0.01f;
    }

    void Update()
    {
        
        
        var myDeltaTime = Time.realtimeSinceStartup - lastFrameTime;
        lastFrameTime = Time.realtimeSinceStartup;
        //shaking
        if (shake_intensity > 0)
        {
            Debug.Log("Shake intensity:" + shake_intensity);
            Camera.main.transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            Camera.main.transform.rotation = new Quaternion(
                            originRotation.x + Random.Range(-shake_intensity, shake_intensity) * 0.2f,
                            originRotation.y + Random.Range(-shake_intensity, shake_intensity) * 0.2f,
                            originRotation.z + Random.Range(-shake_intensity, shake_intensity) * 0.2f,
                            originRotation.w + Random.Range(-shake_intensity, shake_intensity) * 0.2f);
            shake_intensity -= shake_decay;
        }


        // Get the left mouse button
        if (Input.GetMouseButtonDown(1))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isRotating = true;
        }

        // Get the right mouse button
        /*
        if (Input.GetMouseButtonDown(1))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }*/

        // Get the middle mouse button
        if (Input.GetMouseButtonDown(2))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isZooming = true;
        }

        // Disable movements on button release
        if (!Input.GetMouseButton(1)) isRotating = false;
        //if (!Input.GetMouseButton(1)) isPanning = false;
        if (!Input.GetMouseButton(2)) isZooming = false;

        // Rotate camera along X and Y axis
        if (isRotating)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
            transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
        }

        // Moving camera across XZ
        /*if (Input.mousePosition.y >= Screen.height * 0.95 || Input.mousePosition.x >= Screen.width * 0.95
            || Input.mousePosition.y <= Screen.height * 0.05 || Input.mousePosition.x <= Screen.width * 0.05)
        {
            Vector3 pos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move2 = new Vector3(pos1.x * panSpeed, 0, pos1.y * panSpeed);
            transform.Translate(move2, Space.World);
        }*/
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 pos = transform.position;
            Vector3 vector = Quaternion.Euler(0, -90, 0) * transform.right * panSpeed * myDeltaTime;
            transform.Translate(vector, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 pos = transform.position;
            if (pos.z > maxBack)
            {
                Vector3 vector = Quaternion.Euler(0, -90, 0) * transform.right * panSpeed * myDeltaTime;
                transform.Translate(-vector, Space.World);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 pos = transform.position;
            Vector3 vector = transform.right * panSpeed * myDeltaTime;
            transform.Translate(-vector, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 pos = transform.position;
            Vector3 vector = transform.right * panSpeed * myDeltaTime;
            transform.Translate(vector, Space.World);
        }

        Vector3 posit = transform.position;
        if (posit.z > maxFront)
            posit.z = maxFront;
        if (posit.z < maxBack)
            posit.z = maxBack;
        if (posit.x > maxRight)
            posit.x = maxRight;
        if (posit.x < maxLeft)
            posit.x = maxLeft;
        transform.position = posit;
        // Move the camera linearly along Z axis
        if (isZooming)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = pos.y * zoomSpeed * transform.forward;
            transform.Translate(move, Space.World);
        }

        Vector3 move1 = Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed * transform.forward;
        transform.Translate(move1, Space.World);

        Vector3 actualPos = transform.position;
        if (actualPos.y <= minHeight)
        {
            actualPos.y = minHeight;
        }
        if (actualPos.y >= maxHeight)
        {
            actualPos.y = maxHeight;
        }
        transform.position = actualPos;
    }
}