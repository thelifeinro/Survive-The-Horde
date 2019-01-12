using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour {

    public Camera mainCam;
    public Camera followCam;
    public GameObject rotatingBlade;
    public float waitTimeBeforeExit;
    public FlyingBlade fbscript;

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        followCam.enabled = true;
        mainCam.enabled = false;
        fbscript = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<FlyingBlade>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(waitTimeBeforeExit);
        
        mainCam.enabled = true;
        followCam.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        //end this
        fbscript.StopAttack();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit!");
        if (collision.transform.tag != "Enemy")
        {
            rotatingBlade.GetComponent<rotateBlade>().disableRotation();
            gameObject.GetComponent<ControlFlight>().disableMoving();
            //change back to mainCamera
            StartCoroutine(waiter());
        }
        else
        {
            Enemy e = collision.transform.GetComponent<Enemy>();
            e.Die();
        }
        
       
       // return;
    }
}
