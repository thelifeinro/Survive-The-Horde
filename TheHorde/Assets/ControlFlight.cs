using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFlight : MonoBehaviour {

    public Rigidbody m_Rigidbody;
    public float m_Speed;
    public float rotateSpeed = 5;
    public bool bladeMoving = true;
    // Use this for initialization

    void Start () {
        //Fetch the Rigidbody component you attach from your GameObject
        m_Rigidbody = GetComponent<Rigidbody>();
        //Set the speed of the GameObject
    }
	
	// Update is called once per frame
	void Update () {
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            transform.Rotate(0, horizontal / 2, 0);

            float vertical = Input.GetAxis("Mouse Y");
            if (vertical > 0.5)
                vertical = 0.3f;
            if (vertical < -0.5)
                vertical = -0.3f;
            vertical = vertical * rotateSpeed;
            transform.Rotate(vertical, 0, 0);
            if (bladeMoving)
            {
            transform.Translate(transform.forward * m_Speed * Time.deltaTime, Space.World);
            }
    }

    public void disableMoving()
    {
        bladeMoving = false;
        m_Rigidbody.Sleep();
    }

}
