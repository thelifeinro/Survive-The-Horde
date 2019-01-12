using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateBlade : MonoBehaviour {

    private bool rotating = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (rotating )
            transform.Rotate(new Vector3(0, 5, 0));
	}

    public void disableRotation()
    {
        rotating = false;
    }
}
