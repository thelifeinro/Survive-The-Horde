using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedScaleUI : MonoBehaviour {

    public Transform point;

	// Use this for initialization
	void Start () {
        if(point == null)
            point = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v3 = Camera.main.WorldToScreenPoint(point.transform.position);
        if (v3.z < 0)
            v3 *= -1;
        transform.position = v3;
    }
}
